namespace Cube
{
    partial class PasswordDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.IconPictureBox = new System.Windows.Forms.PictureBox();
            this.MessageLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.IconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "パスワードを入力：";
            // 
            // PasswordBox
            // 
            this.PasswordBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.PasswordBox.Location = new System.Drawing.Point(196, 65);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.PasswordChar = '*';
            this.PasswordBox.Size = new System.Drawing.Size(186, 19);
            this.PasswordBox.TabIndex = 1;
            this.PasswordBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PasswordBox_KeyDown);
            // 
            // OKButton
            // 
            this.OKButton.Location = new System.Drawing.Point(226, 90);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 2;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(307, 90);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 3;
            this.CloseButton.Text = "キャンセル";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // IconPictureBox
            // 
            this.IconPictureBox.Location = new System.Drawing.Point(12, 12);
            this.IconPictureBox.Name = "IconPictureBox";
            this.IconPictureBox.Size = new System.Drawing.Size(32, 32);
            this.IconPictureBox.TabIndex = 4;
            this.IconPictureBox.TabStop = false;
            // 
            // MessageLabel
            // 
            this.MessageLabel.AutoEllipsis = true;
            this.MessageLabel.Location = new System.Drawing.Point(50, 22);
            this.MessageLabel.Name = "MessageLabel";
            this.MessageLabel.Size = new System.Drawing.Size(332, 31);
            this.MessageLabel.TabIndex = 5;
            this.MessageLabel.Text = "文書を開くパスワードを入力して下さい。";
            // 
            // PasswordDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 125);
            this.Controls.Add(this.MessageLabel);
            this.Controls.Add(this.IconPictureBox);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.PasswordBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PasswordDialog";
            this.Text = "パスワード";
            ((System.ComponentModel.ISupportInitialize)(this.IconPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.PictureBox IconPictureBox;
        private System.Windows.Forms.Label MessageLabel;
    }
}