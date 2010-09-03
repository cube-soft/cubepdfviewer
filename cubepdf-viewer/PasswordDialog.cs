/* ------------------------------------------------------------------------- */
/*
 *  PasswordDialog.cs
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
    /// PasswordDialog
    /* --------------------------------------------------------------------- */
    public partial class PasswordDialog : Form {
        /* ----------------------------------------------------------------- */
        ///
        /// Constructor
        /// 
        /// <summary>
        /// 対象となるファイル名を指定する．
        /// </summary>
        /// 
        /* ----------------------------------------------------------------- */
        public PasswordDialog(string path) {
            InitializeComponent();
            this.MessageLabel.Text = String.Format("「{0}」は保護されています。文書を開くパスワードを入力してください。", System.IO.Path.GetFileName(path));
            var image = new Bitmap(32, 32);
            var g = Graphics.FromImage(image); 
            g.DrawIcon(SystemIcons.Warning, 0, 0);
            g.Dispose();
            this.IconPictureBox.Image = image;
            this.OKButton.DialogResult = DialogResult.OK;
            this.CloseButton.DialogResult = DialogResult.Cancel;
        }

        /* ----------------------------------------------------------------- */
        /// Password
        /* ----------------------------------------------------------------- */
        public String Password {
            get {
                return this.PasswordBox.Text;
            }
        }

        /* ----------------------------------------------------------------- */
        /// OKButton_Click
        /* ----------------------------------------------------------------- */
        private void OKButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        /* ----------------------------------------------------------------- */
        /// CloseButton_Click
        /* ----------------------------------------------------------------- */
        private void CloseButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        /* ----------------------------------------------------------------- */
        /// PasswordBox_KeyDown
        /* ----------------------------------------------------------------- */
        private void PasswordBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) this.OKButton_Click(this.OKButton, e);
        }
    }
}
