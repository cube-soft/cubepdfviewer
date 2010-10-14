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
            this.OpenButton = new System.Windows.Forms.ToolStripButton();
            this.PrintButton = new System.Windows.Forms.ToolStripButton();
            this.OnlyDisplayCategoryFileSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.CurrentPageTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.OnlyDisplayCategoryTotalPageLabel = new System.Windows.Forms.ToolStripLabel();
            this.TotalPageLabel = new System.Windows.Forms.ToolStripLabel();
            this.FirstPageButton = new System.Windows.Forms.ToolStripButton();
            this.PreviousPageButton = new System.Windows.Forms.ToolStripButton();
            this.NextPageButton = new System.Windows.Forms.ToolStripButton();
            this.LastPageButton = new System.Windows.Forms.ToolStripButton();
            this.OnlyDisplayCategoryPageSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.SearchTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.SearchButton = new System.Windows.Forms.ToolStripButton();
            this.OnlyDisplayCategorySearchSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.ZoomDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.OnlyDisplayZoom25 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom50 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom75 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom125 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom150 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom200 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnlyDisplayZoom400 = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomInButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.FitToWidthButton = new System.Windows.Forms.ToolStripButton();
            this.FitToHeightButton = new System.Windows.Forms.ToolStripButton();
            this.LogoButton = new System.Windows.Forms.ToolStripButton();
            this.OnlyDisplayCategoryZoomSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.AdobeButton = new System.Windows.Forms.ToolStripButton();
            this.MenuSplitContainer = new System.Windows.Forms.SplitContainer();
            this.NavigationSplitContainer = new System.Windows.Forms.SplitContainer();
            this.PageViewerTabControl = new System.Windows.Forms.CustomTabControl();
            this.DefaultTabPage = new System.Windows.Forms.TabPage();
            this.MenuToolStrip.SuspendLayout();
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
            this.OpenButton,
            this.PrintButton,
            this.OnlyDisplayCategoryFileSeparator,
            this.CurrentPageTextBox,
            this.OnlyDisplayCategoryTotalPageLabel,
            this.TotalPageLabel,
            this.FirstPageButton,
            this.PreviousPageButton,
            this.NextPageButton,
            this.LastPageButton,
            this.OnlyDisplayCategoryPageSeparator,
            this.SearchTextBox,
            this.SearchButton,
            this.OnlyDisplayCategorySearchSeparator,
            this.ZoomDropDownButton,
            this.ZoomInButton,
            this.ZoomOutButton,
            this.FitToWidthButton,
            this.FitToHeightButton,
            this.LogoButton,
            this.OnlyDisplayCategoryZoomSeparator,
            this.AdobeButton});
            this.MenuToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.MenuToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuToolStrip.Name = "MenuToolStrip";
            this.MenuToolStrip.Padding = new System.Windows.Forms.Padding(0);
            this.MenuToolStrip.Size = new System.Drawing.Size(934, 40);
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
            this.MenuModeButton.MouseEnter += new System.EventHandler(this.MenuModeButton_MouseEnter);
            this.MenuModeButton.MouseLeave += new System.EventHandler(this.MenuModeButton_MouseLeave);
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
            this.ThumbButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ThumbButton_MouseDown);
            this.ThumbButton.MouseEnter += new System.EventHandler(this.ThumbButton_MouseEnter);
            this.ThumbButton.MouseLeave += new System.EventHandler(this.ThumbButton_MouseLeave);
            this.ThumbButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ThumbButton_MouseUp);
            // 
            // OpenButton
            // 
            this.OpenButton.AutoSize = false;
            this.OpenButton.BackColor = System.Drawing.Color.Transparent;
            this.OpenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenButton.Image = global::Cube.Properties.Resources.open;
            this.OpenButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.OpenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenButton.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(36, 40);
            this.OpenButton.Text = "開く";
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            this.OpenButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OpenButton_MouseDown);
            this.OpenButton.MouseEnter += new System.EventHandler(this.OpenButton_MouseEnter);
            this.OpenButton.MouseLeave += new System.EventHandler(this.OpenButton_MouseLeave);
            this.OpenButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OpenButton_MouseUp);
            // 
            // PrintButton
            // 
            this.PrintButton.AutoSize = false;
            this.PrintButton.BackColor = System.Drawing.Color.Transparent;
            this.PrintButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrintButton.Image = global::Cube.Properties.Resources.print;
            this.PrintButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.PrintButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintButton.Margin = new System.Windows.Forms.Padding(0);
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(36, 40);
            this.PrintButton.Text = "印刷する";
            this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
            this.PrintButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PrintButton_MouseDown);
            this.PrintButton.MouseEnter += new System.EventHandler(this.PrintButton_MouseEnter);
            this.PrintButton.MouseLeave += new System.EventHandler(this.PrintButton_MouseLeave);
            this.PrintButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PrintButton_MouseUp);
            // 
            // OnlyDisplayCategoryFileSeparator
            // 
            this.OnlyDisplayCategoryFileSeparator.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.OnlyDisplayCategoryFileSeparator.Name = "OnlyDisplayCategoryFileSeparator";
            this.OnlyDisplayCategoryFileSeparator.Size = new System.Drawing.Size(6, 40);
            // 
            // CurrentPageTextBox
            // 
            this.CurrentPageTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CurrentPageTextBox.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CurrentPageTextBox.Margin = new System.Windows.Forms.Padding(2, 5, 2, 2);
            this.CurrentPageTextBox.Name = "CurrentPageTextBox";
            this.CurrentPageTextBox.Size = new System.Drawing.Size(50, 33);
            this.CurrentPageTextBox.Text = "0";
            this.CurrentPageTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CurrentPageTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CurrentPageTextBox_KeyDown);
            // 
            // OnlyDisplayCategoryTotalPageLabel
            // 
            this.OnlyDisplayCategoryTotalPageLabel.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OnlyDisplayCategoryTotalPageLabel.ForeColor = System.Drawing.Color.LightGray;
            this.OnlyDisplayCategoryTotalPageLabel.Margin = new System.Windows.Forms.Padding(0, 5, -5, 0);
            this.OnlyDisplayCategoryTotalPageLabel.Name = "OnlyDisplayCategoryTotalPageLabel";
            this.OnlyDisplayCategoryTotalPageLabel.Size = new System.Drawing.Size(19, 35);
            this.OnlyDisplayCategoryTotalPageLabel.Text = "/";
            // 
            // TotalPageLabel
            // 
            this.TotalPageLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TotalPageLabel.ForeColor = System.Drawing.Color.LightGray;
            this.TotalPageLabel.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.TotalPageLabel.Name = "TotalPageLabel";
            this.TotalPageLabel.Size = new System.Drawing.Size(14, 35);
            this.TotalPageLabel.Text = "0";
            // 
            // FirstPageButton
            // 
            this.FirstPageButton.AutoSize = false;
            this.FirstPageButton.BackColor = System.Drawing.Color.Transparent;
            this.FirstPageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FirstPageButton.Image = global::Cube.Properties.Resources.arrow_first;
            this.FirstPageButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.FirstPageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FirstPageButton.Margin = new System.Windows.Forms.Padding(2, 0, -2, 0);
            this.FirstPageButton.Name = "FirstPageButton";
            this.FirstPageButton.Size = new System.Drawing.Size(35, 40);
            this.FirstPageButton.Text = "toolStripButton1";
            this.FirstPageButton.Click += new System.EventHandler(this.FirstPageButton_Click);
            this.FirstPageButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FirstPageButton_MouseDown);
            this.FirstPageButton.MouseEnter += new System.EventHandler(this.FirstPageButton_MouseEnter);
            this.FirstPageButton.MouseLeave += new System.EventHandler(this.FirstPageButton_MouseLeave);
            this.FirstPageButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FirstPageButton_MouseUp);
            // 
            // PreviousPageButton
            // 
            this.PreviousPageButton.AutoSize = false;
            this.PreviousPageButton.BackColor = System.Drawing.Color.Transparent;
            this.PreviousPageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PreviousPageButton.Image = global::Cube.Properties.Resources.arrow_prev;
            this.PreviousPageButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.PreviousPageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PreviousPageButton.Margin = new System.Windows.Forms.Padding(-2, 0, -2, 0);
            this.PreviousPageButton.Name = "PreviousPageButton";
            this.PreviousPageButton.Size = new System.Drawing.Size(35, 40);
            this.PreviousPageButton.Text = "前のページ";
            this.PreviousPageButton.Click += new System.EventHandler(this.PreviousPageButton_Click);
            this.PreviousPageButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PreviousPageButton_MouseDown);
            this.PreviousPageButton.MouseEnter += new System.EventHandler(this.PreviousPageButton_MouseEnter);
            this.PreviousPageButton.MouseLeave += new System.EventHandler(this.PreviousPageButton_MouseLeave);
            this.PreviousPageButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PreviousPageButton_MouseUp);
            // 
            // NextPageButton
            // 
            this.NextPageButton.AutoSize = false;
            this.NextPageButton.BackColor = System.Drawing.Color.Transparent;
            this.NextPageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NextPageButton.Image = global::Cube.Properties.Resources.arrow_next;
            this.NextPageButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.NextPageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NextPageButton.Margin = new System.Windows.Forms.Padding(-2, 0, -2, 0);
            this.NextPageButton.Name = "NextPageButton";
            this.NextPageButton.Size = new System.Drawing.Size(35, 40);
            this.NextPageButton.Text = "次のページ";
            this.NextPageButton.Click += new System.EventHandler(this.NextPageButton_Click);
            this.NextPageButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NextPageButton_MouseDown);
            this.NextPageButton.MouseEnter += new System.EventHandler(this.NextPageButton_MouseEnter);
            this.NextPageButton.MouseLeave += new System.EventHandler(this.NextPageButton_MouseLeave);
            this.NextPageButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.NextPageButton_MouseUp);
            // 
            // LastPageButton
            // 
            this.LastPageButton.AutoSize = false;
            this.LastPageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LastPageButton.Image = global::Cube.Properties.Resources.arrow_last;
            this.LastPageButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LastPageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LastPageButton.Margin = new System.Windows.Forms.Padding(-2, 0, 0, 0);
            this.LastPageButton.Name = "LastPageButton";
            this.LastPageButton.Size = new System.Drawing.Size(35, 40);
            this.LastPageButton.Text = "toolStripButton1";
            this.LastPageButton.Click += new System.EventHandler(this.LastPageButton_Click);
            this.LastPageButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LastPageButton_MouseDown);
            this.LastPageButton.MouseEnter += new System.EventHandler(this.LastPageButton_MouseEnter);
            this.LastPageButton.MouseLeave += new System.EventHandler(this.LastPageButton_MouseLeave);
            this.LastPageButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LastPageButton_MouseUp);
            // 
            // OnlyDisplayCategoryPageSeparator
            // 
            this.OnlyDisplayCategoryPageSeparator.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.OnlyDisplayCategoryPageSeparator.Name = "OnlyDisplayCategoryPageSeparator";
            this.OnlyDisplayCategoryPageSeparator.Size = new System.Drawing.Size(6, 40);
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchTextBox.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SearchTextBox.Margin = new System.Windows.Forms.Padding(0, 5, -1, 0);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(100, 35);
            this.SearchTextBox.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            // 
            // SearchButton
            // 
            this.SearchButton.AutoSize = false;
            this.SearchButton.BackColor = System.Drawing.Color.Transparent;
            this.SearchButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.SearchButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SearchButton.Image = global::Cube.Properties.Resources.search;
            this.SearchButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SearchButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SearchButton.Margin = new System.Windows.Forms.Padding(-2, 0, 0, 0);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(25, 40);
            this.SearchButton.Text = "検索する";
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            this.SearchButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SearchButton_MouseDown);
            this.SearchButton.MouseEnter += new System.EventHandler(this.SearchButton_MouseEnter);
            this.SearchButton.MouseLeave += new System.EventHandler(this.SearchButton_MouseLeave);
            this.SearchButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SearchButton_MouseUp);
            // 
            // OnlyDisplayCategorySearchSeparator
            // 
            this.OnlyDisplayCategorySearchSeparator.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.OnlyDisplayCategorySearchSeparator.Name = "OnlyDisplayCategorySearchSeparator";
            this.OnlyDisplayCategorySearchSeparator.Size = new System.Drawing.Size(6, 40);
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
            this.OnlyDisplayZoom400});
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
            // OnlyDisplayZoom400
            // 
            this.OnlyDisplayZoom400.Name = "OnlyDisplayZoom400";
            this.OnlyDisplayZoom400.Size = new System.Drawing.Size(102, 22);
            this.OnlyDisplayZoom400.Text = "400%";
            // 
            // ZoomInButton
            // 
            this.ZoomInButton.AutoSize = false;
            this.ZoomInButton.BackColor = System.Drawing.Color.Transparent;
            this.ZoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomInButton.Image = global::Cube.Properties.Resources.zoomin;
            this.ZoomInButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ZoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomInButton.Margin = new System.Windows.Forms.Padding(0, 0, -2, 0);
            this.ZoomInButton.Name = "ZoomInButton";
            this.ZoomInButton.Size = new System.Drawing.Size(37, 40);
            this.ZoomInButton.Text = "拡大する";
            this.ZoomInButton.Click += new System.EventHandler(this.ZoomInButton_Click);
            this.ZoomInButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ZoomInButton_MouseDown);
            this.ZoomInButton.MouseEnter += new System.EventHandler(this.ZoomInButton_MouseEnter);
            this.ZoomInButton.MouseLeave += new System.EventHandler(this.ZoomInButton_MouseLeave);
            this.ZoomInButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ZoomInButton_MouseUp);
            // 
            // ZoomOutButton
            // 
            this.ZoomOutButton.AutoSize = false;
            this.ZoomOutButton.BackColor = System.Drawing.Color.Transparent;
            this.ZoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomOutButton.Image = global::Cube.Properties.Resources.zoomout;
            this.ZoomOutButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ZoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomOutButton.Margin = new System.Windows.Forms.Padding(-2, 0, 0, 0);
            this.ZoomOutButton.Name = "ZoomOutButton";
            this.ZoomOutButton.Size = new System.Drawing.Size(37, 40);
            this.ZoomOutButton.Text = "縮小する";
            this.ZoomOutButton.Click += new System.EventHandler(this.ZoomOutButton_Click);
            this.ZoomOutButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ZoomOutButton_MouseDown);
            this.ZoomOutButton.MouseEnter += new System.EventHandler(this.ZoomOutButton_MouseEnter);
            this.ZoomOutButton.MouseLeave += new System.EventHandler(this.ZoomOutButton_MouseLeave);
            this.ZoomOutButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ZoomOutButton_MouseUp);
            // 
            // FitToWidthButton
            // 
            this.FitToWidthButton.AutoSize = false;
            this.FitToWidthButton.BackColor = System.Drawing.Color.Transparent;
            this.FitToWidthButton.CheckOnClick = true;
            this.FitToWidthButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FitToWidthButton.Image = global::Cube.Properties.Resources.fit2width;
            this.FitToWidthButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.FitToWidthButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FitToWidthButton.Margin = new System.Windows.Forms.Padding(0, 0, -2, 0);
            this.FitToWidthButton.Name = "FitToWidthButton";
            this.FitToWidthButton.Size = new System.Drawing.Size(37, 40);
            this.FitToWidthButton.Text = "ウィンドウの幅に合わせる";
            this.FitToWidthButton.CheckedChanged += new System.EventHandler(this.FitToWidthButton_CheckedChanged);
            this.FitToWidthButton.Click += new System.EventHandler(this.FitToWidthButton_Click);
            this.FitToWidthButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FitToWidthButton_MouseDown);
            this.FitToWidthButton.MouseEnter += new System.EventHandler(this.FitToWidthButton_MouseEnter);
            this.FitToWidthButton.MouseLeave += new System.EventHandler(this.FitToWidthButton_MouseLeave);
            this.FitToWidthButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FitToWidthButton_MouseUp);
            // 
            // FitToHeightButton
            // 
            this.FitToHeightButton.AutoSize = false;
            this.FitToHeightButton.BackColor = System.Drawing.Color.Transparent;
            this.FitToHeightButton.CheckOnClick = true;
            this.FitToHeightButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FitToHeightButton.Image = global::Cube.Properties.Resources.fit2height;
            this.FitToHeightButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.FitToHeightButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FitToHeightButton.Margin = new System.Windows.Forms.Padding(-2, 0, 0, 0);
            this.FitToHeightButton.Name = "FitToHeightButton";
            this.FitToHeightButton.Size = new System.Drawing.Size(37, 40);
            this.FitToHeightButton.Text = "ウィンドウの高さに合わせる";
            this.FitToHeightButton.CheckedChanged += new System.EventHandler(this.FitToHeightButton_CheckedChanged);
            this.FitToHeightButton.Click += new System.EventHandler(this.FitToHeightButton_Click);
            this.FitToHeightButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FitToHeightButton_MouseDown);
            this.FitToHeightButton.MouseEnter += new System.EventHandler(this.FitToHeightButton_MouseEnter);
            this.FitToHeightButton.MouseLeave += new System.EventHandler(this.FitToHeightButton_MouseLeave);
            this.FitToHeightButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FitToHeightButton_MouseUp);
            // 
            // LogoButton
            // 
            this.LogoButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.LogoButton.AutoSize = false;
            this.LogoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.LogoButton.Image = global::Cube.Properties.Resources.logo;
            this.LogoButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LogoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LogoButton.Margin = new System.Windows.Forms.Padding(0);
            this.LogoButton.Name = "LogoButton";
            this.LogoButton.Size = new System.Drawing.Size(119, 40);
            this.LogoButton.Text = "CubeSoft の Web ページへ移動する";
            this.LogoButton.Click += new System.EventHandler(this.LogoButton_Click);
            this.LogoButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LogoButton_MouseDown);
            this.LogoButton.MouseEnter += new System.EventHandler(this.LogoButton_MouseEnter);
            this.LogoButton.MouseLeave += new System.EventHandler(this.LogoButton_MouseLeave);
            this.LogoButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LogoButton_MouseUp);
            // 
            // OnlyDisplayCategoryZoomSeparator
            // 
            this.OnlyDisplayCategoryZoomSeparator.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.OnlyDisplayCategoryZoomSeparator.Name = "OnlyDisplayCategoryZoomSeparator";
            this.OnlyDisplayCategoryZoomSeparator.Size = new System.Drawing.Size(6, 40);
            // 
            // AdobeButton
            // 
            this.AdobeButton.BackColor = System.Drawing.Color.Transparent;
            this.AdobeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.AdobeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AdobeButton.Enabled = false;
            this.AdobeButton.Image = ((System.Drawing.Image)(resources.GetObject("AdobeButton.Image")));
            this.AdobeButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.AdobeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AdobeButton.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.AdobeButton.Name = "AdobeButton";
            this.AdobeButton.Size = new System.Drawing.Size(23, 35);
            this.AdobeButton.Text = "Adobe Reader で開く";
            this.AdobeButton.Visible = false;
            this.AdobeButton.Click += new System.EventHandler(this.AdobeButton_Click);
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
            this.MenuSplitContainer.Size = new System.Drawing.Size(934, 562);
            this.MenuSplitContainer.SplitterDistance = 40;
            this.MenuSplitContainer.SplitterWidth = 1;
            this.MenuSplitContainer.TabIndex = 4;
            // 
            // NavigationSplitContainer
            // 
            this.NavigationSplitContainer.BackColor = System.Drawing.Color.Transparent;
            this.NavigationSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NavigationSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.NavigationSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.NavigationSplitContainer.Name = "NavigationSplitContainer";
            // 
            // NavigationSplitContainer.Panel1
            // 
            this.NavigationSplitContainer.Panel1.BackColor = System.Drawing.Color.DimGray;
            this.NavigationSplitContainer.Panel1MinSize = 100;
            // 
            // NavigationSplitContainer.Panel2
            // 
            this.NavigationSplitContainer.Panel2.BackColor = System.Drawing.Color.LightGray;
            this.NavigationSplitContainer.Panel2.BackgroundImage = global::Cube.Properties.Resources.background_tab;
            this.NavigationSplitContainer.Panel2.Controls.Add(this.PageViewerTabControl);
            this.NavigationSplitContainer.Size = new System.Drawing.Size(934, 521);
            this.NavigationSplitContainer.SplitterDistance = 151;
            this.NavigationSplitContainer.SplitterWidth = 3;
            this.NavigationSplitContainer.TabIndex = 2;
            // 
            // PageViewerTabControl
            // 
            this.PageViewerTabControl.AllowDrop = true;
            this.PageViewerTabControl.Controls.Add(this.DefaultTabPage);
            this.PageViewerTabControl.DisplayStyle = System.Windows.Forms.TabStyle.VS2010;
            // 
            // 
            // 
            this.PageViewerTabControl.DisplayStyleProvider.BorderColor = System.Drawing.Color.Transparent;
            this.PageViewerTabControl.DisplayStyleProvider.BorderColorHot = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(167)))), ((int)(((byte)(183)))));
            this.PageViewerTabControl.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.PageViewerTabControl.DisplayStyleProvider.CloserColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(99)))), ((int)(((byte)(61)))));
            this.PageViewerTabControl.DisplayStyleProvider.FocusTrack = false;
            this.PageViewerTabControl.DisplayStyleProvider.HotTrack = true;
            this.PageViewerTabControl.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PageViewerTabControl.DisplayStyleProvider.Opacity = 1F;
            this.PageViewerTabControl.DisplayStyleProvider.Overlap = 0;
            this.PageViewerTabControl.DisplayStyleProvider.Padding = new System.Drawing.Point(10, 5);
            this.PageViewerTabControl.DisplayStyleProvider.Radius = 3;
            this.PageViewerTabControl.DisplayStyleProvider.ShowTabCloser = true;
            this.PageViewerTabControl.DisplayStyleProvider.TextColor = System.Drawing.Color.White;
            this.PageViewerTabControl.DisplayStyleProvider.TextColorDisabled = System.Drawing.Color.WhiteSmoke;
            this.PageViewerTabControl.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText;
            this.PageViewerTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PageViewerTabControl.HotTrack = true;
            this.PageViewerTabControl.Location = new System.Drawing.Point(0, 0);
            this.PageViewerTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.PageViewerTabControl.Name = "PageViewerTabControl";
            this.PageViewerTabControl.SelectedIndex = 0;
            this.PageViewerTabControl.Size = new System.Drawing.Size(780, 521);
            this.PageViewerTabControl.TabIndex = 0;
            this.PageViewerTabControl.TabClosing += new System.EventHandler<System.Windows.Forms.TabControlCancelEventArgs>(this.PageViewerTabControl_TabClosing);
            this.PageViewerTabControl.SelectedIndexChanged += new System.EventHandler(this.PageViewerTabControl_SelectedIndexChanged);
            // 
            // DefaultTabPage
            // 
            this.DefaultTabPage.AllowDrop = true;
            this.DefaultTabPage.AutoScroll = true;
            this.DefaultTabPage.BackColor = System.Drawing.Color.DimGray;
            this.DefaultTabPage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DefaultTabPage.Location = new System.Drawing.Point(4, 27);
            this.DefaultTabPage.Name = "DefaultTabPage";
            this.DefaultTabPage.Size = new System.Drawing.Size(772, 490);
            this.DefaultTabPage.TabIndex = 0;
            this.DefaultTabPage.Text = "(無題)";
            this.DefaultTabPage.Scroll += new System.Windows.Forms.ScrollEventHandler(this.TabPage_Scroll);
            this.DefaultTabPage.DragDrop += new System.Windows.Forms.DragEventHandler(this.TabPage_DragDrop);
            this.DefaultTabPage.DragEnter += new System.Windows.Forms.DragEventHandler(this.TabPage_DragEnter);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(934, 562);
            this.Controls.Add(this.MenuSplitContainer);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(950, 200);
            this.Name = "MainForm";
            this.Text = "CubePDF Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.MenuToolStrip.ResumeLayout(false);
            this.MenuToolStrip.PerformLayout();
            this.MenuSplitContainer.Panel1.ResumeLayout(false);
            this.MenuSplitContainer.Panel2.ResumeLayout(false);
            this.MenuSplitContainer.ResumeLayout(false);
            this.NavigationSplitContainer.Panel2.ResumeLayout(false);
            this.NavigationSplitContainer.ResumeLayout(false);
            this.PageViewerTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.ToolStripButton PrintButton;
        private System.Windows.Forms.SplitContainer MenuSplitContainer;
        private System.Windows.Forms.ToolStripButton ThumbButton;
        private System.Windows.Forms.SplitContainer NavigationSplitContainer;
        private System.Windows.Forms.CustomTabControl PageViewerTabControl;
        private System.Windows.Forms.TabPage DefaultTabPage;
        private System.Windows.Forms.ToolStripButton MenuModeButton;
        private System.Windows.Forms.ToolStripButton OpenButton;
        private System.Windows.Forms.ToolStripButton FirstPageButton;
        private System.Windows.Forms.ToolStripLabel OnlyDisplayCategoryTotalPageLabel;
        private System.Windows.Forms.ToolStripButton LastPageButton;
        private System.Windows.Forms.ToolStripButton LogoButton;
        private System.Windows.Forms.ToolStripSeparator OnlyDisplayCategoryFileSeparator;
        private System.Windows.Forms.ToolStripSeparator OnlyDisplayCategoryPageSeparator;
        private System.Windows.Forms.ToolStripSeparator OnlyDisplayCategorySearchSeparator;
        private System.Windows.Forms.ToolStripSeparator OnlyDisplayCategoryZoomSeparator;
        private System.Windows.Forms.ToolStripButton AdobeButton;
        private System.Windows.Forms.ToolStripMenuItem OnlyDisplayZoom400;
    }
}

