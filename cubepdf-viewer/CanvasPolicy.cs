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
using PDF = PDFLibNet.PDFWrapper;

namespace Cube {
    /* --------------------------------------------------------------------- */
    /// FitCondition
    /* --------------------------------------------------------------------- */
    public enum FitCondition {
        None = 0x00, Width = 0x01, Height = 0x02
    }

    /* --------------------------------------------------------------------- */
    /// Thumbnail
    /* --------------------------------------------------------------------- */
    public class Thumbnail : System.Windows.Forms.ListView {
        /* ----------------------------------------------------------------- */
        ///
        /// WndProc
        /// 
        /// <summary>
        /// サムネイル画像を生成するためのキューを特定のイベントが発生した
        /// 際にキャンセルする．
        /// 
        /// NOTE: LargeChange によるスクロールが発生した場合，必要な
        /// 画像まで生成がキャンセルされている模様．現在は，MouseDown
        /// イベントが発生した直後の Scroll イベント時にのみキャンセル
        /// している．キャンセルのタイミングについては，もう少し検討する
        /// 必要がある．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        protected override void WndProc(ref Message m) {
            const int WM_SIZE        = 0x0005;
            const int WM_VSCROLL     = 0x0115;
            const int WM_LBUTTONDOWN = 0x0201;
            const int WM_LBUTTONUP   = 0x0202;

            var core = (PDF)this.Tag;
            if (core != null) {
                switch (m.Msg) {
                case WM_SIZE:
                    core.CancelThumbProcess();
                    break;
                case WM_VSCROLL:
                    if (valid_) core.CancelThumbProcess();
                    break;
                case WM_LBUTTONDOWN:
                    valid_ = true;
                    break;
                case WM_LBUTTONUP:
                    valid_ = false;
                    break;
                default:
                    break;
                }
            }
            base.WndProc(ref m);
        }

        private bool valid_ = false;
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
                canvas.Paint += new PaintEventHandler(CanvasPolicy.PaintHandler);
                canvas.MouseDown += new MouseEventHandler(CanvasPolicy.MouseDownHandler);
                canvas.MouseUp += new MouseEventHandler(CanvasPolicy.MouseUpHandler);
                canvas.MouseMove += new MouseEventHandler(CanvasPolicy.MouseMoveHandler);
                canvas.MouseEnter += new EventHandler(CanvasPolicy.MouseEnterHandler);

                parent.Controls.Add(canvas);
                parent.MouseEnter += new EventHandler(CanvasPolicy.MouseEnterHandler);
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
                if (which == FitCondition.Height) CanvasPolicy.FitToHeight(canvas);
                else if (which == FitCondition.Width) CanvasPolicy.FitToWidth(canvas);
                else core.Zoom = 100;
                core.RenderPage(IntPtr.Zero, false, false);
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
        /// PageSize
        /* ----------------------------------------------------------------- */
        public static Size PageSize(Canvas canvas) {
            if (canvas == null || canvas.Tag == null) return new Size(0, 0);

            var core = (PDF)canvas.Tag;
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
            if (core.RenderPage(IntPtr.Zero, false, false)) {
                var control = (ScrollableControl)canvas.Parent;
                control.AutoScrollPosition = new Point(0, 0);
            }
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
            if (core.RenderPage(IntPtr.Zero, false, false)) {
                var control = (ScrollableControl)canvas.Parent;
                control.AutoScrollPosition = new Point(0, 0);
            }
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
            if (core.RenderPage(IntPtr.Zero, false, false)) {
                var control = (ScrollableControl)canvas.Parent;
                control.AutoScrollPosition = new Point(0, 0);
            }
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
            if (core.RenderPage(IntPtr.Zero, false, false)) {
                var control = (ScrollableControl)canvas.Parent;
                control.AutoScrollPosition = new Point(0, 0);
            }
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
            if (core.RenderPage(IntPtr.Zero, false, false)) {
                var control = (ScrollableControl)canvas.Parent;
                control.AutoScrollPosition = new Point(0, 0);
            }
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
            var prev = canvas.Size;
            core.Zoom = percent;
            core.RenderPage(IntPtr.Zero, false, false);

            CanvasPolicy.Adjust(canvas, prev);
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
            var prev = canvas.Size;
            core.ZoomIN();
            core.RenderPage(IntPtr.Zero, false, false);

            CanvasPolicy.Adjust(canvas, prev);
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
            var prev = canvas.Size;
            core.ZoomOut();
            core.RenderPage(IntPtr.Zero, false, false);

            CanvasPolicy.Adjust(canvas, prev);
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
            var prev = canvas.Size;
            core.FitToWidth(canvas.Parent.Handle);
            core.Zoom = core.Zoom - 1; // 暫定
            core.RenderPage(IntPtr.Zero, false, false);

            CanvasPolicy.Adjust(canvas, prev);
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
            var prev = canvas.Size;
            core.FitToHeight(canvas.Parent.Handle);
            core.Zoom = core.Zoom - 1; // 暫定
            core.RenderPage(IntPtr.Zero, false, false);

            CanvasPolicy.Adjust(canvas, prev);
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
                core.RenderPage(IntPtr.Zero, false, false);
            }

