/* ------------------------------------------------------------------------- */
/*
 *  Thumbnail.cs
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
 *  Last-modified: Thu 07 Oct 2010 23:26:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Container = System.Collections.Generic;
using PDF = PDFLibNet.PDFWrapper;

namespace Cube {
    /* --------------------------------------------------------------------- */
    /// ThumbEventArgs
    /* --------------------------------------------------------------------- */
    public class ThumbEventArgs : EventArgs {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public ThumbEventArgs(int page) : base() {
            page_ = page;
        }

        /* ----------------------------------------------------------------- */
        /// PageNum
        /* ----------------------------------------------------------------- */
        public int PageNum {
            get { return page_; }
        }

        private int page_ = 0;
    }

    /* --------------------------------------------------------------------- */
    ///
    /// ThumbEventHandler
    /// 
    /// <summary>
    /// サムネイル画像の生成が終了すると発生するイベント．
    /// 発生するタイミングは 1ページ毎で，どのページのサムネイル画像が
    /// 生成されたのかは e.PageNum で取得できる．
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public delegate void ThumbEventHandler(object sender, ThumbEventArgs e);

    /* --------------------------------------------------------------------- */
    ///
    /// ThumbEngine
    /// 
    /// <summary>
    /// サムネイル画像を実際に生成するエンジン．生成したものは，
    /// オブジェクト内でキャッシュしておく．
    /// TODO: 現状では，生成した全てのサムネイル画像をキャッシュしているが
    /// ページ数によっては莫大なメモリを消費してしまう．必要に応じて
    /// キャッシュを開放する処理を追加する．
    /// </summary>
    /// 
    /* --------------------------------------------------------------------- */
    public class ThumbEngine : IDisposable {
        /* ----------------------------------------------------------------- */
        /// ThumbEngine
        /* ----------------------------------------------------------------- */
        public ThumbEngine(PDF core, int width) {
            core_ = core;
            width_ = width;
            worker_.DoWork -= new DoWorkEventHandler(DoWorkHandler);
            worker_.DoWork += new DoWorkEventHandler(DoWorkHandler);
            worker_.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(RunCompletedHandler);
            worker_.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunCompletedHandler);
            worker_.RunWorkerAsync();
        }

        /* ----------------------------------------------------------------- */
        /// Core
        /* ----------------------------------------------------------------- */
        public PDF Core {
            get { return core_; }
            set { core_ = value; }
        }
        
        /* ----------------------------------------------------------------- */
        /// Contains
        /* ----------------------------------------------------------------- */
        public bool Contains(int pagenum) {
            lock (lock_) {
                return images_.ContainsKey(pagenum);
            }
        }

        /* ----------------------------------------------------------------- */
        /// Get
        /* ----------------------------------------------------------------- */
        public Image Get(int pagenum) {
            lock (lock_) {
                if (images_.ContainsKey(pagenum)) return images_[pagenum];
            }
            return null;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Enqueue
        /// 
        /// <summary>
        /// MEMO: 不必要なキューの削除は，本来はイベントが発生した段階
        /// でクリアするはずなのだが，うまくいっていない（lock_ を奪えてない？）
        /// 現状は，必要なさそうな数（画面に表示されるサムネイルは 5 ～ 10
        /// 程度）のキューが溜まったら順次 Dequeue() で削除している．
        /// TODO: 不必要なキューの削除処理を改善する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public void Enqueue(int pagenum) {
            lock (lock_) {
                if (!queue_.Contains(pagenum)) {
                    queue_.Enqueue(pagenum);
                    if (queue_.Count > 15) queue_.Dequeue();
                }
                if (!worker_.IsBusy) worker_.RunWorkerAsync();
            }
        }
        
        /* ----------------------------------------------------------------- */
        /// ClearQueue
        /* ----------------------------------------------------------------- */
        public void ClearQueue() {
            lock (lock_) {
                queue_.Clear();
            }
        }

        /* ----------------------------------------------------------------- */
        /// Clear
        /* ----------------------------------------------------------------- */
        public void Clear() {
            lock (lock_) {
                foreach (Image item in images_.Values) item.Dispose();
                images_.Clear();
                queue_.Clear();
            }
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
            if (!disposed_) {
                if (disposing) {
                    while (worker_.IsBusy) System.Threading.Thread.Sleep(100);
                    worker_.Dispose();
                    this.Clear();
                }
            }
            disposed_ = true;
        }

        /* ----------------------------------------------------------------- */
        /// ImageGenerated
        /* ----------------------------------------------------------------- */
        public event ThumbEventHandler ImageGenerated;
        protected virtual void OnImageGenerated(ThumbEventArgs e) {
            if (ImageGenerated != null) ImageGenerated(this, e);
        }

        /* ----------------------------------------------------------------- */
        //  内部処理
        /* ----------------------------------------------------------------- */
        #region Private methods

        /* ----------------------------------------------------------------- */
        /// DoWorkHandler (private)
        /* ----------------------------------------------------------------- */
        private void DoWorkHandler(object sender, DoWorkEventArgs e) {
            int pagenum = 0;
            lock (lock_) {
                if (queue_.Count > 0) pagenum = queue_.Dequeue();
            }
            if (pagenum > 0) this.GenerateImage(pagenum);
            e.Result = pagenum;
        }

        /* ----------------------------------------------------------------- */
        /// RunCompletedHandler (private)
        /* ----------------------------------------------------------------- */
        private void RunCompletedHandler(object sender, RunWorkerCompletedEventArgs e) {
            lock (lock_) {
                if (queue_.Count > 0) worker_.RunWorkerAsync();
            }
            
            int pagenum = (int)e.Result;
            if (pagenum > 0) {
                var args = new ThumbEventArgs(pagenum);
                this.OnImageGenerated(args);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GenerateImage (private)
        ///
        /// <summary>
        /// MEMO: ロックは暫定処理．Microsoft によると public にアクセス
        /// 可能なオブジェクトを用いた lock は想定していないらしい．
        /// http://msdn.microsoft.com/ja-jp/library/c5kehkcz%28VS.80%29.aspx
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void GenerateImage(int pagenum) {
            if (core_ == null || images_.ContainsKey(pagenum)) return;

            lock (core_) {
                PDFLibNet.PDFPage page;
                if (!core_.Pages.TryGetValue(pagenum, out page)) return;
                double ratio = page.Height / (double)page.Width;
                Image image = page.LoadThumbnail(width_, (int)(width_ * ratio));
                lock (lock_) {
                    images_.Add(pagenum, image);
                }
            }
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  メンバ変数の定義
        /* ----------------------------------------------------------------- */
        #region Member variables
        private PDF core_ = null;
        private int width_ = 0;
        private Container.Dictionary<int, Image> images_ = new Container.Dictionary<int,Image>();
        private Container.Queue<int> queue_ = new Container.Queue<int>();
        private object lock_ = new object();
        private bool disposed_ = false;
        private BackgroundWorker worker_ = new BackgroundWorker();
        #endregion
    }

    /* --------------------------------------------------------------------- */
    /// Thumbnail
    /* --------------------------------------------------------------------- */
    public class Thumbnail : System.Windows.Forms.ListView {
        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public Thumbnail(Control parent, Control src) : base() {
            this.Create(parent, src);
        }

        /* ----------------------------------------------------------------- */
        /// Engine
        /* ----------------------------------------------------------------- */
        public ThumbEngine Engine {
            get { return engine_; }
        }

        /* ----------------------------------------------------------------- */
        /// Get
        /* ----------------------------------------------------------------- */
        public static Thumbnail Get(Control parent) {
            return parent.Controls["Thumbnail"] as Thumbnail;
        }

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
            const int WM_SIZE = 0x0005;
            const int WM_VSCROLL = 0x0115;
            const int WM_LBUTTONDOWN = 0x0201;
            const int WM_LBUTTONUP = 0x0202;

            if (engine_ != null) {
                switch (m.Msg) {
                case WM_SIZE:
                    engine_.ClearQueue();
                    break;
                case WM_VSCROLL:
                    if (valid_) engine_.ClearQueue();
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
        
        /* ----------------------------------------------------------------- */
        /// Dispose
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing) {
            if (disposing) {
                var parent = this.Parent;
                if (this.Engine != null) this.Engine.Dispose();
                this.Items.Clear();
                parent.Controls.Remove(this);
            }
            base.Dispose(disposing);
        }

        /* ----------------------------------------------------------------- */
        /// Create
        /* ----------------------------------------------------------------- */
        private void Create(Control parent, Control src) {
            if (src == null || src.Tag == null) return;
            var core = src.Tag as PDF;
            
            this.Name = "Thumbnail";
            this.BackColor = Color.DimGray;
            this.Alignment = ListViewAlignment.Default;
            this.MultiSelect = false;
            this.Dock = DockStyle.Fill;
            this.OwnerDraw = true;
            this.DrawItem -= new DrawListViewItemEventHandler(DrawItemHandler);
            this.DrawItem += new DrawListViewItemEventHandler(DrawItemHandler);
            this.MouseEnter -= new EventHandler(MouseEnterHandler);
            this.MouseEnter += new EventHandler(MouseEnterHandler);

            // 水平スクロールバーが出ないサイズ．
            // 16 は垂直スクロールバーの幅（TODO: 垂直スクロールバーの幅の取得方法）．
            double ratio = core.Pages[1].Height / (double)core.Pages[1].Width;
            int width = parent.ClientSize.Width;
            if (width * ratio * core.PageCount > parent.Size.Height) width -= 20;
            width -= 3; // NOTE: 余白を持たせる．手動で微調整したもの

            engine_ = new ThumbEngine(core, width - 10);
            engine_.ImageGenerated -= new ThumbEventHandler(ImageGeneratedHandler);
            engine_.ImageGenerated += new ThumbEventHandler(ImageGeneratedHandler);

            this.View = View.Tile;
            this.TileSize = new Size(width, (int)(width * ratio));
            this.Clear();
            for (int i = 0; i < core.PageCount; i++) this.Items.Add((i + 1).ToString());

            parent.Controls.Add(this);
        }

        /* ----------------------------------------------------------------- */
        //  イベントハンドラの定義
        /* ----------------------------------------------------------------- */
        #region Event handlers
        /* ----------------------------------------------------------------- */
        ///
        /// DrawItemHandler (private)
        /// 
        /// <summary>
        /// サムネイルの DrawItem イベントハンドラ．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void DrawItemHandler(object sender, DrawListViewItemEventArgs e) {
            var engine = this.Engine;
            if (engine == null) return;

            Rectangle rect = new Rectangle(e.Bounds.Location, e.Bounds.Size);
            rect.Inflate(-5, -5);
            var image = engine.Get(e.ItemIndex + 1);
            if (image != null) {
                e.Graphics.DrawImage(image, rect);
            }
            else {
                engine.Enqueue(e.ItemIndex + 1);

                // 生成されてないページは真っ白な画像を表示する．
                var brush = new SolidBrush(Color.White);
                e.Graphics.FillRectangle(brush, rect);
                brush.Dispose();
            }
            e.Graphics.DrawRectangle(Pens.LightGray, rect);

            // MEMO: キャプションを描画する方法．
            // var stringFormat = new StringFormat();
            // stringFormat.Alignment = StringAlignment.Center;
            // stringFormat.LineAlignment = StringAlignment.Center;
            // e.Graphics.DrawString(e.Item.Text, canvas.Font, Brushes.Black, new RectangleF(e.Bounds.X, e.Bounds.Y + e.Bounds.Height - 10, e.Bounds.Width, 10), stringFormat);

            if (e.ItemIndex == engine.Core.CurrentPage - 1) {
                var pen = new Pen(Color.FromArgb(255, 50, 0));
                pen.Width = 2;
                e.Graphics.DrawRectangle(pen, rect);
                pen.Dispose();
            }
        }

        /* ----------------------------------------------------------------- */
        /// MouseEnterHandler
        /* ----------------------------------------------------------------- */
        private void MouseEnterHandler(object sender, EventArgs e) {
            this.Focus();
        }

        /* ----------------------------------------------------------------- */
        /// ImageGeneratedHandler
        /* ----------------------------------------------------------------- */
        private void ImageGeneratedHandler(object sender, ThumbEventArgs e) {
            if (e.PageNum <= 0 && e.PageNum >= this.Items.Count) return;
            this.Invalidate(this.Items[e.PageNum - 1].Bounds);
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  メンバ変数の定義
        /* ----------------------------------------------------------------- */
        #region Member variables
        private bool valid_ = false;
        private ThumbEngine engine_ = null;
        #endregion
    }
}
