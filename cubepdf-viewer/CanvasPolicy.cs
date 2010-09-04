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
 *  Last-modified: Wed 01 Sep 2010 00:10:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Drawing;
using System.Windows.Forms;
using Container = System.Collections.Generic;
using Canvas = System.Windows.Forms.PictureBox;
using Thumbnail = System.Windows.Forms.ListView;
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
    /// CanvasPolicy
    /// 
    /// <summary>
    /// PDFWrapper を用いて画面に PDF を内容を表示するための制御関数群
    /// をまとめた抽象クラス．Canvas.Tag に PDFWrapper オブジェクト
    /// を設定し，このオブジェクトが保持する情報を基にして描画を行う．
    /// 現在の実装では，Canvas として PictureBox を利用している．
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
                canvas.BackColor = background_;
                canvas.Size = parent.ClientSize;
                canvas.ClientSize = canvas.Size;
                //canvas.SizeMode = PictureBoxSizeMode.AutoSize;

                // イベントハンドラの登録
                canvas.Paint += new PaintEventHandler(PaintHandler);
                canvas.MouseDown += new MouseEventHandler(MouseDownHandler);
                canvas.MouseUp += new MouseEventHandler(MouseUpHandler);
                canvas.MouseMove += new MouseEventHandler(MouseMoveHandler);

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
        public static void Open(Canvas canvas, string path, string password, FitCondition which = FitCondition.None) {
            if (canvas == null) return;

            var core = (PDF)canvas.Tag;
            if (core != null) core.Dispose();
            core = new PDF();
            core.UseMuPDF = true;
            canvas.Tag = core;
            core.UserPassword = password;
            core.OwnerPassword = password;

            if (core.LoadPDF(path)) {
                core.CurrentPage = 1;
                if (which == FitCondition.Height) core.FitToHeight(canvas.Parent.Handle);
                else if (which == FitCondition.Width) core.FitToWidth(canvas.Parent.Handle);
                else core.Zoom = 100;
                core.RenderPage(IntPtr.Zero);
                canvas.Parent.Text = System.IO.Path.GetFileNameWithoutExtension(path);
                canvas.Parent.Tag = path;
                CanvasPolicy.Adjust(canvas);
            }
        }

        /* ----------------------------------------------------------------- */
        /// Open
        /* ----------------------------------------------------------------- */
        public static void Open(Canvas canvas, string path, FitCondition which = FitCondition.None) {
            CanvasPolicy.Open(canvas, path, "", which);
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

            var core = (PDF)canvas.Tag;
            core.Dispose();
            canvas.Tag = null;

            var parent = (ScrollableControl)canvas.Parent;
            parent.Tag = null;
            parent.Text = "(無題)";
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
            if (canvas == null || canvas.Tag == null) return 0;

            var core = (PDF)canvas.Tag;
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
            if (canvas == null || canvas.Tag == null) return 0;

            var core = (PDF)canvas.Tag;
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
            if (canvas == null || canvas.Tag == null) return 0;

            var core = (PDF)canvas.Tag;
            int n = Math.Min(Math.Max(page, 1), core.PageCount);
            core.CurrentPage = n;
            core.RenderPage(IntPtr.Zero);
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
            if (canvas == null || canvas.Tag == null) return 0;

            var core = (PDF)canvas.Tag;
            core.NextPage();
            core.RenderPage(IntPtr.Zero);
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
            if (canvas == null || canvas.Tag == null) return 0;

            var core = (PDF)canvas.Tag;
            core.PreviousPage();
            core.RenderPage(IntPtr.Zero);
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
            if (canvas == null || canvas.Tag == null) return 0;

            var core = (PDF)canvas.Tag;
            core.CurrentPage = 1;
            core.RenderPage(IntPtr.Zero);
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
            if (canvas == null || canvas.Tag == null) return 0;

            var core = (PDF)canvas.Tag;
            core.CurrentPage = core.PageCount;
            core.RenderPage(IntPtr.Zero);
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
            if (canvas == null || canvas.Tag == null) return 0.0;

            var core = (PDF)canvas.Tag;
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
            if (canvas == null || canvas.Tag == null) return 0.0;

            var core = (PDF)canvas.Tag;
            var prev = new Size(core.PageWidth, core.PageHeight);
            core.Zoom = percent;
            core.RenderPage(IntPtr.Zero);

            CanvasPolicy.ResetPosition(canvas, core, prev);
            CanvasPolicy.Adjust(canvas);
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
            if (canvas == null || canvas.Tag == null) return 0.0;

            var core = (PDF)canvas.Tag;
            var prev = new Size(core.PageWidth, core.PageHeight);
            core.ZoomIN();
            core.RenderPage(IntPtr.Zero);

            CanvasPolicy.ResetPosition(canvas, core, prev);
            CanvasPolicy.Adjust(canvas);
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
            if (canvas == null || canvas.Tag == null) return 0.0;

            var core = (PDF)canvas.Tag;
            core.ZoomOut();
            core.RenderPage(IntPtr.Zero);

            CanvasPolicy.Adjust(canvas);
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
            if (canvas == null || canvas.Tag == null) return 0.0;

            var core = (PDF)canvas.Tag;
            var prev = new Size(core.PageWidth, core.PageHeight);
            core.FitToWidth(canvas.Parent.Handle);
            core.RenderPage(IntPtr.Zero);

            CanvasPolicy.ResetPosition(canvas, core, prev);
            CanvasPolicy.Adjust(canvas);
            return core.Zoom;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// FitToHeight
        /// 
        /// <summary>
        /// ウィンドウ（描画領域）の高さに合わせて拡大/縮小を行う．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static double FitToHeight(Canvas canvas) {
            if (canvas == null || canvas.Tag == null) return 0.0;

            var core = (PDF)canvas.Tag;
            var prev = new Size(core.PageWidth, core.PageHeight);
            var zoom = core.Zoom;
            core.FitToHeight(canvas.Parent.Handle);
            core.RenderPage(IntPtr.Zero);

            if (core.Zoom > zoom) CanvasPolicy.ResetPosition(canvas, core, prev);
            CanvasPolicy.Adjust(canvas);
            return core.Zoom;
        }

        /* ----------------------------------------------------------------- */
        /// Search
        /* ----------------------------------------------------------------- */
        public static bool Search(Canvas canvas, SearchArgs args) {
            if (canvas == null || canvas.Tag == null) return false;

            var core = (PDF)canvas.Tag;
            core.SearchCaseSensitive = !args.IgnoreCase;
            var order = args.WholeDocument ? PDFLibNet.PDFSearchOrder.PDFSearchFromdBegin : PDFLibNet.PDFSearchOrder.PDFSearchFromCurrent;

            int result = 0;
            if (args.FromBegin) result = core.FindFirst(args.Text, order, false, args.WholeWord);
            else if (args.FindNext) result = core.FindNext(args.Text);
            else result = core.FindPrevious(args.Text);
            //else result = core.FindText(args.Text, core.CurrentPage, order, !args.IgnoreCase, !args.FindNext, true, args.WholeWord);

            if (result > 0) {
                core.CurrentPage = core.SearchResults[0].Page;
                core.RenderPage(IntPtr.Zero);
            }

            return result > 0;
        }

        /* ----------------------------------------------------------------- */
        /// Adjust
        /* ----------------------------------------------------------------- */
        public static void Adjust(Canvas canvas) {
            if (canvas == null || canvas.Tag == null) return;

            var core = (PDF)canvas.Tag;
            var parent = canvas.Parent;
            canvas.Size = new Size(core.PageWidth, core.PageHeight);
            canvas.ClientSize = canvas.Size;

            var pos = canvas.Location;
            if (parent.ClientSize.Width > canvas.Width) pos.X = (parent.ClientSize.Width - canvas.Width) / 2;
            if (parent.ClientSize.Height > canvas.Height) pos.Y = (parent.ClientSize.Height - canvas.Height) / 2;
            canvas.Location = pos;
        }

        /* ----------------------------------------------------------------- */
        /// GetThumbnail
        /* ----------------------------------------------------------------- */
        public static Thumbnail GetThumbnail(Control parent) {
            return (Thumbnail)parent.Controls["Thumbnail"];
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateThumbnail
        /// 
        /// <summary>
        /// src に指定された画面に表示されている PDF ファイルのサムネイルを
        /// dest に指定された画面に表示する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static Thumbnail CreateThumbnail(Canvas src, Control parent) {
            var core = (PDF)src.Tag;
            if (core == null) return null;

            var canvas = new Thumbnail();
            parent.Controls.Add(canvas);

            canvas.Name = "Thumbnail";
            canvas.Tag = core;
            canvas.BackColor = background_;
            canvas.Alignment = ListViewAlignment.Default;
            canvas.MultiSelect = false;
            canvas.Dock = DockStyle.Fill;
            canvas.OwnerDraw = true;
            canvas.DrawItem -= new DrawListViewItemEventHandler(CanvasPolicy.DrawItemHandler);
            canvas.DrawItem += new DrawListViewItemEventHandler(CanvasPolicy.DrawItemHandler);

            // 水平スクロールバーが出ないサイズ．
            // 16 は垂直スクロールバーの幅（TODO: 垂直スクロールバーの幅の取得方法）．
            double ratio = core.Pages[1].Height / (double)core.Pages[1].Width;
            int width = parent.ClientSize.Width;
            if (width * ratio * core.PageCount > parent.Size.Height) width -= 16;
            width -= 3; // NOTE: 余白を持たせる．手動で微調整したもの

            canvas.View = View.Tile;
            canvas.TileSize = new Size(width, (int)(width * ratio));
            canvas.BeginUpdate();
            canvas.Clear();
            for (int i = 0; i < core.PageCount; i++) canvas.Items.Add((i + 1).ToString());
            canvas.EndUpdate();
            
            return canvas;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateThumbnail
        /// 
        /// <summary>
        /// サムネイル画像の描画が終了した際のイベントハンドラを指定する場合．
        /// DrawThumbnail では，最初に四角だけを描画して内容は遅延して描画
        /// されるので，イベントハンドラを指定されない場合，ユーザが何らか
        /// のアクションを起こして再描画が起こるまで内容が描画されない．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static Thumbnail CreateThumbnail(Canvas src, Control parent, PDFLibNet.RenderNotifyFinishedHandler finished) {
            thumb_finished_ = finished;
            return CanvasPolicy.CreateThumbnail(src, parent);
        }

        /* ----------------------------------------------------------------- */
        /// DestroyThumbnail
        /* ----------------------------------------------------------------- */
        public static void DestroyThumbnail(Thumbnail canvas) {
            var parent = canvas.Parent;
            canvas.Items.Clear();
            canvas.Tag = null;
            parent.Controls.Remove(canvas);
            thumb_finished_ = null;
        }

        #region Private methods
        /* ----------------------------------------------------------------- */
        ///
        /// ResetPosition (private)
        /// 
        /// <summary>
        /// 現在，描画しているページ内容の幅/高さがコントロールパネルより
        /// も小さい（スクロールバーが表示されず余白が存在する）場合，
        /// センタリングしている．このセンタリングのために位置をずらして
        /// いる部分が，拡大した際にスクロールバーが表示されるような大きさ
        /// になると不正な挙動を示す．
        /// 
        /// 拡大した結果，余白がなくなった場合に位置情報をリセットする．
        /// 
        /// NOTE: 2010/09/03 強制的にリセットに変更
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static void ResetPosition(Canvas canvas, PDF core, Size prev) {
            canvas.Location = new Point(0, 0);
        }

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
            var canvas = (Canvas)sender;
            if (canvas.Tag == null) return;

            var core = (PDF)canvas.Tag;
            core.ClientBounds = new Rectangle(new Point(0, 0), canvas.Size);
            Graphics g = e.Graphics;
            core.DrawPageHDC(g.GetHdc());
            g.ReleaseHdc();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// DrawItemHandler (private)
        /// 
        /// <summary>
        /// サムネイルの DrawItem イベントハンドラ．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private static void DrawItemHandler(object sender, DrawListViewItemEventArgs e) {
            lock (sender) {
                var canvas = (Thumbnail)sender;
                var core = (PDFLibNet.PDFWrapper)canvas.Tag;
                if (core == null) return;
                while (core.IsBusy) System.Threading.Thread.Sleep(50);

                PDFLibNet.PDFPage page;
                if (!core.Pages.TryGetValue(e.ItemIndex + 1, out page)) return;

                if (thumb_finished_ != null) {
                    page.RenderThumbnailFinished -= thumb_finished_;
                    page.RenderThumbnailFinished += thumb_finished_;
                }

                Rectangle rect = new Rectangle(e.Bounds.Location, e.Bounds.Size);
                rect.Inflate(-5, -5);
                int width = canvas.TileSize.Width - 10;
                double ratio = page.Height / (double)page.Width;
                Image image = page.LoadThumbnail(width, (int)(width * ratio));
                if (image != null) e.Graphics.DrawImageUnscaledAndClipped(image, rect);
                e.Graphics.DrawRectangle(Pens.LightGray, rect);

                // MEMO: キャプションを描画する方法．
                // var stringFormat = new StringFormat();
                // stringFormat.Alignment = StringAlignment.Center;
                // stringFormat.LineAlignment = StringAlignment.Center;
                // e.Graphics.DrawString(e.Item.Text, canvas.Font, Brushes.Black, new RectangleF(e.Bounds.X, e.Bounds.Y + e.Bounds.Height - 10, e.Bounds.Width, 10), stringFormat);

                e.DrawFocusRectangle();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MouseMoveHandler
        /// 
        /// <summary>
        /// 手のひらツールの実装．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private static void MouseMoveHandler(object sender, MouseEventArgs e) {
            var canvas = (Canvas)sender;

            if (is_mouse_down_) {
                var control = (ScrollableControl)canvas.Parent;
                var current = canvas.PointToScreen(e.Location);
                int x = current.X - origin_.X;
                int y = current.Y - origin_.Y;
                control.AutoScrollPosition = new Point(-x, -y);
            }
        }

        /* ----------------------------------------------------------------- */
        /// MouseDownHandler
        /* ----------------------------------------------------------------- */
        private static void MouseDownHandler(object sender, MouseEventArgs e) {
            var canvas = (Canvas)sender;
            origin_ = canvas.Parent.PointToScreen(e.Location);
            is_mouse_down_ = true;
        }

        /* ----------------------------------------------------------------- */
        /// MouseDownHandler
        /* ----------------------------------------------------------------- */
        private static void MouseUpHandler(object sender, MouseEventArgs e) {
            is_mouse_down_ = false;
        }
        #endregion

        #region Variables
        private static bool is_mouse_down_ = false;
        private static Point origin_;
        private static PDFLibNet.RenderNotifyFinishedHandler thumb_finished_ = null;
        private static Color background_ = Color.DimGray;
        #endregion
    }
}