            return result > 0;
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
        public static void Adjust(Canvas canvas, Size previous) {
            if (canvas == null || canvas.Tag == null) return;

            var core = (PDF)canvas.Tag;
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

        /* ----------------------------------------------------------------- */
        ///
        /// Adjust
        /// 
        /// <summary>
        /// 画面の位置を調整する．このメソッドは，画面の位置を調整する前と
        /// canvas のサイズに変更がない場合に使用する．canvas のサイズに
        /// 変更があった場合は，変更前の canvas のサイズを第2引数に指定
        /// する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public static void Adjust(Canvas canvas) {
            CanvasPolicy.Adjust(canvas, CanvasPolicy.PageSize(canvas));
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
        /// 
        /// MEMO: サイズが変わった際にスクロールバーの表示/非表示が変わると
        /// 見た目がおかしくなる．
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
            canvas.MouseEnter += new EventHandler(CanvasPolicy.MouseEnterHandler);

            // 水平スクロールバーが出ないサイズ．
            // 16 は垂直スクロールバーの幅（TODO: 垂直スクロールバーの幅の取得方法）．
            double ratio = core.Pages[1].Height / (double)core.Pages[1].Width;
            int width = parent.ClientSize.Width;
            if (width * ratio * core.PageCount > parent.Size.Height) width -= 20;
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
        /// DestroyThumbnail
        /* ----------------------------------------------------------------- */
        public static void DestroyThumbnail(Thumbnail canvas) {
            var parent = canvas.Parent;
            canvas.Items.Clear();
            canvas.Tag = null;
            parent.Controls.Remove(canvas);
        }

        #region Private methods

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

                if (e.ItemIndex == core.CurrentPage - 1) {
                    var pen = new Pen(Color.FromArgb(255, 50, 0));
                    pen.Width = 2;
                    e.Graphics.DrawRectangle(pen, rect);
                    pen.Dispose();
                }
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

            if (is_mouse_down_ && e.Button == MouseButtons.Left) {
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
        /// MouseUpHandler
        /* ----------------------------------------------------------------- */
        private static void MouseUpHandler(object sender, MouseEventArgs e) {
            is_mouse_down_ = false;
        }

        /* ----------------------------------------------------------------- */
        /// MouseEnterHandler
        /* ----------------------------------------------------------------- */
        private static void MouseEnterHandler(object sender, EventArgs e) {
            var control = (Control)sender;
            control.Focus();
        }

        #endregion

        #region Variables
        private static bool is_mouse_down_ = false;
        private static Point origin_;
        private static Color background_ = Color.DimGray;
        #endregion
    }
}
