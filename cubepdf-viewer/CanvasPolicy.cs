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
                canvas.BackColor = Color.DimGray;
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
            parent.Text = "(無題)";
        }
        
        /* ----------------------------------------------------------------- */
        /// Open
        /* ----------------------------------------------------------------- */
        public static void Open(Canvas canvas, string path, FitCondition which = FitCondition.Height) {
            if (canvas == null) return;

            var core = (PDF)canvas.Tag;
            if (core != null) core.Dispose();
            core = new PDF();
            core.UseMuPDF = true;
            canvas.Tag = core;

            if (core.LoadPDF(path)) {
                core.CurrentPage = 1;
                if (which == FitCondition.Height) core.FitToHeight(canvas.Parent.Handle);
                else if (which == FitCondition.Width) core.FitToWidth(canvas.Parent.Handle);
                else core.Zoom = 100;
                core.RenderPage(IntPtr.Zero);
                canvas.Parent.Text = System.IO.Path.GetFileNameWithoutExtension(path);
                CanvasPolicy.Adjust(canvas);
            }
        }

        /* ----------------------------------------------------------------- */
        /// Close
        /* ----------------------------------------------------------------- */
        public static void Close(Canvas canvas) {
            if (canvas == null || canvas.Tag == null) return;

            var core = (PDF)canvas.Tag;
            core.Dispose();
            canvas.Tag = null;
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
        public static void MovePage(Canvas canvas, int page) {
            if (canvas == null || canvas.Tag == null) return;

            var core = (PDF)canvas.Tag;
            int n = Math.Min(Math.Max(page, 1), core.PageCount);
            core.CurrentPage = n;
            core.RenderPage(IntPtr.Zero);
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
        public static void NextPage(Canvas canvas) {
            if (canvas == null || canvas.Tag == null) return;

            var core = (PDF)canvas.Tag;
            core.NextPage();
            core.RenderPage(IntPtr.Zero);
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
        public static void PreviousPage(Canvas canvas) {
            if (canvas == null || canvas.Tag == null) return;

            var core = (PDF)canvas.Tag;
            core.PreviousPage();
            core.RenderPage(IntPtr.Zero);
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
        public static void FirstPage(Canvas canvas) {
            if (canvas == null || canvas.Tag == null) return;

            var core = (PDF)canvas.Tag;
            core.CurrentPage = 1;
            core.RenderPage(IntPtr.Zero);
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
        public static void LastPage(Canvas canvas) {
            if (canvas == null || canvas.Tag == null) return;

            var core = (PDF)canvas.Tag;
            core.CurrentPage = core.PageCount;
            core.RenderPage(IntPtr.Zero);
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
        public static void Zoom(Canvas canvas, int percent) {
            if (canvas == null || canvas.Tag == null) return;

            var core = (PDF)canvas.Tag;
            var prev = new Size(core.PageWidth, core.PageHeight);
            core.Zoom = (double)percent;
            core.RenderPage(IntPtr.Zero);

            var view = canvas.Parent.ClientSize;
            var pos = canvas.Location;
            if (prev.Width < view.Width && core.PageWidth >= view.Width) pos.X = 0;
            if (prev.Height < view.Height && core.PageHeight >= view.Height) pos.Y = 0;
            canvas.Location = pos;

            CanvasPolicy.Adjust(canvas);
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
        public static void ZoomIn(Canvas canvas) {
            if (canvas == null || canvas.Tag == null) return;

            var core = (PDF)canvas.Tag;
            var prev = new Size(core.PageWidth, core.PageHeight);
            core.ZoomIN();
            core.RenderPage(IntPtr.Zero);

            CanvasPolicy.ResetPosition(canvas, core, prev);
            CanvasPolicy.Adjust(canvas);
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
        public static void ZoomOut(Canvas canvas) {
            if (canvas == null || canvas.Tag == null) return;

            var core = (PDF)canvas.Tag;
            core.ZoomOut();
            core.RenderPage(IntPtr.Zero);

            CanvasPolicy.Adjust(canvas);
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
        public static void FitToWidth(Canvas canvas) {
            if (canvas == null || canvas.Tag == null) return;

            var core = (PDF)canvas.Tag;
            var prev = new Size(core.PageWidth, core.PageHeight);
            var zoom = core.Zoom;
            core.FitToWidth(canvas.Parent.Handle);
            core.RenderPage(IntPtr.Zero);

            if (core.Zoom > zoom) CanvasPolicy.ResetPosition(canvas, core, prev);
            CanvasPolicy.Adjust(canvas);
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
        public static void FitToHeight(Canvas canvas) {
            if (canvas == null || canvas.Tag == null) return;

            var core = (PDF)canvas.Tag;
            var prev = new Size(core.PageWidth, core.PageHeight);
            var zoom = core.Zoom;
            core.FitToHeight(canvas.Parent.Handle);
            core.RenderPage(IntPtr.Zero);

            if (core.Zoom > zoom) CanvasPolicy.ResetPosition(canvas, core, prev);
            CanvasPolicy.Adjust(canvas);
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
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private static void ResetPosition(Canvas canvas, PDF core, Size prev) {
            var view = canvas.Parent.ClientSize;
            var pos = canvas.Location;
            if (prev.Width <= view.Width && core.PageWidth > view.Width) pos.X = 0;
            if (prev.Height <= view.Height && core.PageHeight > view.Height) pos.Y = 0;
            canvas.Location = pos;
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
        #endregion
    }
}
