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
            this.MenuModeButton = new System.Windows.Forms.ToolStripButton();
            this.ThumbButton = new System.Windows.Forms.ToolStripButton();
            this.NewTabButton = new System.Windows.Forms.ToolStripButton();
            this.OpenButton = new System.Windows.Forms.ToolStripButton();
            this.OnlyDisplayCategoryFileLabel = new System.Windows.Forms.ToolStripLabel();
            this.PrintButton = new System.Windows.Forms.ToolStripButton();
            this.OnlyDisplayCategoryPrintLabel = new System.Windows.Forms.ToolStripLabel();
            this.CurrentPageTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.OnlyDisplayCategoryTotalPageLabel = new System.Windows.Forms.ToolStripLabel();
            this.TotalPageLabel = new System.Windows.Forms.ToolStripLabel();
            this.FirstPageButton = new System.Windows.Forms.ToolStripButton();
            this.PreviousPageButton = new System.Windows.Forms.ToolStripButton();
            this.NextPageButton = new System.Windows.Forms.ToolStripButton();
            this.OnlyDisplayCategoryPageLabel = new System.Windows.Forms.ToolStripLabel();
            this.SearchTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.SearchButton = new System.Windows.Forms.ToolStripButton();
            this.OnlyDisplayCategorySearchLabel = new System.Windows.Forms.ToolStripLabel();
            this.ZoomDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.OnlyDisplayZoom25 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom50 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom75 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom125 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom150 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom200 = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomInButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.FitToWidthButton = new System.Windows.Forms.ToolStripButton();
            this.FitToHeightButton = new System.Windows.Forms.ToolStripButton();
            this.OnlyDisplayLogoLabel = new System.Windows.Forms.ToolStripLabel();
            this.OnlyDisplayCategoryZoomLabel = new System.Windows.Forms.ToolStripLabel();
            this.FooterStatusStrip = new System.Windows.Forms.StatusStrip();
            this.FooterStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.MenuSplitContainer = new System.Windows.Forms.SplitContainer();
            this.NavigationSplitContainer = new System.Windows.Forms.SplitContainer();
            this.PageViewerTabControl = new System.Windows.Forms.CustomTabControl();
            this.DefaultTabPage = new System.Windows.Forms.TabPage();
            this.MenuToolStrip.SuspendLayout();
            this.FooterStatusStrip.SuspendLayout();
            this.MenuSplitContainer.Panel1.SuspendLayout();
            this.MenuSplitContainer.Panel2.SuspendLayout();
            this.MenuSplitContainer.SuspendLayout();
            this.NavigationSplitContainer.Panel2.SuspendLayout();
            this.NavigationSplitContainer.SuspendLayout();
            this.PageViewerTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuToolStrip
            // 
            this.MenuToolStrip.AutoSize = false;
            this.MenuToolStrip.BackColor = System.Drawing.Color.Black;
            this.MenuToolStrip.BackgroundImage = global::Cube.Properties.Resources.background;
            this.MenuToolStrip.GripMargin = new System.Windows.Forms.Padding(0);
            this.MenuToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.MenuToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.MenuToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuModeButton,
            this.ThumbButton,
            this.NewTabButton,
            this.OpenButton,
            this.OnlyDisplayCategoryFileLabel,
            this.PrintButton,
            this.OnlyDisplayCategoryPrintLabel,
            this.CurrentPageTextBox,
            this.OnlyDisplayCategoryTotalPageLabel,
            this.TotalPageLabel,
            this.FirstPageButton,
            this.PreviousPageButton,
            this.NextPageButton,
            this.OnlyDisplayCategoryPageLabel,
            this.SearchTextBox,
            this.SearchButton,
            this.OnlyDisplayCategorySearchLabel,
            this.ZoomDropDownButton,
            this.ZoomInButton,
            this.ZoomOutButton,
            this.FitToWidthButton,
            this.FitToHeightButton,
            this.OnlyDisplayLogoLabel,
            this.OnlyDisplayCategoryZoomLabel});
            this.MenuToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MenuToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuToolStrip.Name = "MenuToolStrip";
            this.MenuToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.MenuToolStrip.Size = new System.Drawing.Size(892, 40);
            this.MenuToolStrip.TabIndex = 0;
            this.MenuToolStrip.Text = "メニュー";
            // 
            // MenuModeButton
            // 
            this.MenuModeButton.AutoSize = false;
            this.MenuModeButton.BackColor = System.Drawing.Color.Transparent;
            this.MenuModeButton.BackgroundImage = global::Cube.Properties.Resources.hidemenu;
            this.MenuModeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MenuModeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.MenuModeButton.Image = global::Cube.Properties.Resources.hidemenu;
            this.MenuModeButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.MenuModeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuModeButton.Margin = new System.Windows.Forms.Padding(0);
            this.MenuModeButton.Name = "MenuModeButton";
            this.MenuModeButton.Size = new System.Drawing.Size(19, 40);
            this.MenuModeButton.Text = "メニューの非表示";
            this.MenuModeButton.ToolTipText = "メニューの非表示。元に戻すには F8 ボタンを押して下さい。";
            this.MenuModeButton.Click += new System.EventHandler(this.MenuModeButton_Click);
            // 
            // ThumbButton
            // 
            this.ThumbButton.AutoSize = false;
            this.ThumbButton.BackColor = System.Drawing.Color.Transparent;
            this.ThumbButton.BackgroundImage = global::Cube.Properties.Resources.thumbnail;
            this.ThumbButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ThumbButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.ThumbButton.Image = global::Cube.Properties.Resources.thumbnail;
            this.ThumbButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ThumbButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ThumbButton.Margin = new System.Windows.Forms.Padding(0);
            this.ThumbButton.Name = "ThumbButton";
            this.ThumbButton.Size = new System.Drawing.Size(28, 40);
            this.ThumbButton.Text = "サムネイル表示の切り替え";
            this.ThumbButton.Click += new System.EventHandler(this.ThumbButton_Click);
            // 
            // NewTabButton
            // 
            this.NewTabButton.BackColor = System.Drawing.Color.Transparent;
            this.NewTabButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewTabButton.Image = global::Cube.Properties.Resources.newtab;
            this.NewTabButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.NewTabButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewTabButton.Margin = new System.Windows.Forms.Padding(10, 0, -2, 0);
            this.NewTabButton.Name = "NewTabButton";
            this.NewTabButton.Size = new System.Drawing.Size(37, 40);
            this.NewTabButton.Text = "新しいタブ";
            this.NewTabButton.Click += new System.EventHandler(this.NewTabButton_Click);
            // 
            // OpenButton
            // 
            this.OpenButton.BackColor = System.Drawing.Color.Transparent;
            this.OpenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenButton.Image = global::Cube.Properties.Resources.open;
            this.OpenButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.OpenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenButton.Margin = new System.Windows.Forms.Padding(-2, 0, 0, 0);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(37, 40);
            this.OpenButton.Text = "開く";
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // OnlyDisplayCategoryFileLabel
            // 
            this.OnlyDisplayCategoryFileLabel.AutoSize = false;
            this.OnlyDisplayCategoryFileLabel.BackColor = System.Drawing.Color.Black;
            this.OnlyDisplayCategoryFileLabel.BackgroundImage = global::Cube.Properties.Resources.split;
            this.OnlyDisplayCategoryFileLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OnlyDisplayCategoryFileLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.OnlyDisplayCategoryFileLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.OnlyDisplayCategoryFileLabel.Name = "OnlyDisplayCategoryFileLabel";
            this.OnlyDisplayCategoryFileLabel.Size = new System.Drawing.Size(2, 40);
            // 
            // PrintButton
            // 
            this.PrintButton.BackColor = System.Drawing.Color.Transparent;
            this.PrintButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrintButton.Image = global::Cube.Properties.Resources.print;
            this.PrintButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.PrintButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintButton.Margin = new System.Windows.Forms.Padding(0);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(35, 40);
            this.PrintButton.Text = "印刷する";
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            // 
            // OnlyDisplayCategoryPrintLabel
            // 
            this.OnlyDisplayCategoryPrintLabel.AutoSize = false;
            this.OnlyDisplayCategoryPrintLabel.BackColor = System.Drawing.Color.Transparent;
            this.OnlyDisplayCategoryPrintLabel.BackgroundImage = global::Cube.Properties.Resources.split;
            this.OnlyDisplayCategoryPrintLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OnlyDisplayCategoryPrintLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.OnlyDisplayCategoryPrintLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.OnlyDisplayCategoryPrintLabel.Name = "OnlyDisplayCategoryPrintLabel";
            this.OnlyDisplayCategoryPrintLabel.Size = new System.Drawing.Size(2, 40);
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
            // OnlyDisplayCategoryTotalPageLabel
            // 
            this.OnlyDisplayCategoryTotalPageLabel.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OnlyDisplayCategoryTotalPageLabel.ForeColor = System.Drawing.Color.LightGray;
            this.OnlyDisplayCategoryTotalPageLabel.Margin = new System.Windows.Forms.Padding(0, 0, -5, 0);
            this.OnlyDisplayCategoryTotalPageLabel.Name = "OnlyDisplayCategoryTotalPageLabel";
            this.OnlyDisplayCategoryTotalPageLabel.Size = new System.Drawing.Size(19, 40);
            this.OnlyDisplayCategoryTotalPageLabel.Text = "/";
            // 
            // TotalPageLabel
            // 
            this.TotalPageLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TotalPageLabel.ForeColor = System.Drawing.Color.LightGray;
            this.TotalPageLabel.Margin = new System.Windows.Forms.Padding(0, 0, 0, -2);
            this.TotalPageLabel.Name = "TotalPageLabel";
            this.TotalPageLabel.Size = new System.Drawing.Size(14, 42);
            this.TotalPageLabel.Text = "0";
            // 
            // FirstPageButton
            // 
            this.FirstPageButton.BackColor = System.Drawing.Color.Transparent;
            this.FirstPageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FirstPageButton.Image = global::Cube.Properties.Resources.arrow_first;
            this.FirstPageButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.FirstPageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FirstPageButton.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.FirstPageButton.Name = "FirstPageButton";
            this.FirstPageButton.Size = new System.Drawing.Size(34, 40);
            this.FirstPageButton.Text = "toolStripButton1";
            // 
            // PreviousPageButton
            // 
            this.PreviousPageButton.BackColor = System.Drawing.Color.Transparent;
            this.PreviousPageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PreviousPageButton.Image = global::Cube.Properties.Resources.arrow_prev;
            this.PreviousPageButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.PreviousPageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PreviousPageButton.Margin = new System.Windows.Forms.Padding(0, 0, -2, 0);
            this.PreviousPageButton.Name = "PreviousPageButton";
            this.PreviousPageButton.Size = new System.Drawing.Size(36, 40);
            this.PreviousPageButton.Text = "前のページ";
            this.PreviousPageButton.Click += new System.EventHandler(this.PreviousPageButton_Click);
            // 
            // NextPageButton
            // 
            this.NextPageButton.BackColor = System.Drawing.Color.Transparent;
            this.NextPageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NextPageButton.Image = global::Cube.Properties.Resources.arrow_next;
            this.NextPageButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.NextPageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NextPageButton.Margin = new System.Windows.Forms.Padding(-2, 0, 5, 0);
            this.NextPageButton.Name = "NextPageButton";
            this.NextPageButton.Size = new System.Drawing.Size(36, 40);
            this.NextPageButton.Text = "次のページ";
            this.NextPageButton.Click += new System.EventHandler(this.NextPageButton_Click);
            // 
            // OnlyDisplayCategoryPageLabel
            // 
            this.OnlyDisplayCategoryPageLabel.AutoSize = false;
            this.OnlyDisplayCategoryPageLabel.BackColor = System.Drawing.Color.Transparent;
            this.OnlyDisplayCategoryPageLabel.BackgroundImage = global::Cube.Properties.Resources.split;
            this.OnlyDisplayCategoryPageLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OnlyDisplayCategoryPageLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.OnlyDisplayCategoryPageLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.OnlyDisplayCategoryPageLabel.Name = "OnlyDisplayCategoryPageLabel";
            this.OnlyDisplayCategoryPageLabel.Size = new System.Drawing.Size(2, 40);
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchTextBox.Margin = new System.Windows.Forms.Padding(5, 0, -1, 0);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(100, 40);
            this.SearchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            // 
            // SearchButton
            // 
            this.SearchButton.BackColor = System.Drawing.Color.Transparent;
            this.SearchButton.BackgroundImage = global::Cube.Properties.Resources.search;
            this.SearchButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.SearchButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.SearchButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SearchButton.Margin = new System.Windows.Forms.Padding(0);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(23, 40);
            this.SearchButton.Text = "検索する";
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // OnlyDisplayCategorySearchLabel
            // 
            this.OnlyDisplayCategorySearchLabel.AutoSize = false;
            this.OnlyDisplayCategorySearchLabel.BackColor = System.Drawing.Color.Transparent;
            this.OnlyDisplayCategorySearchLabel.BackgroundImage = global::Cube.Properties.Resources.split;
            this.OnlyDisplayCategorySearchLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OnlyDisplayCategorySearchLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.OnlyDisplayCategorySearchLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.OnlyDisplayCategorySearchLabel.Name = "OnlyDisplayCategorySearchLabel";
            this.OnlyDisplayCategorySearchLabel.Size = new System.Drawing.Size(2, 40);
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
            this.OnlyDisplayZoom200});
            this.ZoomDropDownButton.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ZoomDropDownButton.ForeColor = System.Drawing.Color.LightGray;
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
            // ZoomInButton
            // 
            this.ZoomInButton.BackColor = System.Drawing.Color.Transparent;
            this.ZoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomInButton.Image = global::Cube.Properties.Resources.zoomin;
            this.ZoomInButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ZoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomInButton.Margin = new System.Windows.Forms.Padding(0, 0, -2, 0);
            this.ZoomInButton.Name = "ZoomInButton";
            this.ZoomInButton.Size = new System.Drawing.Size(36, 40);
            this.ZoomInButton.Text = "拡大する";
            this.ZoomInButton.Click += new System.EventHandler(this.ZoomInButton_Click);
            // 
            // ZoomOutButton
            // 
            this.ZoomOutButton.BackColor = System.Drawing.Color.Transparent;
            this.ZoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomOutButton.Image = global::Cube.Properties.Resources.zoomout;
            this.ZoomOutButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ZoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomOutButton.Margin = new System.Windows.Forms.Padding(-2, 0, 0, 0);
            this.ZoomOutButton.Name = "ZoomOutButton";
            this.ZoomOutButton.Size = new System.Drawing.Size(36, 40);
            this.ZoomOutButton.Text = "縮小する";
            this.ZoomOutButton.Click += new System.EventHandler(this.ZoomOutButton_Click);
            // 
            // FitToWidthButton
            // 
            this.FitToWidthButton.BackColor = System.Drawing.Color.Transparent;
            this.FitToWidthButton.CheckOnClick = true;
            this.FitToWidthButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FitToWidthButton.Image = global::Cube.Properties.Resources.fit2width;
            this.FitToWidthButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.FitToWidthButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FitToWidthButton.Margin = new System.Windows.Forms.Padding(0, 0, -2, 0);
            this.FitToWidthButton.Name = "FitToWidthButton";
            this.FitToWidthButton.Size = new System.Drawing.Size(36, 40);
            this.FitToWidthButton.Text = "ウィンドウの幅に合わせる";
            this.FitToWidthButton.Click += new System.EventHandler(this.FitToWidthButton_Click);
            // 
            // FitToHeightButton
            // 
            this.FitToHeightButton.BackColor = System.Drawing.Color.Transparent;
            this.FitToHeightButton.CheckOnClick = true;
            this.FitToHeightButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FitToHeightButton.Image = global::Cube.Properties.Resources.fit2height;
            this.FitToHeightButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.FitToHeightButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FitToHeightButton.Margin = new System.Windows.Forms.Padding(-2, 0, 0, 0);
            this.FitToHeightButton.Name = "FitToHeightButton";
            this.FitToHeightButton.Size = new System.Drawing.Size(36, 40);
            this.FitToHeightButton.Text = "ウィンドウの高さに合わせる";
            this.FitToHeightButton.Click += new System.EventHandler(this.FitToHeightButton_Click);
            // 
            // OnlyDisplayLogoLabel
            // 
            this.OnlyDisplayLogoLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.OnlyDisplayLogoLabel.AutoSize = false;
            this.OnlyDisplayLogoLabel.BackColor = System.Drawing.Color.Transparent;
            this.OnlyDisplayLogoLabel.BackgroundImage = global::Cube.Properties.Resources.logo;
            this.OnlyDisplayLogoLabel.Enabled = false;
            this.OnlyDisplayLogoLabel.Margin = new System.Windows.Forms.Padding(0);
            this.OnlyDisplayLogoLabel.Name = "OnlyDisplayLogoLabel";
            this.OnlyDisplayLogoLabel.Size = new System.Drawing.Size(122, 39);
            // 
            // OnlyDisplayCategoryZoomLabel
            // 
            this.OnlyDisplayCategoryZoomLabel.AutoSize = false;
            this.OnlyDisplayCategoryZoomLabel.BackColor = System.Drawing.Color.Transparent;
            this.OnlyDisplayCategoryZoomLabel.BackgroundImage = global::Cube.Properties.Resources.split;
            this.OnlyDisplayCategoryZoomLabel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OnlyDisplayCategoryZoomLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.OnlyDisplayCategoryZoomLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.OnlyDisplayCategoryZoomLabel.Name = "OnlyDisplayCategoryZoomLabel";
            this.OnlyDisplayCategoryZoomLabel.Size = new System.Drawing.Size(2, 40);
            // 
            // FooterStatusStrip
            // 
            this.FooterStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FooterStatusLabel});
            this.FooterStatusStrip.Location = new System.Drawing.Point(0, 551);
            this.FooterStatusStrip.Name = "FooterStatusStrip";
            this.FooterStatusStrip.Size = new System.Drawing.Size(892, 22);
            this.FooterStatusStrip.SizingGrip = false;
            this.FooterStatusStrip.TabIndex = 1;
            // 
            // FooterStatusLabel
            // 
            this.FooterStatusLabel.Name = "FooterStatusLabel";
            this.FooterStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // MenuSplitContainer
            // 
            this.MenuSplitContainer.BackColor = System.Drawing.Color.Black;
            this.MenuSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MenuSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.MenuSplitContainer.IsSplitterFixed = true;
            this.MenuSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.MenuSplitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.MenuSplitContainer.Name = "MenuSplitContainer";
            this.MenuSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MenuSplitContainer.Panel1
            // 
            this.MenuSplitContainer.Panel1.Controls.Add(this.MenuToolStrip);
            // 
            // MenuSplitContainer.Panel2
            // 
            this.MenuSplitContainer.Panel2.Controls.Add(this.NavigationSplitContainer);
            this.MenuSplitContainer.Size = new System.Drawing.Size(892, 551);
            this.MenuSplitContainer.SplitterDistance = 40;
            this.MenuSplitContainer.SplitterWidth = 1;
            this.MenuSplitContainer.TabIndex = 4;
            // 
            // NavigationSplitContainer
            // 
            this.NavigationSplitContainer.BackColor = System.Drawing.Color.Transparent;
            this.NavigationSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NavigationSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.NavigationSplitContainer.IsSplitterFixed = true;
            this.NavigationSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.NavigationSplitContainer.Name = "NavigationSplitContainer";
            // 
            // NavigationSplitContainer.Panel1
            // 
            this.NavigationSplitContainer.Panel1.BackColor = System.Drawing.Color.DimGray;
            // 
            // NavigationSplitContainer.Panel2
            // 
            this.NavigationSplitContainer.Panel2.BackColor = System.Drawing.Color.Black;
            this.NavigationSplitContainer.Panel2.Controls.Add(this.PageViewerTabControl);
            this.NavigationSplitContainer.Size = new System.Drawing.Size(892, 510);
            this.NavigationSplitContainer.SplitterDistance = 150;
            this.NavigationSplitContainer.SplitterWidth = 1;
            this.NavigationSplitContainer.TabIndex = 2;
            // 
            // PageViewerTabControl
            // 
            this.PageViewerTabControl.AllowDrop = true;
            this.PageViewerTabControl.Controls.Add(this.DefaultTabPage);
            // 
            // 
            // 
            this.PageViewerTabControl.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.PageViewerTabControl.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark;
            this.PageViewerTabControl.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(130)))), ((int)(((byte)(132)))));
            this.PageViewerTabControl.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray;
            this.PageViewerTabControl.DisplayStyleProvider.FocusTrack = true;
            this.PageViewerTabControl.DisplayStyleProvider.HotTrack = true;
            this.PageViewerTabControl.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PageViewerTabControl.DisplayStyleProvider.Opacity = 1F;
            this.PageViewerTabControl.DisplayStyleProvider.Overlap = 0;
            this.PageViewerTabControl.DisplayStyleProvider.Padding = new System.Drawing.Point(6, 3);
            this.PageViewerTabControl.DisplayStyleProvider.Radius = 2;
            this.PageViewerTabControl.DisplayStyleProvider.ShowTabCloser = true;
            this.PageViewerTabControl.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText;
            this.PageViewerTabControl.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark;
            this.PageViewerTabControl.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText;
            this.PageViewerTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PageViewerTabControl.HotTrack = true;
            this.PageViewerTabControl.Location = new System.Drawing.Point(0, 0);
            this.PageViewerTabControl.Name = "PageViewerTabControl";
            this.PageViewerTabControl.SelectedIndex = 0;
            this.PageViewerTabControl.Size = new System.Drawing.Size(741, 510);
            this.PageViewerTabControl.TabIndex = 0;
            this.PageViewerTabControl.TabClosing += new System.EventHandler<System.Windows.Forms.TabControlCancelEventArgs>(this.PageViewerTabControl_TabClosing);
            this.PageViewerTabControl.SelectedIndexChanged += new System.EventHandler(this.PageViewerTabControl_SelectedIndexChanged);
            this.PageViewerTabControl.DragDrop += new System.Windows.Forms.DragEventHandler(this.PageViewerTabControl_DragDrop);
            this.PageViewerTabControl.DragEnter += new System.Windows.Forms.DragEventHandler(this.PageViewerTabControl_DragEnter);
            // 
            // DefaultTabPage
            // 
            this.DefaultTabPage.AutoScroll = true;
            this.DefaultTabPage.BackColor = System.Drawing.Color.DimGray;
            this.DefaultTabPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DefaultTabPage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DefaultTabPage.Location = new System.Drawing.Point(4, 23);
            this.DefaultTabPage.Name = "DefaultTabPage";
            this.DefaultTabPage.Size = new System.Drawing.Size(733, 483);
            this.DefaultTabPage.TabIndex = 0;
            this.DefaultTabPage.Text = "(無題)";
            this.DefaultTabPage.Scroll += new System.Windows.Forms.ScrollEventHandler(this.VerticalScrolled);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(892, 573);
            this.Controls.Add(this.MenuSplitContainer);
            this.Controls.Add(this.FooterStatusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "CubePDF Viewer";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.MenuToolStrip.ResumeLayout(false);
            this.MenuToolStrip.PerformLayout();
            this.FooterStatusStrip.ResumeLayout(false);
            this.FooterStatusStrip.PerformLayout();
            this.MenuSplitContainer.Panel1.ResumeLayout(false);
            this.MenuSplitContainer.Panel2.ResumeLayout(false);
            this.MenuSplitContainer.ResumeLayout(false);
            this.NavigationSplitContainer.Panel2.ResumeLayout(false);
            this.NavigationSplitContainer.ResumeLayout(false);
            this.PageViewerTabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip MenuToolStrip;
        private System.Windows.Forms.ToolStripButton ZoomInButton;
        private System.Windows.Forms.ToolStripDropDownButton ZoomDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem OnlyDisplayZoom25;
        private System.Windows.Forms.ToolStripMenuItem OnlyDisplayZoom50;
        private System.Windows.Forms.ToolStripMenuItem OnlyDisplayZoom75;
        private System.Windows.Forms.ToolStripMenuItem OnlyDisplayZoom100;
        private System.Windows.Forms.ToolStripMenuItem OnlyDisplayZoom125;
        private System.Windows.Forms.ToolStripMenuItem OnlyDisplayZoom150;
        private System.Windows.Forms.ToolStripMenuItem OnlyDisplayZoom200;
        private System.Windows.Forms.ToolStripButton ZoomOutButton;
        private System.Windows.Forms.ToolStripButton FitToWidthButton;
        private System.Windows.Forms.ToolStripButton FitToHeightButton;
        private System.Windows.Forms.ToolStripButton PreviousPageButton;
        private System.Windows.Forms.ToolStripTextBox CurrentPageTextBox;
        private System.Windows.Forms.ToolStripLabel TotalPageLabel;
        private System.Windows.Forms.ToolStripButton NextPageButton;
        private System.Windows.Forms.ToolStripTextBox SearchTextBox;
        private System.Windows.Forms.ToolStripButton SearchButton;
        private System.Windows.Forms.StatusStrip FooterStatusStrip;
        private System.Windows.Forms.ToolStripButton PrintButton;
        private System.Windows.Forms.SplitContainer MenuSplitContainer;
        private System.Windows.Forms.ToolStripStatusLabel FooterStatusLabel;
        private System.Windows.Forms.ToolStripButton ThumbButton;
        private System.Windows.Forms.SplitContainer NavigationSplitContainer;
        private System.Windows.Forms.CustomTabControl PageViewerTabControl;
        private System.Windows.Forms.TabPage DefaultTabPage;
        private System.Windows.Forms.ToolStripButton MenuModeButton;
        private System.Windows.Forms.ToolStripLabel OnlyDisplayLogoLabel;
        private System.Windows.Forms.ToolStripButton OpenButton;
        private System.Windows.Forms.ToolStripButton NewTabButton;
        private System.Windows.Forms.ToolStripButton FirstPageButton;
        private System.Windows.Forms.ToolStripLabel OnlyDisplayCategoryTotalPageLabel;
        private System.Windows.Forms.ToolStripLabel OnlyDisplayCategoryFileLabel;
        private System.Windows.Forms.ToolStripLabel OnlyDisplayCategoryPrintLabel;
        private System.Windows.Forms.ToolStripLabel OnlyDisplayCategoryPageLabel;
        private System.Windows.Forms.ToolStripLabel OnlyDisplayCategorySearchLabel;
        private System.Windows.Forms.ToolStripLabel OnlyDisplayCategoryZoomLabel;
    }
}

