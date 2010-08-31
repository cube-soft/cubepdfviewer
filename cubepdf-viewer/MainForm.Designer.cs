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
            this.MenuTool = new System.Windows.Forms.ToolStrip();
            this.MenuOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuZoomIn = new System.Windows.Forms.ToolStripButton();
            this.MenuZoomText = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuZoomOut = new System.Windows.Forms.ToolStripButton();
            this.MenuFitToWidth = new System.Windows.Forms.ToolStripButton();
            this.MenuFitToHeight = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuFirstPage = new System.Windows.Forms.ToolStripButton();
            this.MenuPrevious = new System.Windows.Forms.ToolStripButton();
            this.MenuCurrentPage = new System.Windows.Forms.ToolStripTextBox();
            this.MenuTotalPage = new System.Windows.Forms.ToolStripLabel();
            this.MenuNext = new System.Windows.Forms.ToolStripButton();
            this.MenuLastPage = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuSearchText = new System.Windows.Forms.ToolStripTextBox();
            this.MenuSearch = new System.Windows.Forms.ToolStripButton();
            this.StatausBar = new System.Windows.Forms.StatusStrip();
            this.StatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.MenuTool.SuspendLayout();
            this.StatausBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuTool
            // 
            this.MenuTool.AllowMerge = false;
            this.MenuTool.AutoSize = false;
            this.MenuTool.GripMargin = new System.Windows.Forms.Padding(5);
            this.MenuTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.MenuTool.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.MenuTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuOpen,
            this.toolStripSeparator2,
            this.MenuZoomIn,
            this.MenuZoomText,
            this.MenuZoomOut,
            this.MenuFitToWidth,
            this.MenuFitToHeight,
            this.toolStripSeparator1,
            this.MenuFirstPage,
            this.MenuPrevious,
            this.MenuCurrentPage,
            this.MenuTotalPage,
            this.MenuNext,
            this.MenuLastPage,
            this.toolStripSeparator3,
            this.MenuSearchText,
            this.MenuSearch});
            this.MenuTool.Location = new System.Drawing.Point(0, 0);
            this.MenuTool.Name = "MenuTool";
            this.MenuTool.Padding = new System.Windows.Forms.Padding(2);
            this.MenuTool.Size = new System.Drawing.Size(784, 50);
            this.MenuTool.TabIndex = 1;
            this.MenuTool.Text = "toolStrip1";
            // 
            // MenuOpen
            // 
            this.MenuOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuOpen.Image = global::Cube.Properties.Resources.open;
            this.MenuOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuOpen.Margin = new System.Windows.Forms.Padding(10, 2, 2, 2);
            this.MenuOpen.Name = "MenuOpen";
            this.MenuOpen.Size = new System.Drawing.Size(36, 42);
            this.MenuOpen.Text = "開く";
            this.MenuOpen.Click += new System.EventHandler(this.MenuOpen_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 46);
            // 
            // MenuZoomIn
            // 
            this.MenuZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuZoomIn.Image = global::Cube.Properties.Resources.zoomin;
            this.MenuZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuZoomIn.Margin = new System.Windows.Forms.Padding(2);
            this.MenuZoomIn.Name = "MenuZoomIn";
            this.MenuZoomIn.Size = new System.Drawing.Size(36, 42);
            this.MenuZoomIn.Text = "拡大";
            this.MenuZoomIn.Click += new System.EventHandler(this.MenuZoomIn_Click);
            // 
            // MenuZoomText
            // 
            this.MenuZoomText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuZoomText.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10});
            this.MenuZoomText.Image = ((System.Drawing.Image)(resources.GetObject("MenuZoomText.Image")));
            this.MenuZoomText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuZoomText.Margin = new System.Windows.Forms.Padding(2);
            this.MenuZoomText.Name = "MenuZoomText";
            this.MenuZoomText.Size = new System.Drawing.Size(42, 42);
            this.MenuZoomText.Text = "100%";
            this.MenuZoomText.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MenuZoomText_DropDownItemClicked);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(94, 22);
            this.toolStripMenuItem2.Text = "25%";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(94, 22);
            this.toolStripMenuItem3.Text = "50%";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(94, 22);
            this.toolStripMenuItem4.Text = "75%";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(94, 22);
            this.toolStripMenuItem5.Text = "100%";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(94, 22);
            this.toolStripMenuItem6.Text = "125%";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(94, 22);
            this.toolStripMenuItem7.Text = "150%";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(94, 22);
            this.toolStripMenuItem8.Text = "200%";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(94, 22);
            this.toolStripMenuItem9.Text = "400%";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(94, 22);
            this.toolStripMenuItem10.Text = "800%";
            // 
            // MenuZoomOut
            // 
            this.MenuZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuZoomOut.Image = global::Cube.Properties.Resources.zoomout;
            this.MenuZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuZoomOut.Margin = new System.Windows.Forms.Padding(2);
            this.MenuZoomOut.Name = "MenuZoomOut";
            this.MenuZoomOut.Size = new System.Drawing.Size(36, 42);
            this.MenuZoomOut.Text = "縮小";
            this.MenuZoomOut.Click += new System.EventHandler(this.MenuZoomOut_Click);
            // 
            // MenuFitToWidth
            // 
            this.MenuFitToWidth.BackColor = System.Drawing.SystemColors.Control;
            this.MenuFitToWidth.CheckOnClick = true;
            this.MenuFitToWidth.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuFitToWidth.Image = global::Cube.Properties.Resources.fit2width;
            this.MenuFitToWidth.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuFitToWidth.Margin = new System.Windows.Forms.Padding(2);
            this.MenuFitToWidth.Name = "MenuFitToWidth";
            this.MenuFitToWidth.Size = new System.Drawing.Size(36, 42);
            this.MenuFitToWidth.Text = "ウィンドウの幅に合わせる";
            this.MenuFitToWidth.Click += new System.EventHandler(this.MenuFitToWidth_Click);
            // 
            // MenuFitToHeight
            // 
            this.MenuFitToHeight.CheckOnClick = true;
            this.MenuFitToHeight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuFitToHeight.Image = global::Cube.Properties.Resources.fit2height;
            this.MenuFitToHeight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuFitToHeight.Margin = new System.Windows.Forms.Padding(2);
            this.MenuFitToHeight.Name = "MenuFitToHeight";
            this.MenuFitToHeight.Size = new System.Drawing.Size(36, 42);
            this.MenuFitToHeight.Text = "ウィンドウの高さに合わせる";
            this.MenuFitToHeight.Click += new System.EventHandler(this.MenuFitToHeight_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 46);
            // 
            // MenuFirstPage
            // 
            this.MenuFirstPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuFirstPage.Image = global::Cube.Properties.Resources.arrow_first;
            this.MenuFirstPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuFirstPage.Margin = new System.Windows.Forms.Padding(2);
            this.MenuFirstPage.Name = "MenuFirstPage";
            this.MenuFirstPage.Size = new System.Drawing.Size(36, 42);
            this.MenuFirstPage.Text = "最初のページ";
            this.MenuFirstPage.Click += new System.EventHandler(this.MenuFirstPage_Click);
            // 
            // MenuPrevious
            // 
            this.MenuPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuPrevious.Image = global::Cube.Properties.Resources.arrow_prev;
            this.MenuPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuPrevious.Margin = new System.Windows.Forms.Padding(2);
            this.MenuPrevious.Name = "MenuPrevious";
            this.MenuPrevious.Size = new System.Drawing.Size(36, 42);
            this.MenuPrevious.Text = "前ページ";
            this.MenuPrevious.Click += new System.EventHandler(this.MenuPrevious_Click);
            // 
            // MenuCurrentPage
            // 
            this.MenuCurrentPage.AutoSize = false;
            this.MenuCurrentPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MenuCurrentPage.Font = new System.Drawing.Font("メイリオ", 10F);
            this.MenuCurrentPage.Margin = new System.Windows.Forms.Padding(2);
            this.MenuCurrentPage.Name = "MenuCurrentPage";
            this.MenuCurrentPage.Padding = new System.Windows.Forms.Padding(2);
            this.MenuCurrentPage.Size = new System.Drawing.Size(26, 27);
            this.MenuCurrentPage.Text = "0";
            this.MenuCurrentPage.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MenuCurrentPage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MenuCurrentPage_KeyDown);
            // 
            // MenuTotalPage
            // 
            this.MenuTotalPage.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MenuTotalPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 5);
            this.MenuTotalPage.Name = "MenuTotalPage";
            this.MenuTotalPage.Size = new System.Drawing.Size(27, 39);
            this.MenuTotalPage.Text = "/ 0";
            // 
            // MenuNext
            // 
            this.MenuNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuNext.Image = global::Cube.Properties.Resources.arrow_next;
            this.MenuNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuNext.Margin = new System.Windows.Forms.Padding(2);
            this.MenuNext.Name = "MenuNext";
            this.MenuNext.Size = new System.Drawing.Size(36, 42);
            this.MenuNext.Text = "次ページ";
            this.MenuNext.Click += new System.EventHandler(this.MenuNext_Click);
            // 
            // MenuLastPage
            // 
            this.MenuLastPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuLastPage.Image = global::Cube.Properties.Resources.arrow_last;
            this.MenuLastPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuLastPage.Margin = new System.Windows.Forms.Padding(2);
            this.MenuLastPage.Name = "MenuLastPage";
            this.MenuLastPage.Size = new System.Drawing.Size(36, 42);
            this.MenuLastPage.Text = "最後のページ";
            this.MenuLastPage.Click += new System.EventHandler(this.MenuLastPage_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 46);
            // 
            // MenuSearchText
            // 
            this.MenuSearchText.AutoSize = false;
            this.MenuSearchText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MenuSearchText.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MenuSearchText.Margin = new System.Windows.Forms.Padding(2);
            this.MenuSearchText.Name = "MenuSearchText";
            this.MenuSearchText.Padding = new System.Windows.Forms.Padding(2);
            this.MenuSearchText.Size = new System.Drawing.Size(114, 27);
            this.MenuSearchText.ToolTipText = "検索ワード";
            this.MenuSearchText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MenuSearchText_KeyDown);
            this.MenuSearchText.TextChanged += new System.EventHandler(this.MenuSearchText_TextChanged);
            // 
            // MenuSearch
            // 
            this.MenuSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuSearch.Image = global::Cube.Properties.Resources.search;
            this.MenuSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MenuSearch.Margin = new System.Windows.Forms.Padding(2);
            this.MenuSearch.Name = "MenuSearch";
            this.MenuSearch.Size = new System.Drawing.Size(36, 42);
            this.MenuSearch.Text = "検索";
            this.MenuSearch.Click += new System.EventHandler(this.MenuSearch_Click);
            // 
            // StatausBar
            // 
            this.StatausBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusText});
            this.StatausBar.Location = new System.Drawing.Point(0, 740);
            this.StatausBar.Name = "StatausBar";
            this.StatausBar.Size = new System.Drawing.Size(784, 22);
            this.StatausBar.TabIndex = 2;
            this.StatausBar.Text = "statusStrip1";
            // 
            // StatusText
            // 
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(37, 17);
            this.StatusText.Text = "Ready";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 762);
            this.Controls.Add(this.StatausBar);
            this.Controls.Add(this.MenuTool);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "CubePDF Viewer";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.MenuTool.ResumeLayout(false);
            this.MenuTool.PerformLayout();
            this.StatausBar.ResumeLayout(false);
            this.StatausBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PDFViewer.PageViewer MainViewer;
        private System.Windows.Forms.ToolStrip MenuTool;
        private System.Windows.Forms.ToolStripButton MenuOpen;
        private System.Windows.Forms.ToolStripButton MenuPrevious;
        private System.Windows.Forms.ToolStripButton MenuNext;
        private System.Windows.Forms.ToolStripTextBox MenuCurrentPage;
        private System.Windows.Forms.ToolStripLabel MenuTotalPage;
        private System.Windows.Forms.ToolStripButton MenuZoomIn;
        private System.Windows.Forms.ToolStripButton MenuZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripTextBox MenuSearchText;
        private System.Windows.Forms.ToolStripButton MenuSearch;
        private System.Windows.Forms.StatusStrip StatausBar;
        private System.Windows.Forms.ToolStripButton MenuFitToWidth;
        private System.Windows.Forms.ToolStripButton MenuFitToHeight;
        private System.Windows.Forms.ToolStripDropDownButton MenuZoomText;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripStatusLabel StatusText;
        private System.Windows.Forms.ToolStripButton MenuLastPage;
        private System.Windows.Forms.ToolStripButton MenuFirstPage;

    }
}

