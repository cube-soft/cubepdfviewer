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
            this.NewTabButton = new System.Windows.Forms.ToolStripButton();
            this.OpenButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ZoomInButton = new System.Windows.Forms.ToolStripButton();
            this.ZoomDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.Zoom25 = new System.Windows.Forms.ToolStripMenuItem();
            this.Zoom50 = new System.Windows.Forms.ToolStripMenuItem();
            this.Zoom75 = new System.Windows.Forms.ToolStripMenuItem();
            this.Zoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.Zoom125 = new System.Windows.Forms.ToolStripMenuItem();
            this.Zoom150 = new System.Windows.Forms.ToolStripMenuItem();
            this.Zoom200 = new System.Windows.Forms.ToolStripMenuItem();
            this.Zoom400 = new System.Windows.Forms.ToolStripMenuItem();
            this.Zoom800 = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomOutButton = new System.Windows.Forms.ToolStripButton();
            this.FitToWidthButton = new System.Windows.Forms.ToolStripButton();
            this.FitToHeightButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FirstPageButton = new System.Windows.Forms.ToolStripButton();
            this.PreviousPageButton = new System.Windows.Forms.ToolStripButton();
            this.CurrentPageTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.TotalPageLabel = new System.Windows.Forms.ToolStripLabel();
            this.NextPageButton = new System.Windows.Forms.ToolStripButton();
            this.LastPageButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.SearchTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.SearchButton = new System.Windows.Forms.ToolStripButton();
            this.FooterStatusStrip = new System.Windows.Forms.StatusStrip();
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.PageViewerTabControl = new System.Windows.Forms.TabControl();
            this.DefaultTabPage = new System.Windows.Forms.TabPage();
            this.MenuToolStrip.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            this.PageViewerTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuToolStrip
            // 
            this.MenuToolStrip.AutoSize = false;
            this.MenuToolStrip.GripMargin = new System.Windows.Forms.Padding(5);
            this.MenuToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.MenuToolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.MenuToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewTabButton,
            this.OpenButton,
            this.toolStripSeparator1,
            this.ZoomInButton,
            this.ZoomDropDownButton,
            this.ZoomOutButton,
            this.FitToWidthButton,
            this.FitToHeightButton,
            this.toolStripSeparator2,
            this.FirstPageButton,
            this.PreviousPageButton,
            this.CurrentPageTextBox,
            this.TotalPageLabel,
            this.NextPageButton,
            this.LastPageButton,
            this.toolStripSeparator3,
            this.SearchTextBox,
            this.SearchButton});
            this.MenuToolStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuToolStrip.Name = "MenuToolStrip";
            this.MenuToolStrip.Size = new System.Drawing.Size(792, 40);
            this.MenuToolStrip.TabIndex = 0;
            this.MenuToolStrip.Text = "メニュー";
            // 
            // NewTabButton
            // 
            this.NewTabButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewTabButton.Image = global::Cube.Properties.Resources.newtab;
            this.NewTabButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewTabButton.Margin = new System.Windows.Forms.Padding(2);
            this.NewTabButton.Name = "NewTabButton";
            this.NewTabButton.Size = new System.Drawing.Size(28, 36);
            this.NewTabButton.Text = "新しいタブ";
            this.NewTabButton.Click += new System.EventHandler(this.NewTabButton_Click);
            // 
            // OpenButton
            // 
            this.OpenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenButton.Image = global::Cube.Properties.Resources.open;
            this.OpenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenButton.Margin = new System.Windows.Forms.Padding(2);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(28, 36);
            this.OpenButton.Text = "開く";
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // ZoomInButton
            // 
            this.ZoomInButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomInButton.Image = global::Cube.Properties.Resources.zoomin;
            this.ZoomInButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomInButton.Margin = new System.Windows.Forms.Padding(2);
            this.ZoomInButton.Name = "ZoomInButton";
            this.ZoomInButton.Size = new System.Drawing.Size(28, 36);
            this.ZoomInButton.Text = "拡大する";
            this.ZoomInButton.Click += new System.EventHandler(this.ZoomInButton_Click);
            // 
            // ZoomDropDownButton
            // 
            this.ZoomDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ZoomDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Zoom25,
            this.Zoom50,
            this.Zoom75,
            this.Zoom100,
            this.Zoom125,
            this.Zoom150,
            this.Zoom200,
            this.Zoom400,
            this.Zoom800});
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
            // Zoom25
            // 
            this.Zoom25.Name = "Zoom25";
            this.Zoom25.Size = new System.Drawing.Size(102, 22);
            this.Zoom25.Text = "25%";
            // 
            // Zoom50
            // 
            this.Zoom50.Name = "Zoom50";
            this.Zoom50.Size = new System.Drawing.Size(102, 22);
            this.Zoom50.Text = "50%";
            // 
            // Zoom75
            // 
            this.Zoom75.Name = "Zoom75";
            this.Zoom75.Size = new System.Drawing.Size(102, 22);
            this.Zoom75.Text = "75%";
            // 
            // Zoom100
            // 
            this.Zoom100.Name = "Zoom100";
            this.Zoom100.Size = new System.Drawing.Size(102, 22);
            this.Zoom100.Text = "100%";
            // 
            // Zoom125
            // 
            this.Zoom125.Name = "Zoom125";
            this.Zoom125.Size = new System.Drawing.Size(102, 22);
            this.Zoom125.Text = "125%";
            // 
            // Zoom150
            // 
            this.Zoom150.Name = "Zoom150";
            this.Zoom150.Size = new System.Drawing.Size(102, 22);
            this.Zoom150.Text = "150%";
            // 
            // Zoom200
            // 
            this.Zoom200.Name = "Zoom200";
            this.Zoom200.Size = new System.Drawing.Size(102, 22);
            this.Zoom200.Text = "200%";
            // 
            // Zoom400
            // 
            this.Zoom400.Name = "Zoom400";
            this.Zoom400.Size = new System.Drawing.Size(102, 22);
            this.Zoom400.Text = "400%";
            // 
            // Zoom800
            // 
            this.Zoom800.Name = "Zoom800";
            this.Zoom800.Size = new System.Drawing.Size(102, 22);
            this.Zoom800.Text = "800%";
            // 
            // ZoomOutButton
            // 
            this.ZoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZoomOutButton.Image = global::Cube.Properties.Resources.zoomout;
            this.ZoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZoomOutButton.Margin = new System.Windows.Forms.Padding(2);
            this.ZoomOutButton.Name = "ZoomOutButton";
            this.ZoomOutButton.Size = new System.Drawing.Size(28, 36);
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
            this.FitToWidthButton.Size = new System.Drawing.Size(28, 36);
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
            this.FitToHeightButton.Size = new System.Drawing.Size(28, 36);
            this.FitToHeightButton.Text = "ウィンドウの高さに合わせる";
            this.FitToHeightButton.Click += new System.EventHandler(this.FitToHeightButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // FirstPageButton
            // 
            this.FirstPageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FirstPageButton.Image = global::Cube.Properties.Resources.arrow_first;
            this.FirstPageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FirstPageButton.Margin = new System.Windows.Forms.Padding(2);
            this.FirstPageButton.Name = "FirstPageButton";
            this.FirstPageButton.Size = new System.Drawing.Size(28, 36);
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
            this.PreviousPageButton.Size = new System.Drawing.Size(28, 36);
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
            this.NextPageButton.Size = new System.Drawing.Size(28, 36);
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
            this.LastPageButton.Size = new System.Drawing.Size(28, 36);
            this.LastPageButton.Text = "最後のページ";
            this.LastPageButton.Click += new System.EventHandler(this.LastPageButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 40);
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
            this.SearchButton.Size = new System.Drawing.Size(28, 36);
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
            // MainSplitContainer
            // 
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 40);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.BackColor = System.Drawing.SystemColors.Control;
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.PageViewerTabControl);
            this.MainSplitContainer.Size = new System.Drawing.Size(792, 511);
            this.MainSplitContainer.SplitterDistance = 264;
            this.MainSplitContainer.TabIndex = 2;
            // 
            // PageViewerTabControl
            // 
            this.PageViewerTabControl.Controls.Add(this.DefaultTabPage);
            this.PageViewerTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PageViewerTabControl.Location = new System.Drawing.Point(0, 0);
            this.PageViewerTabControl.Name = "PageViewerTabControl";
            this.PageViewerTabControl.SelectedIndex = 0;
            this.PageViewerTabControl.Size = new System.Drawing.Size(524, 511);
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
            this.DefaultTabPage.Size = new System.Drawing.Size(516, 485);
            this.DefaultTabPage.TabIndex = 0;
            this.DefaultTabPage.Text = "(無題)";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.MainSplitContainer);
            this.Controls.Add(this.FooterStatusStrip);
            this.Controls.Add(this.MenuToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "CubePDF Viewer";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.MenuToolStrip.ResumeLayout(false);
            this.MenuToolStrip.PerformLayout();
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            this.MainSplitContainer.ResumeLayout(false);
            this.PageViewerTabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip MenuToolStrip;
        private System.Windows.Forms.ToolStripButton OpenButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ZoomInButton;
        private System.Windows.Forms.ToolStripDropDownButton ZoomDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem Zoom25;
        private System.Windows.Forms.ToolStripMenuItem Zoom50;
        private System.Windows.Forms.ToolStripMenuItem Zoom75;
        private System.Windows.Forms.ToolStripMenuItem Zoom100;
        private System.Windows.Forms.ToolStripMenuItem Zoom125;
        private System.Windows.Forms.ToolStripMenuItem Zoom150;
        private System.Windows.Forms.ToolStripMenuItem Zoom200;
        private System.Windows.Forms.ToolStripMenuItem Zoom400;
        private System.Windows.Forms.ToolStripMenuItem Zoom800;
        private System.Windows.Forms.ToolStripButton ZoomOutButton;
        private System.Windows.Forms.ToolStripButton FitToWidthButton;
        private System.Windows.Forms.ToolStripButton FitToHeightButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton FirstPageButton;
        private System.Windows.Forms.ToolStripButton PreviousPageButton;
        private System.Windows.Forms.ToolStripTextBox CurrentPageTextBox;
        private System.Windows.Forms.ToolStripLabel TotalPageLabel;
        private System.Windows.Forms.ToolStripButton NextPageButton;
        private System.Windows.Forms.ToolStripButton LastPageButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripTextBox SearchTextBox;
        private System.Windows.Forms.ToolStripButton SearchButton;
        private System.Windows.Forms.StatusStrip FooterStatusStrip;
        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.TabControl PageViewerTabControl;
        private System.Windows.Forms.TabPage DefaultTabPage;
        private System.Windows.Forms.ToolStripButton NewTabButton;
    }
}

