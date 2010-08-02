/* ------------------------------------------------------------------------- */
/*
 *  MainForm.cs
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
 *  Last-modified: Mon 02 Aug 2010 11:52:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cube {
    /* --------------------------------------------------------------------- */
    //  MainForm
    /* --------------------------------------------------------------------- */
    public partial class MainForm : Form {
        /* ----------------------------------------------------------------- */
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /* ----------------------------------------------------------------- */
        public MainForm() {
            InitializeComponent();
            InitializeMainViewer();
            InitializeLibrary();

            this.MouseEnter += new System.EventHandler(this.MainForm_MouseEnter);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseWheel);
            this.KeyPreview = true;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ReDraw
        /// 
        /// <summary>
        /// 再描画処理
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void ReDraw(string status = "Ready") {
            doc_.RenderPage(MainViewer.Handle);
            MainViewer.PageSize = new Size(doc_.PageWidth, doc_.PageHeight);

            // メニューバーの各種情報の更新．
            MenuCurrentPage.Text = doc_.CurrentPage.ToString();
            MenuTotalPage.Text = "/ " + doc_.PageCount.ToString();
            MenuZoomText.Text = ((int)(doc_.Zoom)).ToString() + "%";

            // ステータスバーの各種情報の更新
            StatusText.Text = status;

            this.Refresh();
        }

        /* ----------------------------------------------------------------- */
        /// NextPage
        /* ----------------------------------------------------------------- */
        private bool NextPage(object sender, EventArgs e) {
            if (doc_ == null) return false;
            if (doc_.CurrentPage < doc_.PageCount) {
                doc_.NextPage();
                this.ReDraw();
                return true;
            }
            return false;
        }

        /* ----------------------------------------------------------------- */
        /// PreviousPage
        /* ----------------------------------------------------------------- */
        private bool PreviousPage(object sender, EventArgs e) {
            if (doc_ == null) return false;
            if (doc_.CurrentPage > 1) {
                doc_.PreviousPage();
                this.ReDraw();
                return true;
            }
            return false;
        }

        /* ----------------------------------------------------------------- */
        /// Search
        /* ----------------------------------------------------------------- */
        private void Search(object sender, SearchArgs e) {
            if (doc_ == null) return;

            int result = 0;
            doc_.SearchCaseSensitive = !e.IgnoreCase;

            if (e.FromBegin) {
                result = doc_.FindFirst(
                    e.Text,
                    e.WholeDocument ? PDFLibNet.PDFSearchOrder.PDFSearchFromdBegin : PDFLibNet.PDFSearchOrder.PDFSearchFromCurrent,
                    !e.FindNext,
                    e.WholeWord
                );
            }
            else if (e.FindNext) {
                if (e.FindNext) result = doc_.FindNext(e.Text);
                else result = doc_.FindPrevious(e.Text);
            }
            else {
                result = doc_.FindText(
                    e.Text,
                    doc_.CurrentPage,
                    e.WholeDocument ? PDFLibNet.PDFSearchOrder.PDFSearchFromdBegin : PDFLibNet.PDFSearchOrder.PDFSearchFromCurrent,
                    !e.IgnoreCase,
                    !e.FindNext,
                    true,
                    e.WholeWord
                );
            }

            if (result > 0) {
                doc_.CurrentPage = doc_.SearchResults[0].Page;
                // FocusSearchResult(_pdfDoc.SearchResults[0]);
                this.ReDraw();
            }
            else {
                Console.Beep();
                from_begin_ = true;
                this.ReDraw(Properties.Settings.Default.STATUS_EOS);
            }
           
        }

        /* ----------------------------------------------------------------- */
        /// Search
        /* ----------------------------------------------------------------- */
        private void Search(object sender, bool vector) {
            try {
                var query = new SearchArgs();
                query.Text = MenuSearchText.Text;
                query.FromBegin = from_begin_;
                query.IgnoreCase = true;
                query.WholeDocument = true;
                query.WholeWord = false;
                query.FindNext = vector;
                from_begin_ = false;

                this.Search(sender, query);
            }
            catch (Exception /* err */) { }
        }

        /* ----------------------------------------------------------------- */
        //  各種初期化処理
        /* ----------------------------------------------------------------- */
#region Initialize methods

        /* ----------------------------------------------------------------- */
        /// InitializeLibrary
        /* ----------------------------------------------------------------- */
        private void InitializeLibrary() {
            PDFLibNet.xPDFParams.Antialias = true;
            PDFLibNet.xPDFParams.VectorAntialias = true;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// InitializeMainViewer
        /// 
        /// <summary>
        /// PageViewer クラスの初期化処理
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void InitializeMainViewer() {
            this.MainViewer.PaintControl += new PDFViewer.PageViewer.PaintControlHandler(this.DoubleBuffer_PaintControl);

            // 使用する描画ライブラリが xPDFlib の場合の初期設定
            /*
            if (ConfigurationManager.AppSettings.Get("xpdfrc") == "xpdfrc") {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove("xpdfrc");
                config.AppSettings.Settings.Add("xpdfrc", AppDomain.CurrentDomain.BaseDirectory + "xpdfrc");
                ConfigurationManager.RefreshSection("appSettings");
            }
            */

            int width = this.MainViewer.Width - this.MainViewer.Margin.Left - this.MainViewer.Margin.Left;
            this.MainViewer.PageSize = new Size(width, (int)(width * 11 / 8.5));
            this.MainViewer.Visible = true;
        }
#endregion
        
        /* ----------------------------------------------------------------- */
        //  各種イベント・ハンドラ
        /* ----------------------------------------------------------------- */
#region Event handlers

        /* ----------------------------------------------------------------- */
        ///
        /// MainForm_KeyDown
        /// 
        /// <summary>
        /// 各種ショートカットキーの設定．
        /// TODO: 前方向検索がうまくいかない．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void MainForm_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control) {
                if (e.KeyCode == Keys.O) this.MenuOpen_Click(sender, (EventArgs)e);
                else if (e.KeyCode == Keys.F) MenuSearchText.Focus();
            }
            else if (e.Shift) {
                //if (e.KeyCode == Keys.F3 && MenuSearchText.Text != "") this.Search(sender, false);
            }
            else {
                if (e.KeyCode == Keys.F3 && MenuSearchText.Text != "") this.Search(sender, true);
            }
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// MenuOpen_Click
        /// 
        /// <summary>
        /// ToolStrip->MenuOpen の Click イベントハンドラ
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void MenuOpen_Click(object sender, EventArgs e) {
            var dialog = new OpenFileDialog();
            dialog.Filter = Properties.Settings.Default.FILTER_PDF;

            if (dialog.ShowDialog() == DialogResult.OK) {
                if (doc_ != null) doc_.Dispose();

                doc_ = new PDFLibNet.PDFWrapper();
                doc_.UseMuPDF = false;
                if (doc_.LoadPDF(dialog.FileName)) {
                    doc_.CurrentPage = 1;
                    doc_.FitToWidth(MainViewer.Handle);
                    this.Text = System.IO.Path.GetFileName(dialog.FileName) + " - " + Properties.Settings.Default.TITLE;
                    this.ReDraw();
                }
            }
        }

        /* ----------------------------------------------------------------- */
        /// MainForm_MouseEnter
        /* ----------------------------------------------------------------- */
        private void MainForm_MouseEnter(object sender, EventArgs e) {
            this.Focus();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MainForm_MouseWheel
        /// 
        /// <summary>
        /// マウスホイールのイベントハンドラ．マウスホイールには，
        /// 前ページ/次ページ の挙動を割り当てる．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void MainForm_MouseWheel(object sender, EventArgs e) {
            var mouse = (System.Windows.Forms.MouseEventArgs)e;

            /*
             * 1 wheel up で（1回上側にカチっと音がするまでずらす）+120，
             * 1 wheel down で -120
             */
            if (mouse.Delta < 0) NextPage(sender, e);
            else PreviousPage(sender, e);
        }

        /* ----------------------------------------------------------------- */
        /// MenuPrevious_Click
        /* ----------------------------------------------------------------- */
        private void MenuPrevious_Click(object sender, EventArgs e) {
            PreviousPage(sender, e);
        }

        /* ----------------------------------------------------------------- */
        /// MenuNext_Click
        /* ----------------------------------------------------------------- */
        private void MenuNext_Click(object sender, EventArgs e) {
            NextPage(sender, e);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MenuCurrentPage_KeyDown
        /// 
        /// <summary>
        /// ユーザがページ数を直接指定してエンターキーを押した場合の
        /// イベントハンドラ．最終ページ以降の値が指定された場合には，
        /// 最終ページを表示させる．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void MenuCurrentPage_KeyDown(object sender, KeyEventArgs e) {
            if (doc_ == null) return;

            if (e.KeyCode == Keys.Enter) {
                try {
                    doc_.CurrentPage = System.Math.Min(int.Parse(MenuCurrentPage.Text), doc_.PageCount);
                }
                catch (Exception /* err */) { }
                finally {
                    this.ReDraw();
                }
            }
        }
        
        /* ----------------------------------------------------------------- */
        /// MenuZoomIn_Click
        /* ----------------------------------------------------------------- */
        private void MenuZoomIn_Click(object sender, EventArgs e) {
            if (doc_ == null) return;

            try {
                doc_.ZoomIN();
            }
            catch (Exception /* err */) { }
            finally {
                this.ReDraw();
            }
        }

        /* ----------------------------------------------------------------- */
        /// MenuZoomOut_Click
        /* ----------------------------------------------------------------- */
        private void MenuZoomOut_Click(object sender, EventArgs e) {
            if (doc_ == null) return;

            try {
                doc_.ZoomOut();
            }
            catch (Exception /* err */) { }
            finally {
                this.ReDraw();
            }
        }

        /* ----------------------------------------------------------------- */
        /// MenuZoomText_DropDownItemClicked
        /* ----------------------------------------------------------------- */
        private void MenuZoomText_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            if (doc_ == null) return;

            try {
                var zoom = e.ClickedItem.Text.Replace("%", "");
                doc_.Zoom = int.Parse(zoom);
            }
            catch (Exception /* err */) { }
            finally {
                this.ReDraw();
            }
        }
        
        /* ----------------------------------------------------------------- */
        /// MenuFitToWidth_Click
        /* ----------------------------------------------------------------- */
        private void MenuFitToWidth_Click(object sender, EventArgs e) {
            if (doc_ == null) return;

            try {
                doc_.FitToWidth(MainViewer.Handle);
            }
            catch (Exception /* err */) { }
            finally {
                this.ReDraw();
            }
        }

        /* ----------------------------------------------------------------- */
        /// MenuFitToHeight_Click
        /* ----------------------------------------------------------------- */
        private void MenuFitToHeight_Click(object sender, EventArgs e) {
            if (doc_ == null) return;

            try {
                doc_.FitToHeight(MainViewer.Handle);
            }
            catch (Exception /* err */) { }
            finally {
                this.ReDraw();
            }
        }

        /* ----------------------------------------------------------------- */
        /// MenuSearch_Click
        /* ----------------------------------------------------------------- */
        private void MenuSearch_Click(object sender, EventArgs e) {
            if (doc_ == null || MenuSearchText.Text == "") return;
            try {
                this.Search(sender, true);
            }
            catch (Exception /* err */) { }
        }

        /* ----------------------------------------------------------------- */
        /// MenuSearchText_TextChanged
        /* ----------------------------------------------------------------- */
        private void MenuSearchText_TextChanged(object sender, EventArgs e) {
            from_begin_ = true;
        }

        /* ----------------------------------------------------------------- */
        /// MenuSearchText_KeyDown
        /* ----------------------------------------------------------------- */
        private void MenuSearchText_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                this.MenuSearch_Click(sender, (EventArgs)e);
            }
        }

#endregion

        /* ----------------------------------------------------------------- */
        //  DoubleBuffer 関連のイベント・ハンドラ
        /* ----------------------------------------------------------------- */
#region Event handlers about DoubleBuffer which is located in PageViewer

        /* ----------------------------------------------------------------- */
        /// DoubleBuffer_PaintControl
        /* ----------------------------------------------------------------- */
        private void DoubleBuffer_PaintControl(object sender, Rectangle view, Point location, Graphics g) {
            if (doc_ == null) return;

            Size sF = new Size(view.Right, view.Bottom);
            Rectangle r = new Rectangle(location, sF);
            doc_.ClientBounds = r;
            doc_.CurrentX = view.X;
            doc_.CurrentY = view.Y;
            doc_.DrawPageHDC(g.GetHdc());
            g.ReleaseHdc();
        }

#endregion

        /* ----------------------------------------------------------------- */
        //  メンバ変数の定義
        /* ----------------------------------------------------------------- */
#region Member variables
        private PDFLibNet.PDFWrapper doc_ = null;
        private bool from_begin_ = true;
#endregion
    }
}
