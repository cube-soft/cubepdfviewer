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
            InitializeComponent();

            int x = Screen.PrimaryScreen.Bounds.Height - 100;
            this.Size = new Size(System.Math.Max(x, 800), x);
            this.NavigationSplitContainer.Panel1Collapsed = true;
            this.MenuSplitContainer.SplitterDistance = this.MenuToolStrip.Height;
            this.SubMenuSplitContainer.SplitterDistance = this.SubMenuToolStrip.Width;
            this.DefaultTabPage.VerticalScroll.SmallChange = 3;
            this.DefaultTabPage.HorizontalScroll.SmallChange = 3;
            this.FitToHeightButton.Checked = true;
            TabContextMenu(this.PageViewerTabControl);

            this.MouseEnter += new EventHandler(this.MainForm_MouseEnter);
            this.MouseWheel += new MouseEventHandler(this.MainForm_MouseWheel);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Refresh
        /// 
        /// <summary>
        /// システムの Refresh() を呼ぶ前に，必要な情報を全て更新する．
        /// MEMO: サムネイル画面を更新するとちらつきがひどいので，最小限の更新
        /// になるようにしている．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void Refresh(PictureBox canvas, string message = "") {
            if (canvas == null || canvas.Tag == null) {
                CurrentPageTextBox.Text = "0";
                TotalPageLabel.Text = "/ 0";
                ZoomDropDownButton.Text = "100%";
            }
            else {
                var core = (PDFLibNet.PDFWrapper)canvas.Tag;
                CurrentPageTextBox.Text = core.CurrentPage.ToString();
                TotalPageLabel.Text = "/ " + core.PageCount.ToString();
                ZoomDropDownButton.Text = ((int)core.Zoom).ToString() + "%";
                if (this.PageViewerTabControl != null) this.PageViewerTabControl.Refresh();
            }

            if (this.MainMenuStrip != null) this.MainMenuStrip.Refresh();
            if (this.FooterStatusStrip != null) this.FooterStatusStrip.Refresh();
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
        /// Adjust
        /* ----------------------------------------------------------------- */
        private void Adjust() {
            var tab = this.PageViewerTabControl.SelectedTab;
            var canvas = CanvasPolicy.Get(tab);
            if (this.FitToWidthButton.Checked) CanvasPolicy.FitToWidth(canvas);
            else if (this.FitToHeightButton.Checked) CanvasPolicy.FitToHeight(canvas);
            else CanvasPolicy.Adjust(canvas);
            this.Refresh(canvas);
        }

        /* ----------------------------------------------------------------- */
        /// CreateThumbnail
        /* ----------------------------------------------------------------- */
        private void CreateThumbnail(PictureBox canvas) {
            var old = CanvasPolicy.GetThumbnail(this.NavigationSplitContainer.Panel1);
            if (old != null) CanvasPolicy.DestroyThumbnail(old);
            ListView thumb = CanvasPolicy.CreateThumbnail(canvas, this.NavigationSplitContainer.Panel1, RenderThumbnailFinished);
            thumb.SelectedIndexChanged += new EventHandler(PageChanged);
        }

        /* -----------------------;------------------------------------------ */
        //  メインフォームに関する各種イベント・ハンドラ
        /* ----------------------------------------------------------------- */
        #region MainForm Event handlers

        /* ----------------------------------------------------------------- */
        /// MainForm_SizeChanged
        /* ----------------------------------------------------------------- */
        private void MainForm_SizeChanged(object sender, EventArgs e) {
            this.Adjust();
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
            var tab = this.PageViewerTabControl.SelectedTab;
            var scroll = tab.VerticalScroll;
            if (!scroll.Visible) return;
            
            int delta = -(e.Delta / 120) * scroll.SmallChange;
            if (scroll.Value + delta > scroll.Minimum &&
                scroll.Value + delta < scroll.Maximum) {
                scroll.Value += delta;
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
            CreateTab(this.PageViewerTabControl);
            this.Refresh(null);
        }

        /* ----------------------------------------------------------------- */
        /// FileButton_DropDownItemClicked
        /* ----------------------------------------------------------------- */
        private void FileButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            var control = (ToolStripSplitButton)sender;
            control.HideDropDown();
            if (e.ClickedItem.Name == "CloseMenuItem") {
                CloseButton_Click(sender, e);
                return;
            }

            if (e.ClickedItem.Name == "OpenNewTabMenuItem") {
                TabPage selected = null;
                foreach (TabPage child in this.PageViewerTabControl.TabPages) {
                    if (child.Controls["Canvas"] == null) { // 未使用タブ
                        selected = child;
                        child.Select();
                        break;
                    }
                }
                if (selected == null) CreateTab(this.PageViewerTabControl);
            }
            this.OpenButton_Click(sender, e);
        }

        /* ----------------------------------------------------------------- */
        /// OpenButton_Click
        /* ----------------------------------------------------------------- */
        private void OpenButton_Click(object sender, EventArgs e) {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK) {
                var tab = this.PageViewerTabControl.SelectedTab;
                var canvas = CanvasPolicy.Create(tab);
                CanvasPolicy.Open(canvas, dialog.FileName, fit_);
                this.CreateThumbnail(canvas);
                this.Refresh(canvas);
            }
        }


        /* ----------------------------------------------------------------- */
        /// CloseButton_Click
        /* ----------------------------------------------------------------- */
        private void CloseButton_Click(object sender, EventArgs e) {
            var tab = this.PageViewerTabControl.SelectedTab;
            var canvas = CanvasPolicy.Get(tab);
            var thumb = CanvasPolicy.GetThumbnail(this.NavigationSplitContainer.Panel1);
            if (thumb != null) CanvasPolicy.DestroyThumbnail(thumb);
            CanvasPolicy.Destroy(canvas);
            if (this.PageViewerTabControl.TabCount > 1) DestroyTab(tab);
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

            try {
                CanvasPolicy.ZoomIn(canvas);
            }
            catch (Exception /* err */) { }
            finally {
                this.Refresh(canvas);
            }
        }

        /* ----------------------------------------------------------------- */
        /// ZoomOutButton_Click
        /* ----------------------------------------------------------------- */
        private void ZoomOutButton_Click(object sender, EventArgs e) {
            this.UpdateFitCondition(FitCondition.None);
            var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
            if (canvas == null) return;

            try {
                CanvasPolicy.ZoomOut(canvas);
            }
            catch (Exception /* err */) { }
            finally {
                this.Refresh(canvas);
            }
        }

        /* ----------------------------------------------------------------- */
        /// ZoomDropDownButton_DropDownItemClicked
        /* ----------------------------------------------------------------- */
        private void ZoomDropDownButton_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
            this.UpdateFitCondition(FitCondition.None);
            var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
            if (canvas == null) return;

            try {
                var zoom = e.ClickedItem.Text.Replace("%", "");
                CanvasPolicy.Zoom(canvas, int.Parse(zoom));
            }
            catch (Exception /* err */) { }
            finally {
                this.Refresh(canvas);
            }
        }

        /* ----------------------------------------------------------------- */
        /// FitToWidthButton_Click
        /* ----------------------------------------------------------------- */
        private void FitToWidthButton_Click(object sender, EventArgs e) {
            this.UpdateFitCondition(this.FitToWidthButton.Checked ? FitCondition.Width : FitCondition.None);
            var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
            if (canvas == null) return;

            try {
                if (this.FitToWidthButton.Checked) CanvasPolicy.FitToWidth(canvas);
            }
            catch (Exception /* err */) { }
            finally {
                this.Refresh(canvas);
            }
        }

        /* ----------------------------------------------------------------- */
        /// FitToHeightButton_Click
        /* ----------------------------------------------------------------- */
        private void FitToHeightButton_Click(object sender, EventArgs e) {
            this.UpdateFitCondition(this.FitToHeightButton.Checked ? FitCondition.Height : FitCondition.None);
            var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
            if (canvas == null) return;

            try {
                if (this.FitToHeightButton.Checked) CanvasPolicy.FitToHeight(canvas);
            }
            catch (Exception /* err */) { }
            finally {
                this.Refresh(canvas);
            }
        }

        /* ----------------------------------------------------------------- */
        /// NextPageButton_Click
        /* ----------------------------------------------------------------- */
        private void NextPageButton_Click(object sender, EventArgs e) {
            var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
            if (canvas == null) return;

            try {
                CanvasPolicy.NextPage(canvas);
            }
            catch (Exception /* err */) { }
            finally {
                this.Refresh(canvas);
            }
        }

        /* ----------------------------------------------------------------- */
        /// PreviousPageButton_Click
        /* ----------------------------------------------------------------- */
        private void PreviousPageButton_Click(object sender, EventArgs e) {
            var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
            if (canvas == null) return;

            try {
                CanvasPolicy.PreviousPage(canvas);
            }
            catch (Exception /* err */) { }
            finally {
                this.Refresh(canvas);
            }
        }

        /* ----------------------------------------------------------------- */
        /// FirstPageButton_Click
        /* ----------------------------------------------------------------- */
        private void FirstPageButton_Click(object sender, EventArgs e) {
            var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
            if (canvas == null) return;

            try {
                CanvasPolicy.FirstPage(canvas);
            }
            catch (Exception /* err */) { }
            finally {
                this.Refresh(canvas);
            }
        }

        /* ----------------------------------------------------------------- */
        /// LastPageButton_Click
        /* ----------------------------------------------------------------- */
        private void LastPageButton_Click(object sender, EventArgs e) {
            var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
            if (canvas == null) return;

            try {
                CanvasPolicy.LastPage(canvas);
            }
            catch (Exception /* err */) { }
            finally {
                this.Refresh(canvas);
            }
        }

        /* ----------------------------------------------------------------- */
        /// CurrentPageTextBox_KeyDown
        /* ----------------------------------------------------------------- */
        private void CurrentPageTextBox_KeyDown(object sender, KeyEventArgs e) {
            var canvas = CanvasPolicy.Get(this.PageViewerTabControl.SelectedTab);
            if (canvas == null) return;

            if (e.KeyCode == Keys.Enter) {
                try {
                    var control = (ToolStripTextBox)sender;
                    int page = int.Parse(control.Text);
                    CanvasPolicy.MovePage(canvas, page);
                }
                catch (Exception /* err */) { }
                finally {
                    this.Refresh(canvas);
                }
            }
        }

        /* ----------------------------------------------------------------- */
        /// MenuModeButton_Click
        /* ----------------------------------------------------------------- */
        private void MenuModeButton_Click(object sender, EventArgs e) {
            this.MenuSplitContainer.Panel1Collapsed = !this.MenuSplitContainer.Panel1Collapsed;
            this.MenuModeButton.Image = this.MenuSplitContainer.Panel1Collapsed ?
                global::Cube.Properties.Resources.showmenu :
                global::Cube.Properties.Resources.hidemenu;
            this.Adjust();
        }

        /* ----------------------------------------------------------------- */
        /// ThumbButton_Click
        /* ----------------------------------------------------------------- */
        private void ThumbButton_Click(object sender, EventArgs e) {
            this.NavigationSplitContainer.Panel1Collapsed = !this.NavigationSplitContainer.Panel1Collapsed;
            this.Adjust();
        }

        /* ----------------------------------------------------------------- */
        /// RenderThumbnailFinished
        /* ----------------------------------------------------------------- */
        private void RenderThumbnailFinished(int page, bool successs) {
            Invoke(new PDFLibNet.RenderNotifyFinishedHandler(RenderThumbnailFinishedInvoke), page, successs);
        }

        /* ----------------------------------------------------------------- */
        /// RenderThumbnailFinishedInvoke
        /* ----------------------------------------------------------------- */
        private void RenderThumbnailFinishedInvoke(int page, bool successs) {
            var thumb = CanvasPolicy.GetThumbnail(this.NavigationSplitContainer.Panel1);
            if (thumb == null) return;

            if (!successs) thumb.Invalidate();
            else thumb.Invalidate(thumb.Items[page - 1].Bounds);
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

        #endregion

        /* ----------------------------------------------------------------- */
        //  メンバ変数の定義
        /* ----------------------------------------------------------------- */
        #region Member variables
        private FitCondition fit_ = FitCondition.Height;
        #endregion

        /* ----------------------------------------------------------------- */
        //  タブ関係の処理
        //  TODO: この部分は，依存部分を取り除いて抽象クラス辺りで分離したい．
        /* ----------------------------------------------------------------- */
        #region Tab controls

        /* ----------------------------------------------------------------- */
        /// Create
        /* ----------------------------------------------------------------- */
        public static TabPage CreateTab(TabControl parent) {
            var tab = new TabPage();

            // TabPage の設定
            tab.AutoScroll = true;
            tab.VerticalScroll.SmallChange = 3;
            tab.HorizontalScroll.SmallChange = 3;
            tab.BackColor = Color.DimGray;
            tab.BorderStyle = BorderStyle.Fixed3D;
            tab.ContextMenuStrip = new ContextMenuStrip();
            tab.Text = "(無題)";

            parent.Controls.Add(tab);
            parent.SelectedIndex = parent.TabCount - 1;

            return tab;
        }

        /* ----------------------------------------------------------------- */
        /// Destroy
        /* ----------------------------------------------------------------- */
        public static void DestroyTab(TabPage tab) {
            var parent = (TabControl)tab.Parent;
            CanvasPolicy.Destroy(CanvasPolicy.Get(tab));
            parent.TabPages.Remove(tab);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// ContextMenu
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
        public void TabContextMenu(TabControl parent) {
            var menu = new ContextMenuStrip();
            var elem = new ToolStripMenuItem();
            elem.Text = "閉じる";
            elem.Click += new EventHandler(TabCloseHandler);
            menu.Items.Add(elem);
            menu.Tag = parent; // 暫定
            parent.MouseDown += new MouseEventHandler(TabMouseDownHandler);
            parent.ContextMenuStrip = menu;

            foreach (TabPage child in parent.TabPages) {
                child.ContextMenuStrip = new ContextMenuStrip();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// CloseHandler (private)
        /// 
        /// <summary>
        /// コンテキストメニューの「閉じる」が押された時のイベントハンドラ．
        /// TODO: 「閉じる」が押されたときの座標を取得する方法．現状では，
        /// MouseDown イベントにもハンドラを設定しておき，その時の座標から
        /// 判断している．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        private void TabCloseHandler(object sender, EventArgs e) {
            var item = (ToolStripMenuItem)sender;
            var menu = (ContextMenuStrip)item.Owner;
            var control = (TabControl)menu.Tag;

            for (int i = 0; i < control.TabCount; i++) {
                var rect = control.GetTabRect(i);
                if (position_.X > rect.Left && position_.X < rect.Right &&
                    position_.Y > rect.Top && position_.Y < rect.Bottom) {
                    TabPage tab = control.TabPages[i];

                    // TODO: この部分が MainForm に依存している．この部分をうまく切り離す．
                    var thumb = CanvasPolicy.GetThumbnail(this.NavigationSplitContainer.Panel1);
                    if (thumb != null) CanvasPolicy.DestroyThumbnail(thumb);

                    CanvasPolicy.Destroy(CanvasPolicy.Get(tab));
                    if (control.TabCount > 1) DestroyTab(tab);
                    break;
                }
            }
        }

        /* ----------------------------------------------------------------- */
        /// MouseDownHandler (private)
        /* ----------------------------------------------------------------- */
        private static void TabMouseDownHandler(object sender, MouseEventArgs e) {
            position_ = e.Location;
        }

        private static Point position_;
        #endregion
    }
}
