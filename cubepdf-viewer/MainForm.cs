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
 *  Last-modified: Tue 24 Aug 2010 22:00:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cube {
    /* --------------------------------------------------------------------- */
    ///
    /// MainForm
    /// 
    /// <summary>
    /// NOTE: PDFViewer ではファイルをロードしている間，「リサイズ」，
    /// 「フォームを閉じる」，「各種マウスイベント」を無効化している．
    /// ただ，PDFViewer はこの処理が原因で異常終了するケースが散見される
    /// ため，CubePDF Viewer ではこの処理は保留する．
    /// 
    /// また，現在は使用していないが，PDFLoadBegin, PDFLoadCompleted
    /// イベントが用意されてある．
    /// ファイルのロード時間がやや長いので，この辺りのイベントに適切な
    /// ハンドラを指定する必要があるか．
    /// 追記: PDFLoad() よりは，その後の RenderPage() メソッドの方に
    /// 大きく時間を食われている模様．そのため，これらのイベントは
    /// あまり気にしなくて良い．
    /// 
    /// RenderFinished の他に RenderNotifyFinished と言うイベントも存在
    /// する．現状では，どのような条件でこのイベントが発生するのかは不明．
    /// </summary>
    /// 
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
            this.MenuZoomText.Enabled = false;
        }

        /* ----------------------------------------------------------------- */
        /// UpdateFitCondtion
        /* ----------------------------------------------------------------- */
        private void UpdateFitCondition(int which) {
            fit_ = which;
            MenuFitToWidth.Checked = ((fit_ & FIT_WIDTH) != 0);
            MenuFitToHeight.Checked = ((fit_ & FIT_HEIGHT) != 0);
            this.Refresh();
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
            if (doc_ != null) {
                StatusText.Text = status;
                doc_.RenderPage(MainViewer.Handle);
                this.PostReDraw();
            }
            else this.Refresh();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// AsyncReDraw
        /// 
        /// <summary>
        /// 再描画処理．時間のかかるような再描画処理をスレッドを利用して
        /// 非同期で行う．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void AsyncReDraw(string status = "Ready") {
            if (doc_ != null) {
                StatusText.Text = status;
                doc_.RenderFinished -= new PDFLibNet.RenderFinishedHandler(PostReDraw);
                doc_.RenderFinished += new PDFLibNet.RenderFinishedHandler(PostReDraw);
                doc_.RenderPageThread(MainViewer.Handle, false);
                this.PostReDraw();

                // まだ描画されていない領域を白色で表示しておく．
                MainViewer.PageColor = System.Drawing.Color.White;
                // this.Cursor = Cursors.WaitCursor;
                this.Refresh();
            }
            else this.Refresh();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PostReDraw
        /// 
        /// <summary>
        /// 再描画の最後に行う処理．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        void PostReDraw() {
            if (doc_ != null) {
                MainViewer.PageSize = new Size(doc_.PageWidth, doc_.PageHeight);
                MainViewer.PageColor = System.Drawing.Color.Transparent;
                this.Cursor = Cursors.Default;

                // メニューバーの各種情報の更新．
                MenuCurrentPage.Text = doc_.CurrentPage.ToString();
                MenuTotalPage.Text = "/ " + doc_.PageCount.ToString();
                MenuZoomText.Text = ((int)(doc_.Zoom)).ToString() + "%";
            }
            this.Refresh();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// LoadFile
        /// 
        /// <summary>
        /// ユーザから指定されたファイルを開く．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private bool LoadFile(string filename) {
            if (doc_ == null) return false;

            try {
                return doc_.LoadPDF(filename);
            }
            catch (System.Security.SecurityException) {
                PasswordDialog frm = new PasswordDialog();
                if (frm.ShowDialog() == DialogResult.OK) {
                    if (!frm.Password.Equals(String.Empty)) doc_.UserPassword = frm.Password;
                    return LoadFile(filename);
                }
                else {
                    MessageBox.Show(Properties.Settings.Default.ERROR_PASSWORD,
                        Properties.Settings.Default.ERROR_TITLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }
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
        ///
        /// Search
        /// 
        /// <summary>
        /// 検索結果を描画する．
        /// TODO: あるページ内の検索結果に対するスクロールバーの調整を
        /// 行っていない (FocusSearchResult) 為，その部分の実装．
        /// 処理部分の記述にバグがある気がする．
        /// </summary>
        /// 
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
                // FocusSearchResult(doc_.SearchResults[0]);
                this.ReDraw();
            }
            else {
                Console.Beep();
                from_begin_ = true;
                this.ReDraw(Properties.Settings.Default.STATUS_EOS);
            }
           
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Search
        ///
        /// <summary>
        /// 検索方向を指定して検索する．
        /// </summary>
        /// 
        /// <param name="vector"">
        /// 検索方向
        ///   true:  現在の位置よりも後方向．
        ///   false: 現在の位置よりも前方向．
        /// </param>
        /// 
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
            // MuPDF の場合は不要．
            // PDFLibNet.xPDFParams.Antialias = true;
            // PDFLibNet.xPDFParams.VectorAntialias = true;
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

            int x = Screen.PrimaryScreen.Bounds.Height - 100;
            this.Size = new Size(System.Math.Max(x, 800), x);
            this.MainViewer.Size = new Size(System.Math.Max(x, 800) - DELTA_WIDTH, x - DELTA_HEIGHT);

            // TODO: サイズの設定がおかしい．
            //int width = this.MainViewer.Width;
            //this.MainViewer.PageSize = new Size(width, (int)(width * 11 / 8.5));
            this.MainViewer.PageSize = this.MainViewer.Size;
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
            switch (e.KeyCode) {
            case Keys.Escape: // 検索の解除
                // SearchResultを空にするため空文字で検索
                if (doc_ != null) {
                    doc_.FindText("", doc_.CurrentPage, PDFLibNet.PDFSearchOrder.PDFSearchFromCurrent,
                        false, false, true, false);
                }
                this.ReDraw(Properties.Settings.Default.STATUS_EOS);
                break;
            case Keys.F3: // 次の検索
                //if (MenuSearchText.Text != "") this.Search(sender, !e.Shift);
                if (MenuSearchText.Text != "") this.Search(sender, true);
                break;
            case Keys.F: // 検索
                if (e.Control) this.MenuSearchText.Focus();
                break;
            case Keys.O: // ファイルを開く
                if (e.Control) this.MenuOpen_Click(sender, (EventArgs)e);
                break;
            default:
                break;
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MainForm_SizeChanged
        /// 
        /// <summary>
        /// ウィンドウサイズを変更する．
        /// TODO: スクロールバーがおかしい．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void MainForm_SizeChanged(object sender, EventArgs e) {
            int width = this.Size.Width - DELTA_WIDTH;
            int height = this.Size.Height - DELTA_HEIGHT;
            this.MainViewer.Size = new Size(width, height);
            if (MenuFitToWidth.Checked) this.MenuFitToWidth_Click(sender, e);
            else if (MenuFitToHeight.Checked) this.MenuFitToHeight_Click(sender, e);
            else this.ReDraw();
        }
        
        /* ----------------------------------------------------------------- */
        ///
        /// MenuOpen_Click
        /// 
        /// <summary>
        /// ToolStrip->MenuOpen の Click イベントハンドラ
        /// NOTE: PDFLoadBegin(), PDFLoadCompleted はそれぞれ
        /// doc_.LoadPDF() の実行開始直後，終了直前に呼ばれる．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void MenuOpen_Click(object sender, EventArgs e) {
            var dialog = new OpenFileDialog();
            dialog.Filter = Properties.Settings.Default.FILTER_PDF;

            if (dialog.ShowDialog() == DialogResult.OK) {
                if (doc_ != null) doc_.Dispose();

                doc_ = new PDFLibNet.PDFWrapper();
                doc_.PDFLoadCompleted += new PDFLibNet.PDFLoadCompletedHandler(PDFLoadCompleted);
                doc_.PDFLoadBegin += new PDFLibNet.PDFLoadBeginHandler(PDFLoadBegin);
                doc_.UseMuPDF = USE_MUPDF;

                if (this.LoadFile(dialog.FileName)) {
                    doc_.CurrentPage = 1;
                    if (MenuFitToHeight.Checked) doc_.FitToHeight(MainViewer.Handle);
                    else doc_.FitToWidth(MainViewer.Handle);
                    this.Text = System.IO.Path.GetFileName(dialog.FileName) + " - " + Properties.Settings.Default.TITLE;
                    this.Cursor = Cursors.WaitCursor;
                    this.MenuZoomText.Enabled = true;
                    this.ReDraw(); // ここは AsyncReDraw() だとうまくいかない．
                }
                else {
                    MessageBox.Show(Properties.Settings.Default.ERROR_LOAD_FILE,
                        Properties.Settings.Default.ERROR_TITLE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /* ----------------------------------------------------------------- */
        /// PDFLoadBegin
        /* ----------------------------------------------------------------- */
        private void PDFLoadBegin() {
            this.Cursor = Cursors.WaitCursor;
            this.Refresh();
        }

        /* ----------------------------------------------------------------- */
        /// PDFLoadCompleted
        /* ----------------------------------------------------------------- */
        private void PDFLoadCompleted() {
            this.Cursor = Cursors.Default;
            this.PostReDraw();
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
        /// MenuFirstPage_Click
        /* ----------------------------------------------------------------- */
        private void MenuFirstPage_Click(object sender, EventArgs e) {
            if (doc_ == null) return;
            if (doc_.CurrentPage <= 1) return;
            
            doc_.CurrentPage = 1;
            this.ReDraw();
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
        /// MenuLastPage_Click
        /* ----------------------------------------------------------------- */
        private void MenuLastPage_Click(object sender, EventArgs e) {
            if (doc_ == null) return;
            if (doc_.CurrentPage >= doc_.PageCount) return;

            doc_.CurrentPage = doc_.PageCount;
            this.ReDraw();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MenuCurrentPage_KeyDown
        /// 
        /// <summary>
        /// ユーザがページ数を直接指定してエンターキーを押した場合の
        /// イベントハンドラ．最終ページ以降の値が指定された場合には，
        /// 最終ページを表示させる．
        /// TODO: 数字以外が入力された場合にどうなるか．
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
            UpdateFitCondition(FIT_NONE);
            if (doc_ == null) return;

            try {
                doc_.ZoomIN();
            }
            catch (Exception /* err */) { }
            finally {
                this.AsyncReDraw();
            }
        }

        /* ----------------------------------------------------------------- */
        /// MenuZoomOut_Click
        /* ----------------------------------------------------------------- */
        private void MenuZoomOut_Click(object sender, EventArgs e) {
            UpdateFitCondition(FIT_NONE);
            if (doc_ == null) return;

            try {
                doc_.ZoomOut();
            }
            catch (Exception /* err */) { }
            finally {
                this.AsyncReDraw();
            }
        }

        /* ----------------------------------------------------------------- */
        /// MenuZoomText_DropDownItemClicked
        /* ----------------------------------------------------------------- */
        private void MenuZoomText_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            UpdateFitCondition(FIT_NONE);
            if (doc_ == null) return;

            try {
                var zoom = e.ClickedItem.Text.Replace("%", "");
                doc_.Zoom = int.Parse(zoom);
            }
            catch (Exception /* err */) { }
            finally {
                this.AsyncReDraw();
            }
        }
        
        /* ----------------------------------------------------------------- */
        /// MenuFitToWidth_Click
        /* ----------------------------------------------------------------- */
        private void MenuFitToWidth_Click(object sender, EventArgs e) {
            UpdateFitCondition(MenuFitToWidth.Checked ? FIT_WIDTH : FIT_NONE);
            if (doc_ == null) return;

            try {
                if (MenuFitToWidth.Checked) {
                    doc_.FitToWidth(MainViewer.Handle);
                }
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
            UpdateFitCondition(MenuFitToHeight.Checked ? FIT_HEIGHT : FIT_NONE);
            if (doc_ == null) return;

            try {
                if (MenuFitToHeight.Checked) {
                    doc_.FitToHeight(MainViewer.Handle);
                }
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
        //  定数の定義
        /* ----------------------------------------------------------------- */
        #region Constant variables
        private const int DELTA_WIDTH = 40;
        private const int DELTA_HEIGHT = 115;
        private const bool USE_MUPDF = true;
        private const int FIT_NONE = 0;
        private const int FIT_WIDTH = 0x01;
        private const int FIT_HEIGHT = 0x02;
        #endregion

        /* ----------------------------------------------------------------- */
        //  メンバ変数の定義
        /* ----------------------------------------------------------------- */
        #region Member variables
        private PDFLibNet.PDFWrapper doc_ = null;
        private bool from_begin_ = true;
        private int fit_ = 0;
        #endregion
    }
}
