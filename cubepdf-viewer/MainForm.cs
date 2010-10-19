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
 *  Last-modified: Mon 18 Oct 2010 18:40:00 JST
 */
/* ------------------------------------------------------------------------- */
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Diagnostics;

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

            var exec = System.Reflection.Assembly.GetEntryAssembly();
            var dir = System.IO.Path.GetDirectoryName(exec.Location);
            var path = dir + @"\cubepdf-viewer.log";
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            Utility.SetupLog(path);

            int x = Screen.PrimaryScreen.Bounds.Height - 100;
            this.Size = (setting_.Size.Width > 0 && setting_.Size.Height > 0) ?
                setting_.Size : new Size(System.Math.Max(x, this.MinimumSize.Width), x);
            this.StartPosition = FormStartPosition.Manual;
            var pos = new Point(Math.Min(setting_.Position.X, Screen.PrimaryScreen.Bounds.Width),
                Math.Min(setting_.Position.Y, Screen.PrimaryScreen.Bounds.Height));
            this.Location = pos;

            this.MenuToolStrip.Renderer = new CustomToolStripRenderer();
            this.MenuSplitContainer.SplitterDistance = this.MenuToolStrip.Height;
            this.NavigationSplitContainer.Panel1Collapsed = (setting_.Navigaion == NavigationCondition.None);
            this.NavigationSplitContainer.SplitterDistance = Math.Max(setting_.ThumbWidth, this.NavigationSplitContainer.Panel1MinSize);

            this.UpdateFitCondition(setting_.Fit);
            CreateTabContextMenu(this.PageViewerTabControl);

            this.DefaultTabPage.MouseWheel += new MouseEventHandler(TabPage_MouseWheel);

            if (setting_.UseAdobeExtension) {
                InitializeAdobe();
                if (adobe_.Length > 0) {
                    this.AdobeButton.Enabled = true;
                    this.AdobeButton.Visible = true;
                }
            }
        }

        /* ----------------------------------------------------------------- */
        /// UpdateFitCondtion
        /* ----------------------------------------------------------------- */
        private void UpdateFitCondition(FitCondition which) {
            setting_.Fit = which;
            this.FitToWidthButton.Checked = ((setting_.Fit & FitCondition.Width) != 0);
            this.FitToHeightButton.Checked = ((setting_.Fit & FitCondition.Height) != 0);
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
                vsb.SmallChange = Math.Max(1, (vsb.Maximum - vsb.LargeChange) / 20);
                hsb.SmallChange = Math.Max(1, (hsb.Maximum - hsb.LargeChange) / 20);
            }

            if (this.MainMenuStrip != null) this.MainMenuStrip.Refresh();
        }

        /* ----------------------------------------------------------------- */
        /// Open
        /* ----------------------------------------------------------------- */
        private void Open(TabPage tab, string path, string password) {
            var canvas = CanvasPolicy.Create(tab);
            var message = "";

            try {
                CanvasPolicy.Open(canvas, path, password, setting_.Fit);
                if (!this.NavigationSplitContainer.Panel1Collapsed) {
                    this.CreateThumbnail(canvas);
                }
            }
            catch (System.Security.SecurityException /* err */) {
                PasswordDialog dialog = new PasswordDialog(path);
                dialog.ShowDialog();
                if (dialog.Password.Length > 0) this.Open(tab, path, dialog.Password);
            }
            catch (Exception err) {
                Utility.ErrorLog(err);
                message = err.Message;
            }
            finally {
                this.Refresh(canvas, message);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Open
        /// 
        /// <summary>
        /// 開く前に，ファイルを既に開いているかどうかのチェックを行う．
        /// 既にファイルを開いていた場合は，そのタブをアクティブにして
        /// 終了する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void Open(TabControl control, string path, string password = "") {
            foreach (TabPage item in control.TabPages) {
                var s = item.Tag as string;
                if (s != null && s == path) {
                    this.PageViewerTabControl.SelectedTab = item;
                    return;
                }
            }
            this.Open(this.CreateTab(control), path, password);
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
                if (prev >= CanvasPolicy.PageCount(canvas)) return true;
                if (CanvasPolicy.NextPage(canvas) == prev) status = false;
                this.RefreshThumbnail(this.NavigationSplitContainer.Panel1, CanvasPolicy.CurrentPage(canvas), prev);
                this.Refresh(canvas, message);
            }
            catch (Exception err) {
                Utility.ErrorLog(err);
                status = false;
                message = err.Message;
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
                if (prev <= 1) return true;
                if (CanvasPolicy.PreviousPage(canvas) == prev) status = false;
                this.RefreshThumbnail(this.NavigationSplitContainer.Panel1, CanvasPolicy.CurrentPage(canvas), prev);
                this.Refresh(canvas, message);
            }
            catch (Exception err) {
                Utility.ErrorLog(err);
                status = false;
                message = err.Message;
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
                Utility.ErrorLog(err);
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
                else CanvasPolicy.Adjust(canvas);
            }
            catch (Exception err) {
                Utility.ErrorLog(err);
                message = err.Message;
            }
            finally {
                this.Refresh(canvas, message);
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CreateTab
        /// 
        /// <summary>
        /// 新しい「空のタブ」を生成する．
        /// 「空のタブ」は，同時には 1 つしか存在できないようにしている．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public TabPage CreateTab(TabControl parent) {
            foreach (TabPage item in parent.TabPages) {
                if (item.Tag == null) {
                    parent.SelectedTab = item;
                    return item;
                }
            }

            var tab = new TabPage();

            // TabPage の設定
            tab.AllowDrop = true;
            tab.AutoScroll = true;
            tab.BackColor = Color.DimGray;
            tab.BorderStyle = BorderStyle.None;
            tab.ContextMenuStrip = new ContextMenuStrip();
            tab.Text = "(無題)";
            tab.Scroll += new ScrollEventHandler(TabPage_Scroll);
            tab.DragEnter += new DragEventHandler(TabPage_DragEnter);
            tab.DragDrop += new DragEventHandler(TabPage_DragDrop);
            tab.MouseWheel += new MouseEventHandler(TabPage_MouseWheel);
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
            this.DestroyThumbnail(this.NavigationSplitContainer.Panel1);
            CanvasPolicy.Destroy(canvas);
            if (this.PageViewerTabControl.TabCount > 1) {
                parent.TabPages.Remove(tab);
                tab.Dispose();
            }
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
            elem.Click -= new EventHandler(TabCloseMenuItem_Click);
            elem.Click += new EventHandler(TabCloseMenuItem_Click);
            menu.Items.Add(elem);
            parent.MouseDown -= new MouseEventHandler(PageViewerTabControl_MouseDown);
            parent.MouseDown += new MouseEventHandler(PageViewerTabControl_MouseDown);
            parent.ContextMenuStrip = menu;

            foreach (TabPage child in parent.TabPages) {
                child.ContextMenuStrip = new ContextMenuStrip();
            }
        }

        /* ----------------------------------------------------------------- */
        /// CreateThumbnail
        /* ----------------------------------------------------------------- */
        private void CreateThumbnail(PictureBox canvas) {
            this.DestroyThumbnail(this.NavigationSplitContainer.Panel1);
            Thumbnail thumb = new Thumbnail(this.NavigationSplitContainer.Panel1, canvas);
            thumb.SelectedIndexChanged -= new EventHandler(Thumbnail_SelectedIndexChanged);
            thumb.SelectedIndexChanged += new EventHandler(Thumbnail_SelectedIndexChanged);
        }

        /* ----------------------------------------------------------------- */
        /// DestroyThumbnail
        /* ----------------------------------------------------------------- */
        private void DestroyThumbnail(Control parent) {
            var thumb = Thumbnail.GetInstance(parent);
            if (thumb == null) return;
            parent.Controls.Remove(thumb);
            thumb.Dispose();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// RefreshThumbnail
        /// 
        /// <summary>
        /// 選択ページを表す枠線を再描画する．index0, index1 がそれぞれ
        /// 遷移元ページ，現在の選択ページに対応するインデックスを表す．
        /// 選択ページについては，Selected プロパティを更新する事によって
        /// DrawItem イベントが自動的に発生する模様．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void RefreshThumbnail(Control parent, int current, int previous) {
            var thumb = Thumbnail.GetInstance(parent);
            if (thumb == null) return;

            var index0 = Math.Min(Math.Max(previous - 1, 0), thumb.Items.Count);
            var index1 = Math.Min(Math.Max(current - 1, 0), thumb.Items.Count);
            thumb.Invalidate(thumb.Items[index0].Bounds);
            thumb.Items[index1].Selected = true;
        }

        /* ----------------------------------------------------------------- */
        /// WndProc
        /* ----------------------------------------------------------------- */
        protected override void WndProc(ref Message m) {
            try {
                switch (m.Msg) {
                case Program.WM_COPYDATA:
                    // 既にプログラムが起動している場合に PDF ファイルが
                    // ダブルクリックされた場合，起動しているプログラムに
                    // 新たなタブを生成して描画する．
                    var mystr = new Program.COPYDATASTRUCT();
                    mystr = (Program.COPYDATASTRUCT)m.GetLParam(typeof(Program.COPYDATASTRUCT));
                    var path = mystr.lpData;
                    this.Open(this.PageViewerTabControl, path);
                    break;
                default:
                    break;
                }
            }
            catch (Exception err) {
                Utility.ErrorLog(err);
            }
            finally {
                base.WndProc(ref m);
            }
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
                this.Open(this.PageViewerTabControl, path);
                this.Tag = null;
            }
        }

        /* ----------------------------------------------------------------- */
        ///  MainForm_FormClosing
        /* ----------------------------------------------------------------- */
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            setting_.Position = this.Location;
            setting_.Size = this.Size;
            setting_.ThumbWidth = this.NavigationSplitContainer.SplitterDistance;
            setting_.Save();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MainForm_Resize
        /// 
        /// <summary>
        /// サムネイル画像の最大サイズを指定したいが，各パネルには MinSize
        /// しか設定できないため，メイン画面側のパネルの MinSize を動的に
        /// 変更する事で対応する．
        /// 
        /// MEMO: Panel2MinSize を動的に変更すると，ウィンドウを縮小した
        /// ときにうまく位置調整されない（原因は不明）．今のところは
        /// 無効にしておく．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void MainForm_Resize(object sender, EventArgs e) {
            //this.NavigationSplitContainer.Panel2MinSize = this.NavigationSplitContainer.Width - 256;
            if ((resize_ & 0x01) == 0) this.Adjust(this.PageViewerTabControl.SelectedTab);
            resize_ |= 0x02;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// MainForm_ResizeBegin
        /// 
        /// <summary>
        /// ドラッグ形式のリサイズの場合，リアルタイムに変更すると動作が
        /// 重くなるため，ドラッグ中はリサイズを行わないようにする．
        /// また，サムネイル画像のちらつきを抑えるため，サムネイル画像
        /// への背景消去 (WM_ERASEBKGND) メッセージを無効にする．
        /// 
        /// MEMO: ユーザの操作と発生するイベント
        ///  1. ドラッグによるのリサイズ: ReizeBegin -> Resize -> ResizeEnd
        ///  2. 最大化: Resize
        ///  3. ドラッグによる移動: ResizeBegin -> ResizeEnd
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void MainForm_ResizeBegin(object sender, EventArgs e) {
            resize_ = 0;
            resize_ |= 0x01;
            var thumb = Thumbnail.GetInstance(this.NavigationSplitContainer.Panel1);
            if (thumb != null) thumb.EraseBackground = false;
        }

        /* ----------------------------------------------------------------- */
        /// MainForm_ResizeEnd
        /* ----------------------------------------------------------------- */
        private void MainForm_ResizeEnd(object sender, EventArgs e) {
            if ((resize_ & 0x02) != 0) this.Adjust(this.PageViewerTabControl.SelectedTab);
            resize_ = 0;
            var thumb = Thumbnail.GetInstance(this.NavigationSplitContainer.Panel1);
            if (thumb != null) {
                thumb.EraseBackground = true;
                thumb.Invalidate();
            }
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
                this.Open(this.PageViewerTabControl, dialog.FileName);
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
        ///
        /// PrintButton_Click
        /// 
        /// <summary> 
        /// TODO: PaintEventHandler の解除と各種レンダリングに必要な
        /// プロパティを保存し，印刷終了後元に戻す．
        /// TODO: PDFLibNet.PDFWrapper を CanvasPolicy.cs の中に隠したい．
        /// PrintDocument.PrintPage イベントハンドラに PDFLibNet.PDFWrapper
        /// を渡す方法を検討する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void PrintButton_Click(object sender, EventArgs e) {
            var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
            if (canvas == null || canvas.Tag == null) return;

            // Print するためには，いったん PDFWrapper の値を変える必要がある．
            var core = canvas.Tag as PDFLibNet.PDFWrapper;
            var settings = new { page = core.CurrentPage, zoom = core.Zoom };

            using (var prd = new PrintDialog())
            using (var document = new System.Drawing.Printing.PrintDocument()) {
                prd.AllowCurrentPage = true;
                prd.AllowSelection = false; // ページを選択する方法を提供しないのでfalse
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
                    core.CurrentPage = settings.page;
                    core.Zoom = settings.zoom;
                    core.RenderPage(IntPtr.Zero, false, false);
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
            var core = canvas.Tag as PDFLibNet.PDFWrapper;

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
                else ev.HasMorePages = false;
            }
            else if (ev.PageSettings.PrinterSettings.PrintRange == PrintRange.SomePages) {
                if (core.CurrentPage < ev.PageSettings.PrinterSettings.ToPage) {
                    core.NextPage();
                    ev.HasMorePages = true;
                }
                else ev.HasMorePages = false;
            }
            else ev.HasMorePages = false;
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
                Utility.ErrorLog(err);
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
                Utility.ErrorLog(err);
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
                Utility.ErrorLog(err);
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
                Utility.ErrorLog(err);
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
                Utility.ErrorLog(err);
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
                var prev = CanvasPolicy.CurrentPage(canvas);
                if (prev <= 1) return;
                if (CanvasPolicy.FirstPage(canvas) == prev) return;
                this.RefreshThumbnail(this.NavigationSplitContainer.Panel1, CanvasPolicy.CurrentPage(canvas), prev);
                this.Refresh(canvas, message);
            }
            catch (Exception err) {
                Utility.ErrorLog(err);
                message = err.Message;
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
                var prev = CanvasPolicy.CurrentPage(canvas);
                if (prev >= CanvasPolicy.PageCount(canvas)) return;
                if (CanvasPolicy.LastPage(canvas) == prev) return;
                this.RefreshThumbnail(this.NavigationSplitContainer.Panel1, CanvasPolicy.CurrentPage(canvas), prev);
                this.Refresh(canvas, message);
            }
            catch (Exception err) {
                Utility.ErrorLog(err);
                message = err.Message;
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
                    var page = int.Parse(control.Text);
                    var prev = CanvasPolicy.CurrentPage(canvas);
                    if (page == prev) return;
                    if (CanvasPolicy.MovePage(canvas, page) == prev) return;
                    this.RefreshThumbnail(this.NavigationSplitContainer.Panel1, CanvasPolicy.CurrentPage(canvas), prev);
                    this.Refresh(canvas, message);
                }
                catch (Exception err) {
                    Utility.ErrorLog(err);
                    message = err.Message;
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
            if (!this.MenuSplitContainer.Panel1Collapsed && setting_.ShowMenuInfo) {
                var dialog = new MenuDialog();
                dialog.ShowDialog(this);
                setting_.ShowMenuInfo = !dialog.NoShowNext;
            }

            this.MenuSplitContainer.Panel1Collapsed = !this.MenuSplitContainer.Panel1Collapsed;
            this.Adjust(this.PageViewerTabControl.SelectedTab);
        }

        /* ----------------------------------------------------------------- */
        /// ThumbButton_Click
        /* ----------------------------------------------------------------- */
        private void ThumbButton_Click(object sender, EventArgs e) {
            this.NavigationSplitContainer.Panel1Collapsed = !this.NavigationSplitContainer.Panel1Collapsed;
            if (!this.NavigationSplitContainer.Panel1Collapsed) {
                setting_.Navigaion = NavigationCondition.Thumbnail;
                var control = this.NavigationSplitContainer.Panel1;
                if (Thumbnail.GetInstance(control) == null) {
                    var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
                    if (canvas == null) return;
                    this.CreateThumbnail(canvas);
                }
            }
            else setting_.Navigaion = NavigationCondition.None;
            this.Adjust(this.PageViewerTabControl.SelectedTab);
        }

        /* ----------------------------------------------------------------- */
        /// LogoButton_Click
        /* ----------------------------------------------------------------- */
        private void LogoButton_Click(object sender, EventArgs e) {
            System.Diagnostics.Process.Start("http://www.cube-soft.jp/");
        }

        /* ----------------------------------------------------------------- */
        /// NavigationSplitContainer_SplitterMoved
        /* ----------------------------------------------------------------- */
        private void NavigationSplitContainer_SplitterMoved(object sender, SplitterEventArgs e) {
            this.Adjust(this.PageViewerTabControl.SelectedTab);
        }

        /* ----------------------------------------------------------------- */
        /// TabPage_Scroll
        /* ----------------------------------------------------------------- */
        private void TabPage_Scroll(object sender, ScrollEventArgs e) {
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
        ///
        /// TabPage_MouseWheel
        /// 
        /// <summary>
        /// マウスホイールによるスクロールの処理．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void TabPage_MouseWheel(object sender, MouseEventArgs e) {
            if (Math.Abs(e.Delta) < 120) return;

            var tab = this.PageViewerTabControl.SelectedTab;
            var scroll = tab.VerticalScroll;
            if (!scroll.Visible) {
                if (e.Delta < 0) this.NextPage(tab);
                else this.PreviousPage(tab);
            }
            else {
                var canvas = CanvasPolicy.Get(tab);
                if (canvas == null) return;

                var maximum = 1 + scroll.Maximum - scroll.LargeChange; // ユーザのコントロールで取れる scroll.Value の最大値
                var delta = -(e.Delta / 120) * scroll.SmallChange;
                if (scroll.Value == scroll.Minimum && delta < 0) {
                    if (wheel_counter_ > 1) {
                        if (CanvasPolicy.CurrentPage(canvas) > 1 && this.PreviousPage(tab)) {
                            tab.AutoScrollPosition = new Point(0, maximum);
                        }
                        wheel_counter_ = 0;
                    }
                    else wheel_counter_++;
                }
                else if (scroll.Value == maximum && delta > 0) {
                    if (wheel_counter_ > 1) {
                        if (CanvasPolicy.CurrentPage(canvas) < CanvasPolicy.PageCount(canvas) && this.NextPage(tab)) {
                            tab.AutoScrollPosition = new Point(0, 0);
                        }
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

            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (var path in files) {
                    if (System.IO.Path.GetExtension(path).ToLower() != ".pdf") continue;
                    this.Open(control, path);
                }
            }
        }

        /* ----------------------------------------------------------------- */
        /// PageViewerTabControl_TabClosing
        /* ----------------------------------------------------------------- */
        private void PageViewerTabControl_TabClosing(object sender, TabControlCancelEventArgs e) {
            var control = (CustomTabControl)sender;
            var index = control.SelectedIndex;
            if (control.TabCount <= 1) e.Cancel = true;
            else if (e.TabPageIndex == index) control.SelectedIndex = Math.Max(index - 1, 0);
            this.DestroyTab(e.TabPage);
        }

        /* ----------------------------------------------------------------- */
        /// PageViewerTabControl_MouseDown
        /* ----------------------------------------------------------------- */
        private void PageViewerTabControl_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                var control = sender as TabControl;
                if (control == null || control.ContextMenuStrip == null) return;
                var context = control.ContextMenuStrip;
                context.Tag = e.Location;
            }
        }

        /* ----------------------------------------------------------------- */
        /// Thumbnail_SelectedIndexChanged
        /* ----------------------------------------------------------------- */
        private void Thumbnail_SelectedIndexChanged(object sender, EventArgs e) {
            var thumb = sender as Thumbnail;
            if (thumb == null || thumb.SelectedItems.Count == 0) return;
            var page = thumb.SelectedItems[0].Index + 1;

            var tab = this.PageViewerTabControl.SelectedTab;
            var canvas = CanvasPolicy.Get(tab);
            CanvasPolicy.MovePage(canvas, page);
            this.Refresh(canvas);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TabCloseMenuItem_Click
        /// 
        /// <summary>
        /// コンテキストメニューの「閉じる」が押された時のイベントハンドラ．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void TabCloseMenuItem_Click(object sender, EventArgs e) {
            try {
                var control = this.PageViewerTabControl;
                var item = sender as ToolStripMenuItem;
                var context = item.Owner as ContextMenuStrip;
                if (control == null || context == null) return;
                
                var pos = (Point)context.Tag;
                var index = control.SelectedIndex;
                for (int i = 0; i < control.TabCount; i++) {
                    var rect = control.GetTabRect(i);
                    if (pos.X > rect.Left && pos.X < rect.Right && pos.Y > rect.Top && pos.Y < rect.Bottom) {
                        if (i == index) control.SelectedIndex = Math.Max(index - 1, 0);
                        TabPage tab = control.TabPages[i];
                        this.DestroyTab(tab);
                        break;
                    }
                }
            }
            catch (Exception err) {
                Utility.ErrorLog(err);
            }
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
        // Adobe Reader との連携関係
        /* ----------------------------------------------------------------- */
        #region Adobe extensions

        private Image adobe_icon_ = null;
        private Image adobe_icon_selected_ = null;

        /* ----------------------------------------------------------------- */
        /// InitializeAdobe
        /* ----------------------------------------------------------------- */
        private void InitializeAdobe() {
            adobe_ = "";

            var registry = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\Adobe\Acrobat Reader");
            if (registry == null) registry = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\Adobe\Adobe Acrobat");
            if (registry == null) return;

            string version = "";
            foreach (var elem in registry.GetSubKeyNames()) {
                try {
                    var x = double.Parse(elem);
                    if (version.Length == 0 || x > double.Parse(version)) version = elem;
                }
                catch (Exception err) {
                    Utility.ErrorLog(err);
                }
            }
            if (version.Length == 0) return;

            var subkey = registry.OpenSubKey(version + @"\InstallPath");
            if (subkey == null) return;

            var path = (string)subkey.GetValue("");
            if (path == null) return;

            adobe_ = path + @"\AcroRd32.exe";

            var original = Utility.GetIcon(adobe_).ToBitmap();

            adobe_icon_ = new Bitmap(32, 32);
            var normal = Graphics.FromImage(adobe_icon_);
            normal.DrawImage(original, 4, 4, adobe_icon_.Width - 8, adobe_icon_.Height - 8);
            normal.Dispose();

            adobe_icon_selected_ = new Bitmap(32, 32);
            var selected = Graphics.FromImage(adobe_icon_selected_);
            selected.FillRectangle(SystemBrushes.GradientActiveCaption, new Rectangle(0, 0, adobe_icon_selected_.Width - 2, adobe_icon_selected_.Height - 2));
            selected.DrawImage(original, 4, 4, adobe_icon_.Width - 8, adobe_icon_.Height - 8);
            selected.DrawRectangle(SystemPens.Highlight, new Rectangle(0, 0, adobe_icon_selected_.Width - 2, adobe_icon_selected_.Height - 2));
            selected.Dispose();

            original.Dispose();

            this.AdobeButton.MouseEnter += new EventHandler(AdobeButton_MouseEnter);
            this.AdobeButton.MouseLeave += new EventHandler(AdobeButton_MouseLeave);
            this.AdobeButton.Image = adobe_icon_;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OpenWithAdobe
        ///
        /// <summary>
        /// 現在，表示されている PDF ファイルを Adobe Reader で開く．
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void OpenWithAdobe(TabPage tab, string path) {
            if (path == null || adobe_ == null || adobe_.Length == 0) return;

            try {
                var info = new System.Diagnostics.ProcessStartInfo();
                info.FileName = adobe_;
                info.Arguments = path;
                info.CreateNoWindow = false;
                info.UseShellExecute = false;
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = info;
                proc.Start();
            }
            catch (Exception err) {
                Utility.ErrorLog(err);
            }
        }

        /* ----------------------------------------------------------------- */
        /// AdobeButton_Click
        /* ----------------------------------------------------------------- */
        private void AdobeButton_Click(object sender, EventArgs e) {
            var tab = this.PageViewerTabControl.SelectedTab;
            var path = (string)tab.Tag;
            this.OpenWithAdobe(tab, path);
        }

        /* ----------------------------------------------------------------- */
        /// AdobeButton_MouseEnter
        /* ----------------------------------------------------------------- */
        private void AdobeButton_MouseEnter(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            if (adobe_icon_selected_ != null) control.Image = adobe_icon_selected_;
        }

        /* ----------------------------------------------------------------- */
        /// AdobeButton_MouseLeave
        /* ----------------------------------------------------------------- */
        private void AdobeButton_MouseLeave(object sender, EventArgs e) {
            var control = (ToolStripButton)sender;
            if (adobe_icon_ != null) control.Image = adobe_icon_;
        }

        #endregion

        /* ----------------------------------------------------------------- */
        //  メンバ変数の定義
        /* ----------------------------------------------------------------- */
        #region Member variables
        private UserSetting setting_ = new UserSetting();
        private bool begin_ = true;
        private int wheel_counter_ = 0;
        static int resize_ = 0;
        private string adobe_;
        #endregion
    }
}
