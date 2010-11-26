/* ------------------------------------------------------------------------- */
/*
 *  CanvasPolicy.cs
 *
 *  Copyright (c) 2010 CubeSoft Inc. All rights reserved.
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see < http://www.gnu.org/licenses/ >.
 *
 *  Last-modified: Mon 18 Oct 2010 13:40:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Container = System.Collections.Generic;
using Canvas = System.Windows.Forms.PictureBox;
using PDF = PDFLibNet.PDFWrapper;

namespace Cube {
    /* --------------------------------------------------------------------- */
    /// FitCondition
    /* --------------------------------------------------------------------- */
    public enum FitCondition {
        None = 0x00, Width = 0x01, Height = 0x02
    }

    /* --------------------------------------------------------------------- */
    ///
    /// CanvasEngine
    /// 
    /// <summary>
    /// PDF の内容を表示・操作するために必要な各種情報を保持するための
    /// クラス．通常は，Canvas.Tag に登録しておく．
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class CanvasEngine : IDisposable {
        /* ----------------------------------------------------------------- */
        /// constructor
        /* ----------------------------------------------------------------- */
        public CanvasEngine(PDF core) {
            core_ = core;
            Thumbnail = null;
        }

        /* ----------------------------------------------------------------- */
        /// Core
        /* ----------------------------------------------------------------- */
        public PDF Core {
            get { return core_; }
            set { core_ = value; }
        }

        /* ----------------------------------------------------------------- */
        /// Thumbnail
        /* ----------------------------------------------------------------- */
        public Thumbnail Thumbnail { get; set; }

        /* ----------------------------------------------------------------- */
        ///
        /// UpdateURL
        /// 
        /// <summary>
        /// 現在のページに記載されている URL 情報を取得する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public void UpdateURL() {
            lock (lock_) {
                if (core_ == null) return;
                lock (core_) {
                    string src = null;
                    src = core_.Pages[core_.CurrentPage].Text;
                    if (src == null) return;

                    core_.PreserveSearchResults();
                    urls_.Clear();

                    // parse http/https/ftp addresses.
                    Regex http = new Regex(@"(https?|ftp)(:\/\/[\-_.!~*\'()a-zA-Z0-9;\/?:\@&=+\$,%#]+)");
                    for (var item = http.Match(src); item.Success; item = item.NextMatch()) {
                        var order = PDFLibNet.PDFSearchOrder.PDFSearchFromCurrent;
                        if (core_.FindFirst(item.Value, order, false, false) > 0) {
                            foreach (var result in core_.SearchResults) {
                                if (result.Page == core_.CurrentPage) {
                                    urls_.Add(new Container.KeyValuePair<string, Rectangle>(item.Value, result.Position));
                                }
                            }
                        }
                    }
                    
                    // parse mail addresses.
                    Regex mail = new Regex(@"([a-zA-Z0-9])+([a-zA-Z0-9\._\-\+])*@([a-zA-Z0-9_\-])+([a-zA-Z0-9\._\-]+)+");
                    for (var item = mail.Match(src); item.Success; item = item.NextMatch()) {
                        var order = PDFLibNet.PDFSearchOrder.PDFSearchFromCurrent;
                        if (core_.FindFirst(item.Value, order, false, false) > 0) {
                            foreach (var result in core_.SearchResults) {
                                if (result.Page == core_.CurrentPage) {
                                    urls_.Add(new Container.KeyValuePair<string, Rectangle>("mailto:" + item.Value, result.Position));
                                }
                            }
                        }
                    }

                    core_.RecoverSearchResults();
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetURL
        /// 
        /// <summary>
        /// PDF ファイルの現在のページの指定された座標に URL が存在する
        /// 場合はその URL を取得する．存在しない場合は null.
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public string GetURL(Point pos) {
            lock (lock_) {
                foreach (var item in urls_) {
                    if (pos.X >= item.Value.Left && pos.X <= item.Value.Right &&
                        pos.Y >= item.Value.Top && pos.Y <= item.Value.Bottom) {
                        return item.Key;
                    }
                }
            }
            return null;
        }

        /* ----------------------------------------------------------------- */
        /// Dispose
        /* ----------------------------------------------------------------- */
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /* ----------------------------------------------------------------- */
        /// Dispose
        /* ----------------------------------------------------------------- */
        protected virtual void Dispose(bool disposing) {
            lock (lock_) {
                if (!disposed_) {
                    if (disposing) {
                        urls_.Clear();
                        if (core_ != null) core_ = null;
                    }
                }
                disposed_ = true;
            }
        }

        /* ----------------------------------------------------------------- */
        /// メンバ変数の定義
        /* ----------------------------------------------------------------- */
        #region Variables
        private PDF core_ = null;
        private bool disposed_ = false;
        private object lock_ = new object();
        private Container.List<Container.KeyValuePair<string, Rectangle>> urls_ =
            new Container.List<Container.KeyValuePair<string, Rectangle>>();
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// CanvasPolicy
    /// 
    /// <summary>
    /// PDFWrapper を用いて画面に PDF を内容を表示するための制御関数群
    /// をまとめた抽象クラス．Canvas.Tag に PDFWrapper オブジェクト
    /// を設定し，このオブジェクトが保持する情報を基にして描画を行う．
    /// 現在の実装では，Canvas として PictureBox を利用している．
    /// 
    /// TODO: PictureBox への補助関数群として実装したが，保持しなければ
    /// ならない状態が増えてきたので PictureBox の継承クラスとして
    /// 書き直す．
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public abstract class CanvasPolicy {
        /* ----------------------------------------------------------------- */
        /// Get
        /* ----------------------------------------------------------------- */
        public static Canvas Get(Control parent) {
            return (Canvas)parent.Controls["Canvas"];
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Create
        /// 
        /// <summary>
        /// 新たな Canvas オブジェクトを生成し，parent のコントロールに
        /// 追加する．
        /// 
        /// TODO: 既に Canvas オブジェクトが存在している場合の処理．
        /// 現状では，何もしない．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static Canvas Create(ScrollableControl parent) {
            var canvas = (Canvas)parent.Controls["Canvas"];
            if (canvas == null) {
                canvas = new Canvas();

                // プロパティ
                canvas.Name = "Canvas";
                canvas.BackColor = Color.Transparent;
                canvas.Size = parent.ClientSize;
                canvas.ClientSize = canvas.Size;

                // イベントハンドラの登録
                canvas.Paint += new PaintEventHandler(CanvasPolicy.PaintHandler);
                canvas.MouseDown += new MouseEventHandler(CanvasPolicy.MouseDownHandler);
                canvas.MouseUp += new MouseEventHandler(CanvasPolicy.MouseUpHandler);
                canvas.MouseMove += new MouseEventHandler(CanvasPolicy.MouseMoveHandler);
                canvas.MouseEnter += new EventHandler(CanvasPolicy.MouseEnterHandler);
                canvas.MouseClick -= new MouseEventHandler(CanvasPolicy.MouseClickHandler);
                canvas.MouseClick += new MouseEventHandler(CanvasPolicy.MouseClickHandler);

                parent.Controls.Add(canvas);
            }
            return canvas;
        }

        /* ----------------------------------------------------------------- */
        /// Destroy
        /* ----------------------------------------------------------------- */
        public static void Destroy(Canvas canvas) {
            if (canvas == null) return;

            var parent = (ScrollableControl)canvas.Parent;
            foreach (var child in parent.Controls.Find("Canvas", false)) {
                CanvasPolicy.Close((Canvas)child);
                parent.Controls.Remove(child);
                child.Dispose();
            }
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// Open
        /// 
        /// <summary>
        /// 指定された PDF ファイルを開いて，最初のページを描画する．
        /// MEMO: パスの記憶場所を検討中．現在は，Parent.Tag を利用
        /// している．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static void Open(Canvas canvas, string path, string password, FitCondition which) {
            if (canvas == null) return;
            var engine = canvas.Tag as CanvasEngine;
            if (engine != null) CanvasPolicy.Close(canvas);

            var core = new PDF();
            engine = new CanvasEngine(core);
            canvas.Tag = engine;
            core.UseMuPDF = true;
            core.UserPassword = password;
            core.OwnerPassword = password;

            if (core.LoadPDF(path)) {
                core.CurrentPage = 1;
                if (which == FitCondition.Height) {
                    core.FitToHeight(canvas.Parent.Handle);
                    core.Zoom = core.Zoom - 1; // 暫定
                }
                else if (which == FitCondition.Width) {
                    core.FitToWidth(canvas.Parent.Handle);
                    core.Zoom = core.Zoom - 1; // 暫定
                }
                else core.Zoom = 100;
                canvas.Parent.Text = System.IO.Path.GetFileNameWithoutExtension(path);
                canvas.Parent.Tag = path;
                CanvasPolicy.AsyncRender(canvas, true);
            }
        }

        /* ----------------------------------------------------------------- */
        /// Open
        /* ----------------------------------------------------------------- */
        public static void Open(Canvas canvas, string path, FitCondition which) {
            CanvasPolicy.Open(canvas, path, "", which);
        }

        /* ----------------------------------------------------------------- */
        /// Open
        /* ----------------------------------------------------------------- */
        public static void Open(Canvas canvas, string path) {
            CanvasPolicy.Open(canvas, path, "", FitCondition.None);
        }

        /* ----------------------------------------------------------------- */
        /// 
        /// Close
        /// 
        /// <summary>
        /// ファイルを閉じる．
        /// 
        /// MEMO: 今のところ，Close() 自体は非公開とする．
        /// ユーザには，代わりに Destroy() を用いて，描画領域の破棄と
        /// 同時に行ってもらう．
        /// 
        /// TODO: 現在，ファイルを開いているかどうかを描画領域が生成されて
        /// いるかどうかで判断している．これを Canvas.Parent.Tag にパスが
        /// 設定されているかどうかに変える（その時点で public にしても
        /// OK か）．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private static void Close(Canvas canvas) {
            if (canvas == null || canvas.Tag == null) return;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return;
            var core = engine.Core;
            if (core == null) return;

            engine.Dispose();
            core.Dispose();
            canvas.Tag = null;

            var parent = (ScrollableControl)canvas.Parent;
            parent.Tag = null;
            parent.Text = "(無題)";
        }

        /* ----------------------------------------------------------------- */
        /// PageSize
        /* ----------------------------------------------------------------- */
        public static Size PageSize(Canvas canvas) {
            if (canvas == null) return new Size(0, 0);
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return new Size(0, 0);
            var core = engine.Core;
            if (core == null) return new Size(0, 0);

            return new Size(core.PageWidth, core.PageHeight);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PageCount
        /// 
        /// <summary>
        /// 表示されている PDF のページ数を返す．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static int PageCount(Canvas canvas) {
            if (canvas == null) return 0;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return 0;
            var core = engine.Core;
            if (core == null) return 0;

            return core.PageCount;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CurrentPage
        /// 
        /// <summary>
        /// 現在表示されているページ番号を返す．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static int CurrentPage(Canvas canvas) {
            if (canvas == null) return 0;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return 0;
            var core = engine.Core;
            if (core == null) return 0;

            return core.CurrentPage;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MovePage
        /// 
        /// <summary>
        /// 指定したページを描画する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static int MovePage(Canvas canvas, int page) {
            if (canvas == null) return 0;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return 0;
            var core = engine.Core;
            if (core == null) return 0;
            
            int n = Math.Min(Math.Max(page, 1), core.PageCount);
            core.CurrentPage = n;
#if CUBE_ASYNC
            CanvasPolicy.AsyncRender(canvas, false);
            var control = (ScrollableControl)canvas.Parent;
            control.AutoScrollPosition = new Point(0, 0);
#else
            if (CanvasPolicy.Render(canvas, false)) {
                engine.UpdateURL();
                var control = (ScrollableControl)canvas.Parent;
                control.AutoScrollPosition = new Point(0, 0);
            }
#endif
            return core.CurrentPage;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// NextPage
        /// 
        /// <summary>
        /// 次のページを描画する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static int NextPage(Canvas canvas) {
            if (canvas == null) return 0;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return 0;
            var core = engine.Core;
            if (core == null) return 0;

            core.NextPage();

#if CUBE_ASYNC
            CanvasPolicy.AsyncRender(canvas, false);
            var control = (ScrollableControl)canvas.Parent;
            control.AutoScrollPosition = new Point(0, 0);
#else
            if (CanvasPolicy.Render(canvas, false)) {
                engine.UpdateURL();
                var control = (ScrollableControl)canvas.Parent;
                control.AutoScrollPosition = new Point(0, 0);
            }
#endif
            return core.CurrentPage;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PreviousPage
        /// 
        /// <summary>
        /// 前のページを描画する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static int PreviousPage(Canvas canvas) {
            if (canvas == null) return 0;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return 0;
            var core = engine.Core;
            if (core == null) return 0;

            core.PreviousPage();

#if CUBE_ASYNC
            CanvasPolicy.AsyncRender(canvas, false);
            var control = (ScrollableControl)canvas.Parent;
            control.AutoScrollPosition = new Point(0, 0);
#else
            if (CanvasPolicy.Render(canvas, false)) {
                engine.UpdateURL();
                var control = (ScrollableControl)canvas.Parent;
                control.AutoScrollPosition = new Point(0, 0);
            }
#endif
            return core.CurrentPage;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FirstPage
        /// 
        /// <summary>
        /// 最初のページを描画する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static int FirstPage(Canvas canvas) {
            if (canvas == null) return 0;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return 0;
            var core = engine.Core;
            if (core == null) return 0;

            core.CurrentPage = 1;
#if CUBE_ASYNC
            CanvasPolicy.AsyncRender(canvas, false);
            var control = (ScrollableControl)canvas.Parent;
            control.AutoScrollPosition = new Point(0, 0);
#else
            if (CanvasPolicy.Render(canvas, false)) {
                engine.UpdateURL();
                var control = (ScrollableControl)canvas.Parent;
                control.AutoScrollPosition = new Point(0, 0);
            }
#endif
            return core.CurrentPage;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LastPage
        /// 
        /// <summary>
        /// 最後のページを描画する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static int LastPage(Canvas canvas) {
            if (canvas == null) return 0;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return 0;
            var core = engine.Core;
            if (core == null) return 0;

            core.CurrentPage = core.PageCount;
#if CUBE_ASYNC
            CanvasPolicy.AsyncRender(canvas, false);
            var control = (ScrollableControl)canvas.Parent;
            control.AutoScrollPosition = new Point(0, 0);
#else
            if (CanvasPolicy.Render(canvas, false)) {
                engine.UpdateURL();
                var control = (ScrollableControl)canvas.Parent;
                control.AutoScrollPosition = new Point(0, 0);
            }
#endif
            return core.CurrentPage;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Zoom
        /// 
        /// <summary>
        /// 現在表示されている画像の表示倍率を返す．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static double Zoom(Canvas canvas) {
            if (canvas == null) return 0.0;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return 0.0;
            var core = engine.Core;
            if (core == null) return 0.0;

            return core.Zoom;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Zoom
        /// 
        /// <summary>
        /// 指定した倍率で現在のページを再描画する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static double Zoom(Canvas canvas, double percent) {
            if (canvas == null) return 0.0;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return 0.0;
            var core = engine.Core;
            if (core == null) return 0.0;

            var prev = canvas.Size;
            if (percent < core.Zoom || core.Zoom < 400) {
                core.Zoom = Math.Min(percent, 400);
#if CUBE_ASYNC
                CanvasPolicy.AsyncRender(canvas, true);
#else
                CanvasPolicy.Render(canvas, true);
#endif
            }
            return core.Zoom;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ZoomIn
        /// 
        /// <summary>
        /// 現在のページを拡大して再描画する．拡大率はライブラリ依存．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static double ZoomIn(Canvas canvas) {
            if (canvas == null) return 0.0;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return 0.0;
            var core = engine.Core;
            if (core == null) return 0.0;

            var prev = canvas.Size;
            if (core.Zoom < 400) {
                core.ZoomIN();
                if (core.Zoom > 400) core.Zoom = 400;
#if CUBE_ASYNC
                CanvasPolicy.AsyncRender(canvas, true);
#else
                CanvasPolicy.Render(canvas, true);
#endif
            }
            return core.Zoom;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ZoomOut
        /// 
        /// <summary>
        /// 現在のページを縮小して再描画する．縮小率はライブラリ依存．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static double ZoomOut(Canvas canvas) {
            if (canvas == null) return 0.0;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return 0.0;
            var core = engine.Core;
            if (core == null) return 0.0;

            var prev = canvas.Size;
            core.ZoomOut();
#if CUBE_ASYNC
            CanvasPolicy.AsyncRender(canvas, true);
#else
            CanvasPolicy.Render(canvas, true);
#endif
            return core.Zoom;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FitToWidth
        /// 
        /// <summary>
        /// ウィンドウ（描画領域）の幅に合わせて拡大/縮小を行う．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static double FitToWidth(Canvas canvas) {
            if (canvas == null) return 0.0;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return 0.0;
            var core = engine.Core;
            if (core == null) return 0.0;

            core.FitToWidth(canvas.Parent.Handle);
            core.Zoom = core.Zoom - 1; // 暫定
#if CUBE_ASYNC
            CanvasPolicy.AsyncRender(canvas, true);
#else
            CanvasPolicy.Render(canvas, true);
#endif
            return core.Zoom;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FitToPage
        /// 
        /// <summary>
        /// ウィンドウ（描画領域）に合わせて拡大/縮小を行う．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static double FitToPage(Canvas canvas) {
            if (canvas == null) return 0.0;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return 0.0;
            var core = engine.Core;
            if (core == null) return 0.0;

            // 横長ならばFitToWidthを、縦長ならばFitToHeightを呼ぶ
            if (checkPDFOrientation(canvas) == Orientation.portratit)
            {
                core.FitToHeight(canvas.Parent.Handle);
            }
            else
            {
                core.FitToWidth(canvas.Parent.Handle);
            }
            core.Zoom = core.Zoom - 1; // 暫定

#if CUBE_ASYNC
            CanvasPolicy.AsyncRender(canvas, true);
#else
            CanvasPolicy.Render(canvas, true);
#endif
            return core.Zoom;
        }
        enum Orientation
        {
            landscape,
            portratit,
        };
        private static Orientation checkPDFOrientation(Canvas canvas)
        {
            var core = ((CanvasEngine)canvas.Tag).Core;
            int rotation = core.Pages[1].Rotation;
            // 0, 90, 180, 270度以外の場合は無いであろうと仮定して、処理を省略
            double realWidth, realHeight;
            if ((rotation >= 45 && rotation < 135) || (rotation >= 225 && rotation < 315)) // 90 270度の場合
            {
                realWidth = core.Pages[1].Height;
                realHeight = core.Pages[1].Width;
            }
            else // 0, 180度の場合
            {
                realWidth = core.Pages[1].Width;
                realHeight = core.Pages[1].Height;
            }
            return (realWidth >= realHeight) ? Orientation.landscape : Orientation.portratit;
        }
       

        /* ----------------------------------------------------------------- */
        /// Search
        /* ----------------------------------------------------------------- */
        private static PDFLibNet.PDFSearchResult previousSearchResult = null;
        public static bool Search(Canvas canvas, SearchArgs args) {
            
            if (canvas == null) return false;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return false;
            var core = engine.Core;
            if (core == null) return false;

            core.SearchCaseSensitive = !args.IgnoreCase;
            var order = args.WholeDocument ? PDFLibNet.PDFSearchOrder.PDFSearchFromdBegin : PDFLibNet.PDFSearchOrder.PDFSearchFromCurrent;

            // NOTE: FindFirstで、resultに1か0しか返っていない。そのため、以前の検索結果と同じかどうかで区別する
            int result = 0;
            if (args.FromBegin) {
                previousSearchResult = null;
                result = core.FindFirst(args.Text, order, false, args.WholeWord); 
            }
            else if (args.FindNext) result = core.FindNext(args.Text);
            else result = core.FindPrevious(args.Text);
            //else result = core.FindText(args.Text, core.CurrentPage, order, !args.IgnoreCase, !args.FindNext, true, args.WholeWord);

            
            if (result > 0) {
                if (previousSearchResult != null && equalsSearchResult(previousSearchResult, core.SearchResults[0]))
                {
                    // 以前の検索結果と同じであったため、見つからなかった場合と同じ処理を行う
                    return false;
                }
                else
                {
                    core.CurrentPage = core.SearchResults[0].Page;
                    previousSearchResult = core.SearchResults[0];
                }
                CanvasPolicy.Render(canvas, false);
                engine.UpdateURL();
            }

            return result > 0;
        }

        private static bool equalsSearchResult(PDFLibNet.PDFSearchResult arg0, PDFLibNet.PDFSearchResult arg1)
        {
            return (arg0.Page == arg1.Page && arg0.Position == arg1.Position);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Adjust
        /// 
        /// <summary>
        /// 画面の位置を調整する．
        /// 
        /// MEMO: canvas.Location と canvas.Parent.AutoScrollPosition を
        /// 一端リセットする．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public static void Adjust(Canvas canvas) {
            if (canvas == null) return;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return;
            var core = engine.Core;
            if (core == null) return;

            var previous = canvas.Size;
            var parent = (ScrollableControl)canvas.Parent;
            canvas.Size = new Size(core.PageWidth, core.PageHeight);
            canvas.ClientSize = canvas.Size;

            var h = parent.HorizontalScroll.Value;
            var v = parent.VerticalScroll.Value;
            var pos = new Point(0, 0);
            var scroll = new Point(0, 0);

            // 位置情報のリセット
            canvas.Location = new Point(0, 0);
            parent.AutoScrollPosition = new Point(0, 0);

            if (parent.ClientSize.Width >= canvas.Width) pos.X = (parent.ClientSize.Width - canvas.Width) / 2;
            else scroll.X = (int)(h * canvas.Width  / (double)previous.Width);
            if (parent.ClientSize.Height >= canvas.Height) pos.Y = (parent.ClientSize.Height - canvas.Height) / 2;
            else scroll.Y = (int)(v * canvas.Height / (double)previous.Height);

            canvas.Location = pos;
            parent.AutoScrollPosition = scroll;
        }

        #region Private methods

        /* ----------------------------------------------------------------- */
        ///
        /// Render (private)
        ///
        /// <summary>
        /// MEMO: ロックは暫定処理．Microsoft によると public にアクセス
        /// 可能なオブジェクトを用いた lock は想定していないらしい．
        /// http://msdn.microsoft.com/ja-jp/library/c5kehkcz%28VS.80%29.aspx
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private static bool Render(Canvas canvas, bool adjust) {
            if (canvas == null) return false;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return false;
            var core = engine.Core;
            if (core == null) return false;
            
            lock (core) {
                var status = core.RenderPage(IntPtr.Zero, false, false);
                if (status && adjust) CanvasPolicy.Adjust(canvas);
                canvas.Invalidate();
                return status;
            }
        }

        /* ----------------------------------------------------------------- */
        /// AsyncRender (private)
        /* ----------------------------------------------------------------- */
        private static void AsyncRender(Canvas canvas, bool adjust) {
            if (canvas == null || canvas.Tag == null) return;
            var worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(DoWorkHandler);
            if (adjust) worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Adjust_WorkCompletedHandler);
            else worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(NoAdjust_WorkCompletedHandler);
            canvas.Cursor = Cursors.WaitCursor;
            worker.RunWorkerAsync(canvas);
        }

        #endregion

        #region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// PaintHandler (private)
        /// 
        /// <summary>
        /// Create() で生成された Canvas の Paint イベントハンドラ．
        /// Canvas.Tag に格納されている PDFWrapper オブジェクトを用いて，
        /// Canvas に PDF の内容を描画する．
        /// </summary> 
        /// 
        /* ----------------------------------------------------------------- */
        private static void PaintHandler(object sender, PaintEventArgs e) {
            var canvas = sender as Canvas;
            if (canvas == null) return;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return;
            var core = engine.Core;
            if (core == null) return;

            core.ClientBounds = new Rectangle(new Point(0, 0), canvas.Size);
            Graphics g = e.Graphics;
            core.DrawPageHDC(g.GetHdc());
            g.ReleaseHdc();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseMoveHandler
        /// 
        /// <summary>
        /// マウス押下中は手のひらツールとして機能する．
        /// それ以外の場合は，URL に重なった時にツールチップを表示する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private static void MouseMoveHandler(object sender, MouseEventArgs e) {
            var canvas = sender as Canvas;
            if (canvas == null) return;

            if (is_mouse_down_ && e.Button == MouseButtons.Left) {
                var control = (ScrollableControl)canvas.Parent;
                var current = canvas.PointToScreen(e.Location);
                int x = current.X - origin_.X;
                int y = current.Y - origin_.Y;
                control.AutoScrollPosition = new Point(-x, -y);
                canvas.Cursor = MainForm.HandMoveCursor;
            }
            else {
                var engine = canvas.Tag as CanvasEngine;
                if (engine == null) return;
                var core = engine.Core;
                if (core == null) return;

                lock (core) {
                    var pos = new Point((int)(e.Location.X * 72.0 / core.RenderDPI), (int)(e.Location.Y * 72.0 / core.RenderDPI));
                    var result = engine.GetURL(pos);
                    if (result != null) {
                        if (canvas.Cursor == Cursors.Default) {
                            canvas.Cursor = Cursors.Hand;
                            tooltip_.Show(result, canvas, 3000);
                        }
                    }
                    else {
                        canvas.Cursor = Cursors.Default;
                        tooltip_.Hide(canvas);
                    }
                }
            }
        }

        /* ----------------------------------------------------------------- */
        /// MouseClickHandler
        /* ----------------------------------------------------------------- */
        private static void MouseClickHandler(object sender, MouseEventArgs e) {
            var canvas = sender as Canvas;
            if (canvas == null) return;

            if (canvas.Cursor == Cursors.Hand)
            {
                var engine = canvas.Tag as CanvasEngine;
                if (engine == null) return;
                var core = engine.Core;
                if (core == null) return;

                lock (core)
                {
                    var pos = new Point((int)(e.Location.X * 72.0 / core.RenderDPI), (int)(e.Location.Y * 72.0 / core.RenderDPI));
                    var addr = engine.GetURL(pos);
                    if (addr != null) System.Diagnostics.Process.Start(addr);
                }
                canvas.Cursor = Cursors.Default;
            }
            else
            {
                // カーソルを手のひらに変更
                canvas.Cursor = MainForm.HandMoveCursor;
            }
        }

        /* ----------------------------------------------------------------- */
        /// MouseDownHandler
        /* ----------------------------------------------------------------- */
        private static void MouseDownHandler(object sender, MouseEventArgs e) {
            var canvas = sender as Canvas;
            if (canvas == null) return;
            origin_ = canvas.Parent.PointToScreen(e.Location);
            is_mouse_down_ = true;
        }

        /* ----------------------------------------------------------------- */
        /// MouseUpHandler
        /* ----------------------------------------------------------------- */
        private static void MouseUpHandler(object sender, MouseEventArgs e) {
            is_mouse_down_ = false;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseEnterHandler
        /// 
        /// <summary>
        /// MEMO: control.Focus() を実行するとスクロールバーがリセット
        /// されてしまう為，control.Parent.Focus() を実行する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private static void MouseEnterHandler(object sender, EventArgs e) {
            var control = sender as Control;
            if (control == null) return;
            control.Parent.Focus();
        }

        /* ----------------------------------------------------------------- */
        /// 
        /// DoWorkHandler
        /// 
        /// <summary>
        /// MEMO: ロックは暫定処理．Microsoft によると public にアクセス
        /// 可能なオブジェクトを用いた lock は想定していないらしい．
        /// http://msdn.microsoft.com/ja-jp/library/c5kehkcz%28VS.80%29.aspx
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private static void DoWorkHandler(object sender, DoWorkEventArgs e) {
            var worker = sender as System.ComponentModel.BackgroundWorker;
            var canvas = e.Argument as Canvas;
            if (canvas == null) return;
            var engine = canvas.Tag as CanvasEngine;
            if (engine == null) return;
            var core = engine.Core;
            if (core == null) return;

            lock (core) {
                core.RenderPage(IntPtr.Zero, false, false);
                engine.UpdateURL();
            }
            
            e.Result = canvas;
        }

        /* ----------------------------------------------------------------- */
        /// NoAdjust_WorkCompletedHandler
        /* ----------------------------------------------------------------- */
        private static void NoAdjust_WorkCompletedHandler(object sender, RunWorkerCompletedEventArgs e) {
            var canvas = e.Result as Canvas;
            if (canvas == null) return;
            canvas.Cursor = Cursors.Default;
            canvas.Invalidate();
        }

        /* ----------------------------------------------------------------- */
        /// Adjust_WorkCompletedHandler
        /* ----------------------------------------------------------------- */
        private static void Adjust_WorkCompletedHandler(object sender, RunWorkerCompletedEventArgs e) {
            var canvas = e.Result as Canvas;
            if (canvas == null) return;
            canvas.Cursor = Cursors.Default;
            CanvasPolicy.Adjust(canvas);
            canvas.Invalidate();
        }

        #endregion

        #region Variables
        private static bool is_mouse_down_ = false;
        private static Point origin_;
        private static ToolTip tooltip_ = new ToolTip();
        #endregion
    }
}
