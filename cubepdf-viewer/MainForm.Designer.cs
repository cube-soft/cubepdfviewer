namespace Cube {
    partial class MainForm {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MenuToolStrip = new System.Windows.Forms.ToolStrip();
            this.FileButton = new System.Windows.Forms.ToolStripSplitButton();
            this.OpenNewTabMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenExistedTabMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayFileOpenCategorySeparator = new System.Windows.Forms.ToolStripSeparator();
            this.CloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrintButton = new System.Windows.Forms.ToolStripButton();
            this.OnlyDisplayCommonCategorySeparator = new System.Windows.Forms.ToolStripSeparator();
            this.ZoomInButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.OnlyDisplayZoom25 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom50 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom75 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom125 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom150 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom200 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom400 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom800 = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.FitToWidthButton = new System.Windows.Forms.ToolStripButton();
            this.FitToHeightButton = new System.Windows.Forms.ToolStripButton();
            this.OnlyDisplayZoomCategorySeparator = new System.Windows.Forms.ToolStripSeparator();
            this.FirstPageButton = new System.Windows.Forms.ToolStripButton();
            this.PreviousPageButton = new System.Windows.Forms.ToolStripButton();
            this.CurrentPageTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.TotalPageLabel = new System.Windows.Forms.ToolStripLabel();
            this.NextPageButton = new System.Windows.Forms.ToolStripButton();
            this.LastPageButton = new System.Windows.Forms.ToolStripButton();
            this.OnlyDisplayPageCategorySeparator = new System.Windows.Forms.ToolStripSeparator();
            this.SearchTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.SearchButton = new System.Windows.Forms.ToolStripButton();
            this.FooterStatusStrip = new System.Windows.Forms.StatusStrip();
            this.NavigationSplitContainer = new System.Windows.Forms.SplitContainer();
            this.NavigationPanel = new System.Windows.Forms.Panel();
            this.PageViewerTabControl = new System.Windows.Forms.TabControl();
            this.DefaultTabPage = new System.Windows.Forms.TabPage();
            this.SubMenuSplitContainer = new System.Windows.Forms.SplitContainer();
            this.SubMenuToolStrip = new System.Windows.Forms.ToolStrip();
            this.ThumbButton = new System.Windows.Forms.ToolStripButton();
            this.MenuModeButton = new System.Windows.Forms.ToolStripButton();
            this.MenuSplitContainer = new System.Windows.Forms.SplitContainer();
            this.MenuToolStrip.SuspendLayout();
            this.NavigationSplitContainer.Panel1.SuspendLayout();
            this.NavigationSplitContainer.Panel2.SuspendLayout();
            this.NavigationSplitContainer.SuspendLayout();
            this.PageViewerTabControl.SuspendLayout();
            this.SubMenuSplitContainer.Panel1.SuspendLayout();
            this.SubMenuSplitContainer.Panel2.SuspendLayout();
            this.SubMenuSplitContainer.SuspendLayout();
            this.SubMenuToolStrip.SuspendLayout();
            this.MenuSplitContainer.Panel1.SuspendLayout();
            this.MenuSplitContainer.Panel2.SuspendLayout();
            this.MenuSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuToolStrip
            // 
            this.MenuToolStrip.AutoSize = false;
            this.MenuToolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.MenuToolStrip.GripMargin = new System.Windows.Forms.Padding(5);
            this.MenuToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.MenuToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.MenuToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileButton,
            this.PrintButton,
            this.OnlyDisplayCommonCategorySeparator,
            this.ZoomInButton,
            this.ZoomDropDownButton,
            this.ZoomOutButton,
            this.FitToWidthButton,
            this.FitToHeightButton,
            this.OnlyDisplayZoomCategorySeparator,
            this.FirstPageButton,
            this.PreviousPageButton,
            this.CurrentPageTextBox,
            this.TotalPageLabel,
            this.NextPageButton,
            this.LastPageButton,
            this.OnlyDisplayPageCategorySeparator,
            this.SearchTextBox,
            this.SearchButton});
            this.MenuToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuToolStrip.Name = "MenuToolStrip";
            this.MenuToolStrip.Size = new System.Drawing.Size(792, 40);
            this.MenuToolStrip.TabIndex = 0;
            this.MenuToolStrip.Text = "メニュー";
            // 
            // FileButton
            // 
            this.FileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FileButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenNewTabMenuItem,
            this.OpenExistedTabMenuItem,
            this.OnlyDisplayFileOpenCategorySeparator,
            this.CloseMenuItem});
            this.FileButton.Image = global::Cube.Properties.Resources.open;
            this.FileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FileButton.Name = "FileButton";
            this.FileButton.Size = new System.Drawing.Size(48, 37);
            this.FileButton.Text = "ファイル";
            this.FileButton.ButtonClick += new System.EventHandler(this.OpenButton_Click);
            this.FileButton.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.FileButton_DropDownItemClicked);
            // 
            // OpenNewTabMenuItem
            // 
            this.OpenNewTabMenuItem.Name = "OpenNewTabMenuItem";
            this.OpenNewTabMenuItem.Size = new System.Drawing.Size(167, 22);
            this.OpenNewTabMenuItem.Text = "新しいタブで開く";
            this.OpenNewTabMenuItem.ToolTipText = "新しいタブで開く";
            // 
            // OpenExistedTabMenuItem
            // 
            this.OpenExistedTabMenuItem.Name = "OpenExistedTabMenuItem";
            this.OpenExistedTabMenuItem.Size = new System.Drawing.Size(167, 22);
            this.OpenExistedTabMenuItem.Text = "アクティブなタブで開く";
            this.OpenExistedTabMenuItem.ToolTipText = "アクティブなタブで開く";
            // 
            // OnlyDisplayFileOpenCategorySeparator
            // 
            this.OnlyDisplayFileOpenCategorySeparator.Name = "OnlyDisplayFileOpenCategorySeparator";
            this.OnlyDisplayFileOpenCategorySeparator.Size = new System.Drawing.Size(149, 6);
            // 
            // CloseMenuItem
            // 
            this.CloseMenuItem.Name = "CloseMenuItem";
            this.CloseMenuItem.Size = new System.Drawing.Size(152, 22);
            this.CloseMenuItem.Text = "閉じる";
            // 
            // PrintButton
            // 
            this.PrintButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrintButton.Image = global::Cube.Properties.Resources.print;
            this.PrintButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(36, 37);
            this.PrintButton.Text = "印刷する";
            // 
            // OnlyDisplayCommonCategorySeparator
            // 
            this.OnlyDisplayCommonCategorySeparator.Name = "OnlyDisplayCommonCategorySeparator";
            this.OnlyDisplayCommonCategorySeparator.Size = new System.Drawing.Size(6, 40);
            // 
            // ZoomInButton
            // 
            this.ZoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomInButton.Image = global::Cube.Properties.Resources.zoomin;
            this.ZoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomInButton.Margin = new System.Windows.Forms.Padding(2);
            this.ZoomInButton.Name = "ZoomInButton";
            this.ZoomInButton.Size = new System.Drawing.Size(36, 36);
            this.ZoomInButton.Text = "拡大する";
            this.ZoomInButton.Click += new System.EventHandler(this.ZoomInButton_Click);
            // 
            // ZoomDropDownButton
            // 
            this.ZoomDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ZoomDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OnlyDisplayZoom25,
            this.OnlyDisplayZoom50,
            this.OnlyDisplayZoom75,
            this.OnlyDisplayZoom100,
            this.OnlyDisplayZoom125,
            this.OnlyDisplayZoom150,
            this.OnlyDisplayZoom200,
            this.OnlyDisplayZoom400,
            this.OnlyDisplayZoom800});
            this.ZoomDropDownButton.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ZoomDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("ZoomDropDownButton.Image")));
            this.ZoomDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomDropDownButton.Margin = new System.Windows.Forms.Padding(2);
            this.ZoomDropDownButton.Name = "ZoomDropDownButton";
            this.ZoomDropDownButton.Size = new System.Drawing.Size(48, 36);
            this.ZoomDropDownButton.Text = "100%";
            this.ZoomDropDownButton.ToolTipText = "倍率";
            this.ZoomDropDownButton.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ZoomDropDownButton_DropDownItemClicked);
            // 
            // OnlyDisplayZoom25
            // 
            this.OnlyDisplayZoom25.Name = "OnlyDisplayZoom25";
            this.OnlyDisplayZoom25.Size = new System.Drawing.Size(102, 22);
            this.OnlyDisplayZoom25.Text = "25%";
            // 
            // OnlyDisplayZoom50
            // 
            this.OnlyDisplayZoom50.Name = "OnlyDisplayZoom50";
            this.OnlyDisplayZoom50.Size = new System.Drawing.Size(102, 22);
            this.OnlyDisplayZoom50.Text = "50%";
            // 
            // OnlyDisplayZoom75
            // 
            this.OnlyDisplayZoom75.Name = "OnlyDisplayZoom75";
            this.OnlyDisplayZoom75.Size = new System.Drawing.Size(102, 22);
            this.OnlyDisplayZoom75.Text = "75%";
            // 
            // OnlyDisplayZoom100
            // 
            this.OnlyDisplayZoom100.Name = "OnlyDisplayZoom100";
            this.OnlyDisplayZoom100.Size = new System.Drawing.Size(102, 22);
            this.OnlyDisplayZoom100.Text = "100%";
            // 
            // OnlyDisplayZoom125
            // 
            this.OnlyDisplayZoom125.Name = "OnlyDisplayZoom125";
            this.OnlyDisplayZoom125.Size = new System.Drawing.Size(102, 22);
            this.OnlyDisplayZoom125.Text = "125%";
            // 
            // OnlyDisplayZoom150
            // 
            this.OnlyDisplayZoom150.Name = "OnlyDisplayZoom150";
            this.OnlyDisplayZoom150.Size = new System.Drawing.Size(102, 22);
            this.OnlyDisplayZoom150.Text = "150%";
            // 
            // OnlyDisplayZoom200
            // 
            this.OnlyDisplayZoom200.Name = "OnlyDisplayZoom200";
            this.OnlyDisplayZoom200.Size = new System.Drawing.Size(102, 22);
            this.OnlyDisplayZoom200.Text = "200%";
            // 
            // OnlyDisplayZoom400
            // 
            this.OnlyDisplayZoom400.Name = "OnlyDisplayZoom400";
            this.OnlyDisplayZoom400.Size = new System.Drawing.Size(102, 22);
            this.OnlyDisplayZoom400.Text = "400%";
            // 
            // OnlyDisplayZoom800
            // 
            this.OnlyDisplayZoom800.Name = "OnlyDisplayZoom800";
            this.OnlyDisplayZoom800.Size = new System.Drawing.Size(102, 22);
            this.OnlyDisplayZoom800.Text = "800%";
            // 
            // ZoomOutButton
            // 
            this.ZoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomOutButton.Image = global::Cube.Properties.Resources.zoomout;
            this.ZoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomOutButton.Margin = new System.Windows.Forms.Padding(2);
            this.ZoomOutButton.Name = "ZoomOutButton";
            this.ZoomOutButton.Size = new System.Drawing.Size(36, 36);
            this.ZoomOutButton.Text = "縮小する";
            this.ZoomOutButton.Click += new System.EventHandler(this.ZoomOutButton_Click);
            // 
            // FitToWidthButton
            // 
            this.FitToWidthButton.CheckOnClick = true;
            this.FitToWidthButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FitToWidthButton.Image = global::Cube.Properties.Resources.fit2width;
            this.FitToWidthButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FitToWidthButton.Margin = new System.Windows.Forms.Padding(2);
            this.FitToWidthButton.Name = "FitToWidthButton";
            this.FitToWidthButton.Size = new System.Drawing.Size(36, 36);
            this.FitToWidthButton.Text = "ウィンドウの幅に合わせる";
            this.FitToWidthButton.Click += new System.EventHandler(this.FitToWidthButton_Click);
            // 
            // FitToHeightButton
            // 
            this.FitToHeightButton.CheckOnClick = true;
            this.FitToHeightButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FitToHeightButton.Image = global::Cube.Properties.Resources.fit2height;
            this.FitToHeightButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FitToHeightButton.Margin = new System.Windows.Forms.Padding(2);
            this.FitToHeightButton.Name = "FitToHeightButton";
            this.FitToHeightButton.Size = new System.Drawing.Size(36, 36);
            this.FitToHeightButton.Text = "ウィンドウの高さに合わせる";
            this.FitToHeightButton.Click += new System.EventHandler(this.FitToHeightButton_Click);
            // 
            // OnlyDisplayZoomCategorySeparator
            // 
            this.OnlyDisplayZoomCategorySeparator.Name = "OnlyDisplayZoomCategorySeparator";
            this.OnlyDisplayZoomCategorySeparator.Size = new System.Drawing.Size(6, 40);
            // 
            // FirstPageButton
            // 
            this.FirstPageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FirstPageButton.Image = global::Cube.Properties.Resources.arrow_first;
            this.FirstPageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FirstPageButton.Margin = new System.Windows.Forms.Padding(2);
            this.FirstPageButton.Name = "FirstPageButton";
            this.FirstPageButton.Size = new System.Drawing.Size(36, 36);
            this.FirstPageButton.Text = "最初のページ";
            this.FirstPageButton.Click += new System.EventHandler(this.FirstPageButton_Click);
            // 
            // PreviousPageButton
            // 
            this.PreviousPageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PreviousPageButton.Image = global::Cube.Properties.Resources.arrow_prev;
            this.PreviousPageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PreviousPageButton.Margin = new System.Windows.Forms.Padding(2);
            this.PreviousPageButton.Name = "PreviousPageButton";
            this.PreviousPageButton.Size = new System.Drawing.Size(36, 36);
            this.PreviousPageButton.Text = "前のページ";
            this.PreviousPageButton.Click += new System.EventHandler(this.PreviousPageButton_Click);
            // 
            // CurrentPageTextBox
            // 
            this.CurrentPageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrentPageTextBox.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CurrentPageTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.CurrentPageTextBox.Name = "CurrentPageTextBox";
            this.CurrentPageTextBox.Size = new System.Drawing.Size(50, 36);
            this.CurrentPageTextBox.Text = "0";
            this.CurrentPageTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CurrentPageTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CurrentPageTextBox_KeyDown);
            // 
            // TotalPageLabel
            // 
            this.TotalPageLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TotalPageLabel.Margin = new System.Windows.Forms.Padding(2);
            this.TotalPageLabel.Name = "TotalPageLabel";
            this.TotalPageLabel.Size = new System.Drawing.Size(25, 36);
            this.TotalPageLabel.Text = "/ 0";
            // 
            // NextPageButton
            // 
            this.NextPageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NextPageButton.Image = global::Cube.Properties.Resources.arrow_next;
            this.NextPageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NextPageButton.Margin = new System.Windows.Forms.Padding(2);
            this.NextPageButton.Name = "NextPageButton";
            this.NextPageButton.Size = new System.Drawing.Size(36, 36);
            this.NextPageButton.Text = "次のページ";
            this.NextPageButton.Click += new System.EventHandler(this.NextPageButton_Click);
            // 
            // LastPageButton
            // 
            this.LastPageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LastPageButton.Image = global::Cube.Properties.Resources.arrow_last;
            this.LastPageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LastPageButton.Margin = new System.Windows.Forms.Padding(2);
            this.LastPageButton.Name = "LastPageButton";
            this.LastPageButton.Size = new System.Drawing.Size(36, 36);
            this.LastPageButton.Text = "最後のページ";
            this.LastPageButton.Click += new System.EventHandler(this.LastPageButton_Click);
            // 
            // OnlyDisplayPageCategorySeparator
            // 
            this.OnlyDisplayPageCategorySeparator.Name = "OnlyDisplayPageCategorySeparator";
            this.OnlyDisplayPageCategorySeparator.Size = new System.Drawing.Size(6, 40);
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(100, 36);
            // 
            // SearchButton
            // 
            this.SearchButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SearchButton.Image = global::Cube.Properties.Resources.search;
            this.SearchButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SearchButton.Margin = new System.Windows.Forms.Padding(2);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(36, 36);
            this.SearchButton.Text = "検索する";
            // 
            // FooterStatusStrip
            // 
            this.FooterStatusStrip.Location = new System.Drawing.Point(0, 551);
            this.FooterStatusStrip.Name = "FooterStatusStrip";
            this.FooterStatusStrip.Size = new System.Drawing.Size(792, 22);
            this.FooterStatusStrip.TabIndex = 1;
            this.FooterStatusStrip.Text = "statusStrip1";
            // 
            // NavigationSplitContainer
            // 
            this.NavigationSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NavigationSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.NavigationSplitContainer.IsSplitterFixed = true;
            this.NavigationSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.NavigationSplitContainer.Name = "NavigationSplitContainer";
            // 
            // NavigationSplitContainer.Panel1
            // 
            this.NavigationSplitContainer.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.NavigationSplitContainer.Panel1.Controls.Add(this.NavigationPanel);
            // 
            // NavigationSplitContainer.Panel2
            // 
            this.NavigationSplitContainer.Panel2.Controls.Add(this.PageViewerTabControl);
            this.NavigationSplitContainer.Size = new System.Drawing.Size(759, 510);
            this.NavigationSplitContainer.SplitterDistance = 100;
            this.NavigationSplitContainer.SplitterWidth = 1;
            this.NavigationSplitContainer.TabIndex = 2;
            // 
            // NavigationPanel
            // 
            this.NavigationPanel.BackColor = System.Drawing.Color.DimGray;
            this.NavigationPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.NavigationPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NavigationPanel.Location = new System.Drawing.Point(0, 0);
            this.NavigationPanel.Name = "NavigationPanel";
            this.NavigationPanel.Size = new System.Drawing.Size(100, 510);
            this.NavigationPanel.TabIndex = 0;
            // 
            // PageViewerTabControl
            // 
            this.PageViewerTabControl.Controls.Add(this.DefaultTabPage);
            this.PageViewerTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PageViewerTabControl.Location = new System.Drawing.Point(0, 0);
            this.PageViewerTabControl.Name = "PageViewerTabControl";
            this.PageViewerTabControl.SelectedIndex = 0;
            this.PageViewerTabControl.Size = new System.Drawing.Size(658, 510);
            this.PageViewerTabControl.TabIndex = 0;
            this.PageViewerTabControl.SelectedIndexChanged += new System.EventHandler(this.PageViewerTabControl_SelectedIndexChanged);
            // 
            // DefaultTabPage
            // 
            this.DefaultTabPage.AutoScroll = true;
            this.DefaultTabPage.BackColor = System.Drawing.Color.DimGray;
            this.DefaultTabPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DefaultTabPage.Location = new System.Drawing.Point(4, 22);
            this.DefaultTabPage.Name = "DefaultTabPage";
            this.DefaultTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.DefaultTabPage.Size = new System.Drawing.Size(650, 484);
            this.DefaultTabPage.TabIndex = 0;
            this.DefaultTabPage.Text = "(無題)";
            // 
            // SubMenuSplitContainer
            // 
            this.SubMenuSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SubMenuSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SubMenuSplitContainer.IsSplitterFixed = true;
            this.SubMenuSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SubMenuSplitContainer.Name = "SubMenuSplitContainer";
            // 
            // SubMenuSplitContainer.Panel1
            // 
            this.SubMenuSplitContainer.Panel1.Controls.Add(this.SubMenuToolStrip);
            // 
            // SubMenuSplitContainer.Panel2
            // 
            this.SubMenuSplitContainer.Panel2.Controls.Add(this.NavigationSplitContainer);
            this.SubMenuSplitContainer.Size = new System.Drawing.Size(792, 510);
            this.SubMenuSplitContainer.SplitterDistance = 32;
            this.SubMenuSplitContainer.SplitterWidth = 1;
            this.SubMenuSplitContainer.TabIndex = 3;
            // 
            // SubMenuToolStrip
            // 
            this.SubMenuToolStrip.BackColor = System.Drawing.SystemColors.Control;
            this.SubMenuToolStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.SubMenuToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.SubMenuToolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.SubMenuToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ThumbButton,
            this.MenuModeButton});
            this.SubMenuToolStrip.Location = new System.Drawing.Point(0, 0);
            this.SubMenuToolStrip.Name = "SubMenuToolStrip";
            this.SubMenuToolStrip.Size = new System.Drawing.Size(29, 510);
            this.SubMenuToolStrip.TabIndex = 0;
            this.SubMenuToolStrip.Text = "toolStrip1";
            // 
            // ThumbButton
            // 
            this.ThumbButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ThumbButton.Image = global::Cube.Properties.Resources.thumbnail;
            this.ThumbButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ThumbButton.Name = "ThumbButton";
            this.ThumbButton.Size = new System.Drawing.Size(26, 28);
            this.ThumbButton.Text = "サムネイル";
            this.ThumbButton.Click += new System.EventHandler(this.ThumbButton_Click);
            // 
            // MenuModeButton
            // 
            this.MenuModeButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.MenuModeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuModeButton.Image = global::Cube.Properties.Resources.hidemenu;
            this.MenuModeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuModeButton.Name = "MenuModeButton";
            this.MenuModeButton.Size = new System.Drawing.Size(26, 28);
            this.MenuModeButton.Text = "メニュー表示の切り替え";
            this.MenuModeButton.Click += new System.EventHandler(this.MenuModeButton_Click);
            // 
            // MenuSplitContainer
            // 
            this.MenuSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MenuSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.MenuSplitContainer.IsSplitterFixed = true;
            this.MenuSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.MenuSplitContainer.Name = "MenuSplitContainer";
            this.MenuSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MenuSplitContainer.Panel1
            // 
            this.MenuSplitContainer.Panel1.Controls.Add(this.MenuToolStrip);
            // 
            // MenuSplitContainer.Panel2
            // 
            this.MenuSplitContainer.Panel2.Controls.Add(this.SubMenuSplitContainer);
            this.MenuSplitContainer.Size = new System.Drawing.Size(792, 551);
            this.MenuSplitContainer.SplitterDistance = 40;
            this.MenuSplitContainer.SplitterWidth = 1;
            this.MenuSplitContainer.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.MenuSplitContainer);
            this.Controls.Add(this.FooterStatusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "CubePDF Viewer";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.MenuToolStrip.ResumeLayout(false);
            this.MenuToolStrip.PerformLayout();
            this.NavigationSplitContainer.Panel1.ResumeLayout(false);
            this.NavigationSplitContainer.Panel2.ResumeLayout(false);
            this.NavigationSplitContainer.ResumeLayout(false);
            this.PageViewerTabControl.ResumeLayout(false);
            this.SubMenuSplitContainer.Panel1.ResumeLayout(false);
            this.SubMenuSplitContainer.Panel1.PerformLayout();
            this.SubMenuSplitContainer.Panel2.ResumeLayout(false);
            this.SubMenuSplitContainer.ResumeLayout(false);
            this.SubMenuToolStrip.ResumeLayout(false);
            this.SubMenuToolStrip.PerformLayout();
            this.MenuSplitContainer.Panel1.ResumeLayout(false);
            this.MenuSplitContainer.Panel2.ResumeLayout(false);
            this.MenuSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip MenuToolStrip;
        private System.Windows.Forms.ToolStripSeparator OnlyDisplayCommonCategorySeparator;
        private System.Windows.Forms.ToolStripButton ZoomInButton;
        private System.Windows.Forms.ToolStripDropDownButton ZoomDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem OnlyDisplayZoom25;
        private System.Windows.Forms.ToolStripMenuItem OnlyDisplayZoom50;
        private System.Windows.Forms.ToolStripMenuItem OnlyDisplayZoom75;
        private System.Windows.Forms.ToolStripMenuItem OnlyDisplayZoom100;
        private System.Windows.Forms.ToolStripMenuItem OnlyDisplayZoom125;
        private System.Windows.Forms.ToolStripMenuItem OnlyDisplayZoom150;
        private System.Windows.Forms.ToolStripMenuItem OnlyDisplayZoom200;
        private System.Windows.Forms.ToolStripMenuItem OnlyDisplayZoom400;
        private System.Windows.Forms.ToolStripMenuItem OnlyDisplayZoom800;
        private System.Windows.Forms.ToolStripButton ZoomOutButton;
        private System.Windows.Forms.ToolStripButton FitToWidthButton;
        private System.Windows.Forms.ToolStripButton FitToHeightButton;
        private System.Windows.Forms.ToolStripSeparator OnlyDisplayZoomCategorySeparator;
        private System.Windows.Forms.ToolStripButton FirstPageButton;
        private System.Windows.Forms.ToolStripButton PreviousPageButton;
        private System.Windows.Forms.ToolStripTextBox CurrentPageTextBox;
        private System.Windows.Forms.ToolStripLabel TotalPageLabel;
        private System.Windows.Forms.ToolStripButton NextPageButton;
        private System.Windows.Forms.ToolStripButton LastPageButton;
        private System.Windows.Forms.ToolStripSeparator OnlyDisplayPageCategorySeparator;
        private System.Windows.Forms.ToolStripTextBox SearchTextBox;
        private System.Windows.Forms.ToolStripButton SearchButton;
        private System.Windows.Forms.StatusStrip FooterStatusStrip;
        private System.Windows.Forms.SplitContainer NavigationSplitContainer;
        private System.Windows.Forms.TabControl PageViewerTabControl;
        private System.Windows.Forms.TabPage DefaultTabPage;
        private System.Windows.Forms.SplitContainer SubMenuSplitContainer;
        private System.Windows.Forms.ToolStrip SubMenuToolStrip;
        private System.Windows.Forms.ToolStripButton PrintButton;
        private System.Windows.Forms.ToolStripButton ThumbButton;
        private System.Windows.Forms.Panel NavigationPanel;
        private System.Windows.Forms.SplitContainer MenuSplitContainer;
        private System.Windows.Forms.ToolStripButton MenuModeButton;
        private System.Windows.Forms.ToolStripSplitButton FileButton;
        private System.Windows.Forms.ToolStripMenuItem OpenNewTabMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenExistedTabMenuItem;
        private System.Windows.Forms.ToolStripSeparator OnlyDisplayFileOpenCategorySeparator;
        private System.Windows.Forms.ToolStripMenuItem CloseMenuItem;
    }
}

