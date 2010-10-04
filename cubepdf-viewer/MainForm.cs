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
 *  Last-modified: Wed 01 Sep 2010 00:10:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Drawing;
using System.Drawing.Printing;
using PDF = PDFLibNet.PDFWrapper;
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
        /// Constructor
        /* ----------------------------------------------------------------- */
        public MainForm() {
            this.Initialize();
        }

        /* ----------------------------------------------------------------- */
        /// Constructor
        /* ----------------------------------------------------------------- */
        public MainForm(string path) {
            this.Initialize();
            this.Tag = path;
        }

        /* ----------------------------------------------------------------- */
        /// Initialize
        /* ----------------------------------------------------------------- */
        private void Initialize() {
            InitializeComponent();

            int x = Screen.PrimaryScreen.Bounds.Height - 100;
            this.Size = new Size(System.Math.Max(x, 800), x);

            this.MenuToolStrip.Renderer = new CustomToolStripRenderer();
            this.MenuSplitContainer.SplitterDistance = this.MenuToolStrip.Height;
            this.FitToHeightButton.Checked = true;
            CreateTabContextMenu(this.PageViewerTabControl);

            this.MouseEnter += new EventHandler(this.MainForm_MouseEnter);
            this.MouseWheel += new MouseEventHandler(this.MainForm_MouseWheel);
        }

        /* ----------------------------------------------------------------- */
        /// UpdateFitCondtion
        /* ----------------------------------------------------------------- */
        private void UpdateFitCondition(FitCondition which) {
            fit_ = which;
            this.FitToWidthButton.Checked = ((fit_ & FitCondition.Width) != 0);
            this.FitToHeightButton.Checked = ((fit_ & FitCondition.Height) != 0);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Refresh
        /// 
        /// <summary>
        /// システムの Refresh() を呼ぶ前に，必要な情報を全て更新する．
        /// MEMO: サムネイル画面を更新するとちらつきがひどいので，
        /// 最小限の更新になるようにしている．
        /// ステータスバーを除去した．現状は，message はどこにも表示させて
        /// いない (2010/09/30)．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void Refresh(PictureBox canvas, string message = "") {
            if (canvas == null || canvas.Tag == null) {
                this.CurrentPageTextBox.Text = "0";
                this.TotalPageLabel.Text = "0";
                this.ZoomDropDownButton.Text = "100%";
            }
            else {
                this.CurrentPageTextBox.Text = CanvasPolicy.CurrentPage(canvas).ToString();
                this.TotalPageLabel.Text = CanvasPolicy.PageCount(canvas).ToString();
                this.ZoomDropDownButton.Text = ((int)CanvasPolicy.Zoom(canvas)).ToString() + "%";

                // scrollbarのsmallchangeの更新
                var control = (ScrollableControl)canvas.Parent;
                var vsb = control.VerticalScroll;
                var hsb = control.HorizontalScroll;

                // Minimumは0と仮定
                vsb.SmallChange = (vsb.Maximum - vsb.LargeChange) / 20;
                hsb.SmallChange = (hsb.Maximum - hsb.LargeChange) / 20;

                control.Parent.Refresh();
            }

            if (this.MainMenuStrip != null) this.MainMenuStrip.Refresh();
        }

        /* ----------------------------------------------------------------- */
        /// Open
        /* ----------------------------------------------------------------- */
        private void Open(TabPage tab, string path, string password = "") {
            var canvas = CanvasPolicy.Create(tab);
            var message = "";

            try {
                CanvasPolicy.Open(canvas, path, password, fit_);
                this.CreateThumbnail(canvas);
            }
            catch (System.Security.SecurityException /* err */) {
                PasswordDialog dialog = new PasswordDialog(path);
                dialog.ShowDialog();
                if (dialog.Password.Length > 0) this.Open(tab, path, dialog.Password);
            }
            catch (Exception err) {
                message = err.Message;
            }
            finally {
                this.Refresh(canvas, message);
            }
        }

        /* ----------------------------------------------------------------- */
        /// NextPage
        /* ----------------------------------------------------------------- */
        private bool NextPage(TabPage tab) {
            var canvas = CanvasPolicy.Get(tab);
            if (canvas == null) return false;

            var status = true;
            var message = "";
            try {
                int prev = CanvasPolicy.CurrentPage(canvas);
                if (CanvasPolicy.NextPage(canvas) == prev) status = false;
            }
            catch (Exception err) {
                status = false;
                message = err.Message;
            }
            finally {
                this.Refresh(canvas, message);
            }
            return status;
        }

        /* ----------------------------------------------------------------- */
        /// PreviousPage
        /* ----------------------------------------------------------------- */
        private bool PreviousPage(TabPage tab) {
            var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
            if (canvas == null) return false;

            var status = true;
            var message = "";
            try {
                int prev = CanvasPolicy.CurrentPage(canvas);
                if (CanvasPolicy.PreviousPage(canvas) == prev) status = false;
            }
            catch (Exception err) {
                status = false;
                message = err.Message;
            }
            finally {
                this.Refresh(canvas, message);
            }
            return status;
        }

        /* ----------------------------------------------------------------- */
        /// Search
        /* ----------------------------------------------------------------- */
        private void Search(TabPage tab, string text, bool next) {
            var canvas = CanvasPolicy.Get(tab);
            var message = "";

            try {
                var args = new SearchArgs(text);
                args.FromBegin = begin_;
                args.IgnoreCase = true;
                args.WholeDocument = true;
                args.WholeWord = false;
                args.FindNext = next;

                var result = CanvasPolicy.Search(canvas, args);
                begin_ = !result; // 最後まで検索したら始めに戻る
            }
            catch (Exception err) {
                message = err.Message;
            }
            finally {
                this.Refresh(canvas, message);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ResetSearch
        /// 
        /// <summary>
        /// MEMO: ライブラリが，検索結果を描画する状態を解除する方法を
        /// 持っていないため，空の文字列で検索してリセットする．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void ResetSearch(TabPage tab) {
            var canvas = CanvasPolicy.Get(tab);

            try {
                var dummy = new SearchArgs();
                CanvasPolicy.Search(canvas, dummy);
            }
            catch (Exception /* err */) { }
            finally {
                begin_ = true;
                this.Refresh(canvas);
            }
        }

        /* ----------------------------------------------------------------- */
        /// Adjust
        /* ----------------------------------------------------------------- */
        private void Adjust(TabPage tab) {
            var canvas = CanvasPolicy.Get(tab);
            var message = "";

            try {
                if (this.FitToWidthButton.Checked) CanvasPolicy.FitToWidth(canvas);
                else if (this.FitToHeightButton.Checked) CanvasPolicy.FitToHeight(canvas);
                else CanvasPolicy.Adjust(canvas, CanvasPolicy.PageSize(canvas));
            }
            catch (Exception err) {
                message = err.Message;
            }
            finally {
                this.Refresh(canvas, message);
            }
        }

        /* ----------------------------------------------------------------- */
        /// CreateTab
        /* ----------------------------------------------------------------- */
        public TabPage CreateTab(TabControl parent) {
            var tab = new TabPage();

            // TabPage の設定
            tab.AutoScroll = true;
            tab.BackColor = Color.DimGray;
            //tab.BorderStyle = BorderStyle.Fixed3D;
            tab.ContextMenuStrip = new ContextMenuStrip();
            tab.Text = "(無題)";
            tab.Scroll += new ScrollEventHandler(VerticalScrolled);
            tab.DragEnter += new DragEventHandler(TabPage_DragEnter);
            tab.DragDrop += new DragEventHandler(TabPage_DragDrop);
            parent.Controls.Add(tab);
            parent.SelectedIndex = parent.TabCount - 1;

            return tab;
        }

        /* ----------------------------------------------------------------- */
        /// DestroyTab
        /* ----------------------------------------------------------------- */
        public void DestroyTab(TabPage tab) {
            var parent = (TabControl)tab.Parent;
            var canvas = CanvasPolicy.Get(tab);
            var thumb = CanvasPolicy.GetThumbnail(this.NavigationSplitContainer.Panel1);
            if (thumb != null) CanvasPolicy.DestroyThumbnail(thumb);
            CanvasPolicy.Destroy(canvas);
            if (this.PageViewerTabControl.TabCount > 1) parent.TabPages.Remove(tab);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateTabContextMenu
        ///
        /// <summary>
        /// コンテキストメニューを設定する．
        /// TODO: コンテキストメニューから登録元である TabControl の
        /// オブジェクトへ辿る方法の調査．現状では，暫定的にコンテキスト
        /// メニューの Tag に TabControl のオブジェクトを設定しておく
        /// 事で対処している．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public void CreateTabContextMenu(TabControl parent) {
            var menu = new ContextMenuStrip();
            var elem = new ToolStripMenuItem();
            elem.Text = "閉じる";
            elem.Click += new EventHandler(TabClosed);
            menu.Items.Add(elem);
            parent.MouseDown += new MouseEventHandler(ContextMenu_MouseDown);
            parent.ContextMenuStrip = menu;

            foreach (TabPage child in parent.TabPages) {
                child.ContextMenuStrip = new ContextMenuStrip();
            }
        }

        /* ----------------------------------------------------------------- */
        /// CreateThumbnail
        /* ----------------------------------------------------------------- */
        private void CreateThumbnail(PictureBox canvas) {
            var old = CanvasPolicy.GetThumbnail(this.NavigationSplitContainer.Panel1);
            if (old != null) CanvasPolicy.DestroyThumbnail(old);
            ListView thumb = CanvasPolicy.CreateThumbnail(canvas, this.NavigationSplitContainer.Panel1);
            thumb.SelectedIndexChanged += new EventHandler(PageChanged);
        }

        /* ----------------------------------------------------------------- */
        //  キーボード・ショートカット一覧
        /* ----------------------------------------------------------------- */
        #region Keybord shortcuts

        /* ----------------------------------------------------------------- */
        ///
        /// MainForm_KeyDown
        ///
        /// <summary>
        /// キーボード・ショートカット一覧．
        /// KeyPreview を有効にして，全てのキーボードイベントを一括で
        /// 処理している．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void MainForm_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
            case Keys.Enter:
                if (this.SearchTextBox.Focused && this.SearchTextBox.Text.Length > 0) {
                    this.SearchButton_Click(this.SearchButton, e);
                }
                break;
            case Keys.Escape:
                this.ResetSearch(this.PageViewerTabControl.SelectedTab);
                break;
            case Keys.Right:
            case Keys.Down:
                if (e.Control) this.ZoomInButton_Click(this.ZoomInButton, e);
                //else this.NextPageButton_Click(this.NextPageButton, e);
                break;
            case Keys.Left:
            case Keys.Up:
                if (e.Control) this.ZoomOutButton_Click(this.ZoomOutButton, e);
                //else this.PreviousPageButton_Click(this.PreviousPageButton, e);
                break;
            case Keys.F3: // 検索
                if (this.SearchTextBox.Text.Length > 0) this.Search(this.PageViewerTabControl.SelectedTab, this.SearchTextBox.Text, !e.Shift);
                break;
            case Keys.F4: // 閉じる
                if (e.Control) this.DestroyTab(this.PageViewerTabControl.SelectedTab);
                break;
            case Keys.F8: // メニューの表示/非表示
                this.MenuModeButton_Click(this.MenuModeButton, e);
                break;
            case Keys.F:  // 検索ボックスにフォーカス
                if (e.Control) this.SearchTextBox.Focus();
                break;
            case Keys.M:  // メニューの表示/非表示
                if (e.Control) this.MenuModeButton_Click(this.MenuModeButton, e);
                break;
            case Keys.N:  // 新規タブ
                if (e.Control) this.NewTabButton_Click(this.PageViewerTabControl, e);
                break;
            case Keys.O:  // ファイルを開く
                if (e.Control) this.OpenButton_Click(this.PageViewerTabControl.SelectedTab, e);
                break;
            case Keys.W:  // ファイルを閉じる
                if (e.Control) this.CloseButton_Click(this.PageViewerTabControl.SelectedTab, e);
                break;
            default:
                break;
            }
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  メインフォームに関する各種イベント・ハンドラ
        /* ----------------------------------------------------------------- */
        #region MainForm Event handlers

        /* ----------------------------------------------------------------- */
        /// MainForm_Shown
        /* ----------------------------------------------------------------- */
        private void MainForm_Shown(object sender, EventArgs e) {
            if (this.Tag != null) {
                var path = (string)this.Tag;
                var tab = this.PageViewerTabControl.SelectedTab;
                this.Open(tab, path);
                this.Tag = null;
            }
        }

        /* ----------------------------------------------------------------- */
        /// MainForm_SizeChanged
        /* ----------------------------------------------------------------- */
        private void MainForm_SizeChanged(object sender, EventArgs e) {
            this.Adjust(this.PageViewerTabControl.SelectedTab);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MainForm_MouseWheel
        /// 
        /// <summary>
        /// マウスホイールによるスクロールの処理．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void MainForm_MouseWheel(object sender, MouseEventArgs e) {
            if (Math.Abs(e.Delta) < 120) return;

            var tab = this.PageViewerTabControl.SelectedTab;
            var scroll = tab.VerticalScroll;
            if (!scroll.Visible) {
                if (e.Delta < 0) this.NextPage(tab);
                else this.PreviousPage(tab);
            }
            else {
                var maximum = 1 + scroll.Maximum - scroll.LargeChange; // ユーザのコントロールで取れる scroll.Value の最大値
                var delta = -(e.Delta / 120) * scroll.SmallChange;
                if (scroll.Value == scroll.Minimum && delta < 0) {
                    if (wheel_counter_ > 2) {
                        if (this.PreviousPage(tab)) tab.AutoScrollPosition = new Point(0, maximum);
                        wheel_counter_ = 0;
                    }
                    else wheel_counter_++;
                }
                else if (scroll.Value == maximum && delta > 0) {
                    if (wheel_counter_ > 2) {
                        if (this.NextPage(tab)) tab.AutoScrollPosition = new Point(0, 0);
                        wheel_counter_ = 0;
                    }
                    else wheel_counter_++;
                }
                else {
                    var value = Math.Min(Math.Max(scroll.Value + delta, scroll.Minimum), maximum);
                    tab.AutoScrollPosition = new Point(0, value);
                    wheel_counter_ = 0;
                }
            }
        }

        /* ----------------------------------------------------------------- */
        /// MainForm_MouseEnter
        /* ----------------------------------------------------------------- */
        private void MainForm_MouseEnter(object sender, EventArgs e) {
            this.Focus();
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  メインフォームに登録している各種コントロールのイベントハンドラ
        /* ----------------------------------------------------------------- */
        #region Other controls event handlers

        /* ----------------------------------------------------------------- */
        /// NewTabButton_Click
        /* ----------------------------------------------------------------- */
        private void NewTabButton_Click(object sender, EventArgs e) {
            this.CreateTab(this.PageViewerTabControl);
        }

        /* ----------------------------------------------------------------- */
        /// OpenButton_Click
        /* ----------------------------------------------------------------- */
        private void OpenButton_Click(object sender, EventArgs e) {
            var dialog = new OpenFileDialog();
            dialog.Filter = "PDF ファイル(*.pdf)|*.pdf";
            if (dialog.ShowDialog() == DialogResult.OK) {
                var tab = this.PageViewerTabControl.SelectedTab;
                this.Open(tab, dialog.FileName);
            }
        }

        /* ----------------------------------------------------------------- */
        /// CloseButton_Click
        /* ----------------------------------------------------------------- */
        private void CloseButton_Click(object sender, EventArgs e) {
            var tab = this.PageViewerTabControl.SelectedTab;
            this.DestroyTab(tab);
        }

        /* ----------------------------------------------------------------- */
        /// PrintButton_Click
        /* ----------------------------------------------------------------- */
        private void PrintButton_Click(object sender, EventArgs e) {
            var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
            if (canvas == null) return;
            var core = (PDF)canvas.Tag;
            if (core == null) return;

            // 一旦PrintするためにはPDFWrapperの値を変える必要がある
            // TODO: paintEventHandlerの解除と各種レンダリングに必要なプロパティを保存
            // 印刷終了後元に戻す
            var saveSettings = new { page = core.CurrentPage, zoom = core.Zoom };

            using (var prd = new PrintDialog())
            using (var document = new System.Drawing.Printing.PrintDocument()) {
                prd.AllowCurrentPage = true;
                prd.AllowSelection = false; // 選択した部分の設定だが、ページを選択する方法を提供しないのでfalse
                prd.AllowSomePages = true;
                prd.PrinterSettings.MinimumPage = 1;
                prd.PrinterSettings.MaximumPage = core.PageCount;
                prd.PrinterSettings.FromPage = core.CurrentPage;
                prd.PrinterSettings.ToPage = core.PageCount;

                if (prd.ShowDialog() == DialogResult.OK) {
                    document.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);
                    document.PrinterSettings = prd.PrinterSettings;

                    core.CurrentPage = prd.PrinterSettings.FromPage;
                    document.Print();
                    core.CurrentPage = saveSettings.page;
                    core.Zoom = saveSettings.zoom;
                    core.RenderPage(IntPtr.Zero, false, false);
                    Adjust(this.PageViewerTabControl.SelectedTab);
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// PrintDocument_PrintPage
        ///
        /// <summary>
        /// The PrintPage event is raised for each page to be printed.
        /// TODO: レンダリングのDpiを変更しなければならない？
        /// また、ページ全体の印刷やページ指定での印刷ができなければならない
        /// NOTE: 次回600dpiで描画してみる & renderPageThread を用いて
        /// レンダリングが終わるまで待機させたい
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs ev) {
            //const float INCH = 2.54f;
            //const int DPI = 72;

            var control = this.PageViewerTabControl;
            var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
            var core = (PDF)canvas.Tag;


            using (var bitmap = new Bitmap(ev.PageSettings.PaperSize.Width, ev.PageSettings.PaperSize.Height)) {
                // NOTE: ページのサイズに合わせて拡大縮小するにはどうすればよいのか？
                var g = Graphics.FromImage(bitmap);
                core.ClientBounds = new Rectangle(new Point(0, 0), bitmap.Size);
                core.Zoom = 100;
                core.RenderPage(IntPtr.Zero, false, false);
                core.DrawPageHDC(g.GetHdc());
                g.ReleaseHdc();
                g.Save();


                ev.Graphics.DrawImage(bitmap, 0, 0);
            }


            // If more lines exist, print another page.
            if (ev.PageSettings.PrinterSettings.PrintRange == PrintRange.AllPages) {
                if (core.CurrentPage < core.PageCount) {
                    core.NextPage();
                    ev.HasMorePages = true;
                }
                else {
                    ev.HasMorePages = false;
                }

            }
            else if (ev.PageSettings.PrinterSettings.PrintRange == PrintRange.SomePages) {
                if (core.CurrentPage < ev.PageSettings.PrinterSettings.ToPage) {
                    core.NextPage();
                    ev.HasMorePages = true;
                }
                else {
                    ev.HasMorePages = false;
                }
            }
            else {
                ev.HasMorePages = false;
            }
        }

        /* ----------------------------------------------------------------- */
        /// PageViewerTabControl_SelectedIndexChanged
        /* ----------------------------------------------------------------- */
        private void PageViewerTabControl_SelectedIndexChanged(object sender, EventArgs e) {
            var control = (TabControl)sender;
            var canvas = CanvasPolicy.Get(control.SelectedTab);
            if (canvas == null) return;

            this.CreateThumbnail(canvas);
            CanvasPolicy.Adjust(canvas);
            this.Refresh(canvas);
        }

        /* ----------------------------------------------------------------- */
        /// ZoomInButton_Click
        /* ----------------------------------------------------------------- */
        private void ZoomInButton_Click(object sender, EventArgs e) {
            this.UpdateFitCondition(FitCondition.None);
            var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
            if (canvas == null) return;

            var message = "";
            try {
                CanvasPolicy.ZoomIn(canvas);
            }
            catch (Exception err) {
                message = err.Message;
            }
            finally {
                this.Refresh(canvas, message);
            }
        }

        /* ----------------------------------------------------------------- */
        /// ZoomOutButton_Click
        /* ----------------------------------------------------------------- */
        private void ZoomOutButton_Click(object sender, EventArgs e) {
            this.UpdateFitCondition(FitCondition.None);
            var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
            if (canvas == null) return;

            var message = "";
            try {
                CanvasPolicy.ZoomOut(canvas);
            }
            catch (Exception err) {
                message = err.Message;
            }
            finally {
                this.Refresh(canvas, message);
            }
        }

        /* ----------------------------------------------------------------- */
        /// ZoomDropDownButton_DropDownItemClicked
        /* ----------------------------------------------------------------- */
        private void ZoomDropDownButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            this.UpdateFitCondition(FitCondition.None);
            var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
            if (canvas == null) return;

            var message = "";
            try {
                var zoom = e.ClickedItem.Text.Replace("%", "");
                CanvasPolicy.Zoom(canvas, int.Parse(zoom));
            }
            catch (Exception err) {
                message = err.Message;
            }
            finally {
                this.Refresh(canvas, message);
            }
        }

        /* ----------------------------------------------------------------- */
        /// FitToWidthButton_Click
        /* ----------------------------------------------------------------- */
        private void FitToWidthButton_Click(object sender, EventArgs e) {
            this.UpdateFitCondition(this.FitToWidthButton.Checked ? FitCondition.Width : FitCondition.None);
            var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
            if (canvas == null) return;

            var message = "";
            try {
                if (this.FitToWidthButton.Checked) CanvasPolicy.FitToWidth(canvas);
            }
            catch (Exception err) {
                message = err.Message;
            }
            finally {
                this.Refresh(canvas, message);
            }
        }

        /* ----------------------------------------------------------------- */
        /// FitToHeightButton_Click
        /* ----------------------------------------------------------------- */
        private void FitToHeightButton_Click(object sender, EventArgs e) {
            this.UpdateFitCondition(this.FitToHeightButton.Checked ? FitCondition.Height : FitCondition.None);
            var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
            if (canvas == null) return;

            var message = "";
            try {
                if (this.FitToHeightButton.Checked) CanvasPolicy.FitToHeight(canvas);
            }
            catch (Exception err) {
                message = err.Message;
            }
            finally {
                this.Refresh(canvas, message);
            }
        }

        /* ----------------------------------------------------------------- */
        /// NextPageButton_Click
        /* ----------------------------------------------------------------- */
        private void NextPageButton_Click(object sender, EventArgs e) {
            NextPage(this.PageViewerTabControl.SelectedTab);
        }

        /* ----------------------------------------------------------------- */
        /// PreviousPageButton_Click
        /* ----------------------------------------------------------------- */
        private void PreviousPageButton_Click(object sender, EventArgs e) {
            PreviousPage(this.PageViewerTabControl.SelectedTab);
        }

        /* ----------------------------------------------------------------- */
        /// FirstPageButton_Click
        /* ----------------------------------------------------------------- */
        private void FirstPageButton_Click(object sender, EventArgs e) {
            var tab = this.PageViewerTabControl.SelectedTab;
            var canvas = CanvasPolicy.Get(tab);
            if (canvas == null) return;

            var message = "";
            try {
                CanvasPolicy.FirstPage(canvas);
            }
            catch (Exception err) {
                message = err.Message;
            }
            finally {
                this.Refresh(canvas, message);
            }
        }

        /* ----------------------------------------------------------------- */
        /// LastPageButton_Click
        /* ----------------------------------------------------------------- */
        private void LastPageButton_Click(object sender, EventArgs e) {
            var tab = this.PageViewerTabControl.SelectedTab;
            var canvas = CanvasPolicy.Get(tab);
            if (canvas == null) return;

            var message = "";
            try {
                CanvasPolicy.LastPage(canvas);
            }
            catch (Exception err) {
                message = err.Message;
            }
            finally {
                this.Refresh(canvas);
            }
        }

        /* ----------------------------------------------------------------- */
        /// CurrentPageTextBox_KeyDown
        /* ----------------------------------------------------------------- */
        private void CurrentPageTextBox_KeyDown(object sender, KeyEventArgs e) {
            var tab = this.PageViewerTabControl.SelectedTab;
            var canvas = CanvasPolicy.Get(tab);
            if (canvas == null) return;

            if (e.KeyCode == Keys.Enter) {
                var message = "";
                try {
                    var control = (ToolStripTextBox)sender;
                    int page = int.Parse(control.Text);
                    CanvasPolicy.MovePage(canvas, page);
                }
                catch (Exception err) {
                    message = err.Message;
                }
                finally {
                    this.Refresh(canvas, message);
                }
            }
        }

        /* ----------------------------------------------------------------- */
        /// SearchTextBox_TextChanged
        /* ----------------------------------------------------------------- */
        private void SearchTextBox_TextChanged(object sender, EventArgs e) {
            begin_ = true;
        }

        /* ----------------------------------------------------------------- */
        /// SearchButton_Click
        /* ----------------------------------------------------------------- */
        private void SearchButton_Click(object sender, EventArgs e) {
            this.Search(this.PageViewerTabControl.SelectedTab, this.SearchTextBox.Text, true);
        }

        /* ----------------------------------------------------------------- */
        /// MenuModeButton_Click
        /* ----------------------------------------------------------------- */
        private void MenuModeButton_Click(object sender, EventArgs e) {
            this.MenuSplitContainer.Panel1Collapsed = !this.MenuSplitContainer.Panel1Collapsed;
            this.Adjust(this.PageViewerTabControl.SelectedTab);
        }

        /* ----------------------------------------------------------------- */
        /// ThumbButton_Click
        /* ----------------------------------------------------------------- */
        private void ThumbButton_Click(object sender, EventArgs e) {
            this.NavigationSplitContainer.Panel1Collapsed = !this.NavigationSplitContainer.Panel1Collapsed;
            this.Adjust(this.PageViewerTabControl.SelectedTab);
        }

        /* ----------------------------------------------------------------- */
        /// LogoButton_Click
        /* ----------------------------------------------------------------- */
        private void LogoButton_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://www.cube-soft.jp/");
        }

        /* ----------------------------------------------------------------- */
        /// TabPage_DragEnter
        /* ----------------------------------------------------------------- */
        private void TabPage_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.All;
            else e.Effect = DragDropEffects.None;
        }

        /* ----------------------------------------------------------------- */
        /// TabPage_DragDrop
        /* ----------------------------------------------------------------- */
        private void TabPage_DragDrop(object sender, DragEventArgs e) {
            var tab = (TabPage)sender;
            var control = (TabControl)tab.Parent;

            bool current = true;
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (var path in files) {
                    if (System.IO.Path.GetExtension(path).ToLower() != ".pdf") continue;
                    tab = current ? control.SelectedTab : this.CreateTab(control);
                    current = false;
                    this.Open(tab, path);
                }
            }
        }

        /* ----------------------------------------------------------------- */
        /// PageViewerTabControl_TabClosing
        /* ----------------------------------------------------------------- */
        private void PageViewerTabControl_TabClosing(object sender, TabControlCancelEventArgs e) {
            this.DestroyTab(e.TabPage);
            var control = (CustomTabControl)sender;
            if (control.TabCount <= 1) e.Cancel = true;
        }

        /* ----------------------------------------------------------------- */
        /// VerticalScrolled
        /* ----------------------------------------------------------------- */
        private void VerticalScrolled(object sender, ScrollEventArgs e) {
            var control = (TabPage)sender;
            var scroll = control.VerticalScroll;
            if (e.NewValue == e.OldValue) {
                var maximum = 1 + scroll.Maximum - scroll.LargeChange;
                if (scroll.Value == scroll.Minimum && e.Type == ScrollEventType.SmallDecrement) {
                    if (this.PreviousPage(control)) scroll.Value = maximum;
                }
                else if (scroll.Value == maximum && e.Type == ScrollEventType.SmallIncrement) {
                    this.NextPage(control);
                }
            }
        }

        /* ----------------------------------------------------------------- */
        /// PageChanged
        /* ----------------------------------------------------------------- */
        private void PageChanged(object sender, EventArgs e) {
            var thumb = (ListView)sender;
            if (thumb.SelectedItems.Count == 0) return;
            var page = thumb.SelectedItems[0].Index + 1;

            var tab = this.PageViewerTabControl.SelectedTab;
            var canvas = CanvasPolicy.Get(tab);
            CanvasPolicy.MovePage(canvas, page);
            this.Refresh(canvas);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TabClosed
        /// 
        /// <summary>
        /// コンテキストメニューの「閉じる」が押された時のイベントハンドラ．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void TabClosed(object sender, EventArgs e) {
            var control = this.PageViewerTabControl;
            for (int i = 0; i < control.TabCount; i++) {
                var rect = control.GetTabRect(i);
                if (pos_.X > rect.Left && pos_.X < rect.Right && pos_.Y > rect.Top && pos_.Y < rect.Bottom) {
                    TabPage tab = control.TabPages[i];
                    this.DestroyTab(tab);
                    break;
                }
            }
        }

        /* ----------------------------------------------------------------- */
        /// ContextMenu_MouseDown
        /* ----------------------------------------------------------------- */
        private void ContextMenu_MouseDown(object sender, MouseEventArgs e) {
            pos_ = e.Location;
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  メニューボタンの外観の調整
        /* ----------------------------------------------------------------- */
        #region Icon settings for MenuToolStrip

        /* ----------------------------------------------------------------- */
        /// OpenButton_MouseEnter
        /* ----------------------------------------------------------------- */
        private void OpenButton_MouseEnter(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.open_over;
        }

        /* ----------------------------------------------------------------- */
        /// OpenButton_MouseLeave
        /* ----------------------------------------------------------------- */
        private void OpenButton_MouseLeave(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.open;
        }

        /* ----------------------------------------------------------------- */
        /// OpenButton_MouseDown
        /* ----------------------------------------------------------------- */
        private void OpenButton_MouseDown(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.open_press;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OpenButton_MouseUp
        /// 
        /// <summary>
        /// MEMO: 開くのようにボタンを押すことで何らかのダイアログが
        /// 出るタイプのものは，ボタンの状態が戻らない事がある．
        /// そのため，マウスオーバー状態のものではなく，デフォルトの
        /// ボタンに戻す．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void OpenButton_MouseUp(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.open;
        }

        /* ----------------------------------------------------------------- */
        /// PrintButton_MouseEnter
        /* ----------------------------------------------------------------- */
        private void PrintButton_MouseEnter(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.print_over;
        }

        /* ----------------------------------------------------------------- */
        /// PrintButton_MouseLeave
        /* ----------------------------------------------------------------- */
        private void PrintButton_MouseLeave(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.print;
        }

        /* ----------------------------------------------------------------- */
        /// PrintButton_MouseDown
        /* ----------------------------------------------------------------- */
        private void PrintButton_MouseDown(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.print_press;
        }

        /* ----------------------------------------------------------------- */
        /// PrintButton_MouseUp
        /* ----------------------------------------------------------------- */
        private void PrintButton_MouseUp(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.print_over;
        }

        /* ----------------------------------------------------------------- */
        /// FirstPageButton_MouseEnter
        /* ----------------------------------------------------------------- */
        private void FirstPageButton_MouseEnter(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.arrow_first_over;
        }

        /* ----------------------------------------------------------------- */
        /// FirstPageButton_MouseLeave
        /* ----------------------------------------------------------------- */
        private void FirstPageButton_MouseLeave(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.arrow_first;
        }

        /* ----------------------------------------------------------------- */
        /// FirstPageButton_MouseDown
        /* ----------------------------------------------------------------- */
        private void FirstPageButton_MouseDown(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.arrow_first_press;
        }

        /* ----------------------------------------------------------------- */
        /// FirstPageButton_MouseUp
        /* ----------------------------------------------------------------- */
        private void FirstPageButton_MouseUp(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.arrow_first_over;
        }

        /* ----------------------------------------------------------------- */
        /// PreviousPageButton_MouseEnter
        /* ----------------------------------------------------------------- */
        private void PreviousPageButton_MouseEnter(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.arrow_prev_over;
        }

        /* ----------------------------------------------------------------- */
        /// PreviousPageButton_MouseLeave
        /* ----------------------------------------------------------------- */
        private void PreviousPageButton_MouseLeave(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.arrow_prev;
        }

        /* ----------------------------------------------------------------- */
        /// PreviousPageButton_MouseDown
        /* ----------------------------------------------------------------- */
        private void PreviousPageButton_MouseDown(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.arrow_prev_press;
        }

        /* ----------------------------------------------------------------- */
        /// PreviousPageButton_MouseUp
        /* ----------------------------------------------------------------- */
        private void PreviousPageButton_MouseUp(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.arrow_prev_over;
        }

        /* ----------------------------------------------------------------- */
        /// NextPageButton_MouseEnter
        /* ----------------------------------------------------------------- */
        private void NextPageButton_MouseEnter(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.arrow_next_over;
        }

        /* ----------------------------------------------------------------- */
        /// NextPageButton_MouseLeave
        /* ----------------------------------------------------------------- */
        private void NextPageButton_MouseLeave(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.arrow_next;
        }

        /* ----------------------------------------------------------------- */
        /// NextPageButton_MouseDown
        /* ----------------------------------------------------------------- */
        private void NextPageButton_MouseDown(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.arrow_next_press;
        }

        /* ----------------------------------------------------------------- */
        /// NextPageButton_MouseUp
        /* ----------------------------------------------------------------- */
        private void NextPageButton_MouseUp(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.arrow_next_over;
        }

        /* ----------------------------------------------------------------- */
        /// LastPageButton_MouseEnter
        /* ----------------------------------------------------------------- */
        private void LastPageButton_MouseEnter(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.arrow_last_over;
        }

        /* ----------------------------------------------------------------- */
        /// LastPageButton_MouseLeave
        /* ----------------------------------------------------------------- */
        private void LastPageButton_MouseLeave(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.arrow_last;
        }

        /* ----------------------------------------------------------------- */
        /// LastPageButton_MouseDown
        /* ----------------------------------------------------------------- */
        private void LastPageButton_MouseDown(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.arrow_last_press;
        }

        /* ----------------------------------------------------------------- */
        /// LastPageButton_MouseUp
        /* ----------------------------------------------------------------- */
        private void LastPageButton_MouseUp(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.arrow_last_over;
        }

        /* ----------------------------------------------------------------- */
        /// SearchButton_MouseEnter
        /* ----------------------------------------------------------------- */
        private void SearchButton_MouseEnter(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.search_over;
        }

        /* ----------------------------------------------------------------- */
        /// SearchButton_MouseLeave
        /* ----------------------------------------------------------------- */
        private void SearchButton_MouseLeave(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.search;
        }

        /* ----------------------------------------------------------------- */
        /// SearchButton_MouseDown
        /* ----------------------------------------------------------------- */
        private void SearchButton_MouseDown(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.search_press;
        }

        /* ----------------------------------------------------------------- */
        /// SearchButton_MouseUp
        /* ----------------------------------------------------------------- */
        private void SearchButton_MouseUp(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.search_over;
        }

        /* ----------------------------------------------------------------- */
        /// ZoomInButton_MouseEnter
        /* ----------------------------------------------------------------- */
        private void ZoomInButton_MouseEnter(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.zoomin_over;
        }

        /* ----------------------------------------------------------------- */
        /// ZoomInButton_MouseLeave
        /* ----------------------------------------------------------------- */
        private void ZoomInButton_MouseLeave(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.zoomin;
        }

        /* ----------------------------------------------------------------- */
        /// ZoomInButton_MouseDown
        /* ----------------------------------------------------------------- */
        private void ZoomInButton_MouseDown(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.zoomin_press;
        }

        /* ----------------------------------------------------------------- */
        /// ZoomInButton_MouseUp
        /* ----------------------------------------------------------------- */
        private void ZoomInButton_MouseUp(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.zoomin_over;
        }

        /* ----------------------------------------------------------------- */
        /// ZoomOutButton_MouseEnter
        /* ----------------------------------------------------------------- */
        private void ZoomOutButton_MouseEnter(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.zoomout_over;
        }

        /* ----------------------------------------------------------------- */
        /// ZoomOutButton_MouseLeave
        /* ----------------------------------------------------------------- */
        private void ZoomOutButton_MouseLeave(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.zoomout;
        }

        /* ----------------------------------------------------------------- */
        /// ZoomOutButton_MouseDown
        /* ----------------------------------------------------------------- */
        private void ZoomOutButton_MouseDown(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.zoomout_press;
        }

        /* ----------------------------------------------------------------- */
        /// ZoomOutButton_MouseUp
        /* ----------------------------------------------------------------- */
        private void ZoomOutButton_MouseUp(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.zoomout_over;
        }

        /* ----------------------------------------------------------------- */
        /// FitToWidthButton_MouseEnter
        /* ----------------------------------------------------------------- */
        private void FitToWidthButton_MouseEnter(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.fit2width_over;
        }

        /* ----------------------------------------------------------------- */
        /// FitToWidthButton_MouseLeave
        /* ----------------------------------------------------------------- */
        private void FitToWidthButton_MouseLeave(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = control.Checked ? Properties.Resources.fit2width_over : Properties.Resources.fit2width;
        }

        /* ----------------------------------------------------------------- */
        /// FitToWidthButton_MouseDown
        /* ----------------------------------------------------------------- */
        private void FitToWidthButton_MouseDown(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.fit2width_press;
        }

        /* ----------------------------------------------------------------- */
        /// FitToWidthButton_MouseUp
        /* ----------------------------------------------------------------- */
        private void FitToWidthButton_MouseUp(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.fit2width_over;
        }

        /* ----------------------------------------------------------------- */
        /// FitToWidthButton_CheckedChanged
        /* ----------------------------------------------------------------- */
        private void FitToWidthButton_CheckedChanged(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = control.Checked ? Properties.Resources.fit2width_over : Properties.Resources.fit2width;
        }

        /* ----------------------------------------------------------------- */
        /// FitToHeightButton_MouseEnter
        /* ----------------------------------------------------------------- */
        private void FitToHeightButton_MouseEnter(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.fit2height_over;
        }

        /* ----------------------------------------------------------------- */
        /// FitToHeightButton_MouseLeave
        /* ----------------------------------------------------------------- */
        private void FitToHeightButton_MouseLeave(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = control.Checked ? Properties.Resources.fit2height_over : Properties.Resources.fit2height;
        }

        /* ----------------------------------------------------------------- */
        /// FitToHeightButton_MouseDown
        /* ----------------------------------------------------------------- */
        private void FitToHeightButton_MouseDown(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.fit2height_press;
        }

        /* ----------------------------------------------------------------- */
        /// FitToHeightButton_MouseUp
        /* ----------------------------------------------------------------- */
        private void FitToHeightButton_MouseUp(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.fit2height_over;
        }

        /* ----------------------------------------------------------------- */
        /// FitToHeightButton_CheckedChanged
        /* ----------------------------------------------------------------- */
        private void FitToHeightButton_CheckedChanged(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = control.Checked ? Properties.Resources.fit2height_over : Properties.Resources.fit2height;
        }

        /* ----------------------------------------------------------------- */
        /// LogoButton_MouseEnter
        /* ----------------------------------------------------------------- */
        private void LogoButton_MouseEnter(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.logo_over;
        }

        /* ----------------------------------------------------------------- */
        /// LogoButton_MouseLeave
        /* ----------------------------------------------------------------- */
        private void LogoButton_MouseLeave(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.logo;
        }

        /* ----------------------------------------------------------------- */
        /// LogoButton_MouseDown
        /* ----------------------------------------------------------------- */
        private void LogoButton_MouseDown(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.logo_press;
        }

        /* ----------------------------------------------------------------- */
        /// LogoButton_MouseUp
        /* ----------------------------------------------------------------- */
        private void LogoButton_MouseUp(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.Image = Properties.Resources.logo;
        }

        /* ----------------------------------------------------------------- */
        /// MenuModeButton_MouseEnter
        /* ----------------------------------------------------------------- */
        private void MenuModeButton_MouseEnter(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.BackgroundImage = Properties.Resources.hidemenu_over;
        }

        /* ----------------------------------------------------------------- */
        /// MenuModeButton_MouseLeave
        /* ----------------------------------------------------------------- */
        private void MenuModeButton_MouseLeave(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.BackgroundImage = Properties.Resources.hidemenu;
        }

        /* ----------------------------------------------------------------- */
        /// ThumbButton_MouseEnter
        /* ----------------------------------------------------------------- */
        private void ThumbButton_MouseEnter(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.BackgroundImage = Properties.Resources.thumbnail_over;
        }

        /* ----------------------------------------------------------------- */
        /// ThumbButton_MouseLeave
        /* ----------------------------------------------------------------- */
        private void ThumbButton_MouseLeave(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            control.BackgroundImage = Properties.Resources.thumbnail;
        }

        /* ----------------------------------------------------------------- */
        /// ThumbButton_MouseDown
        /* ----------------------------------------------------------------- */
        private void ThumbButton_MouseDown(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.BackgroundImage = Properties.Resources.thumbnail_press;
        }

        /* ----------------------------------------------------------------- */
        /// ThumbButton_MouseUp
        /* ----------------------------------------------------------------- */
        private void ThumbButton_MouseUp(object sender, MouseEventArgs e) {
            var control = (ToolStripButton)sender;
            control.BackgroundImage = Properties.Resources.thumbnail_over;
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  メンバ変数の定義
        /* ----------------------------------------------------------------- */
        #region Member variables
        private bool begin_ = true;
        private FitCondition fit_ = FitCondition.Height;
        private int wheel_counter_ = 0;
        private Point pos_;
        #endregion
    }
}
