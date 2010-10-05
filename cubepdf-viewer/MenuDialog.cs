using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cube {
    public partial class MenuDialog : Form {
        public MenuDialog() {
            InitializeComponent();
            var image = new Bitmap(32, 32);
            var g = Graphics.FromImage(image);
            g.DrawIcon(SystemIcons.Information, 0, 0);
            g.Dispose();
            this.IconPictureBox.Image = image;
        }

        public bool NoShowNext {
            get { return this.NoShowNextCheckBox.Checked; }
        }

        private void OKButton_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
