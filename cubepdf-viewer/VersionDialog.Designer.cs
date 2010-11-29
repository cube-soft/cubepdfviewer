namespace Cube
{
    partial class VersionDialog
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.OKButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.CopyrightLabel = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.CubePDFLinkLabel = new System.Windows.Forms.LinkLabel();
            this.LogoPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(83, 120);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TitleLabel.Location = new System.Drawing.Point(80, 16);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(115, 13);
            this.TitleLabel.TabIndex = 2;
            this.TitleLabel.Text = "CubePDF Viewer";
            // 
            // CopyrightLabel
            // 
            this.CopyrightLabel.AutoSize = true;
            this.CopyrightLabel.Location = new System.Drawing.Point(80, 54);
            this.CopyrightLabel.Name = "CopyrightLabel";
            this.CopyrightLabel.Size = new System.Drawing.Size(151, 12);
            this.CopyrightLabel.TabIndex = 4;
            this.CopyrightLabel.Text = "Copyright(C) 2010 CubeSoft.";
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(80, 42);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(44, 12);
            this.VersionLabel.TabIndex = 3;
            this.VersionLabel.Text = "Version";
            // 
            // CubePDFLinkLabel
            // 
            this.CubePDFLinkLabel.AutoSize = true;
            this.CubePDFLinkLabel.Location = new System.Drawing.Point(31, 79);
            this.CubePDFLinkLabel.Name = "CubePDFLinkLabel";
            this.CubePDFLinkLabel.Size = new System.Drawing.Size(211, 12);
            this.CubePDFLinkLabel.TabIndex = 1;
            this.CubePDFLinkLabel.TabStop = true;
            this.CubePDFLinkLabel.Text = "http://www.cube-soft.jp/cubepdfviewer/";
            this.CubePDFLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CubePDFLinkLabel_LinkClicked);
            // 
            // LogoPictureBox
            // 
            this.LogoPictureBox.Location = new System.Drawing.Point(33, 25);
            this.LogoPictureBox.Name = "LogoPictureBox";
            this.LogoPictureBox.Size = new System.Drawing.Size(32, 32);
            this.LogoPictureBox.TabIndex = 5;
            this.LogoPictureBox.TabStop = false;
            // 
            // VersionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 155);
            this.Controls.Add(this.LogoPictureBox);
            this.Controls.Add(this.CubePDFLinkLabel);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.CopyrightLabel);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.TitleLabel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VersionDialog";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "バージョン情報";
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label CopyrightLabel;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.LinkLabel CubePDFLinkLabel;
        private System.Windows.Forms.PictureBox LogoPictureBox;

    }
}