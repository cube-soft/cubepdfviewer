using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cube
{
    public partial class PasswordDialog : Form
    {
        public PasswordDialog()
        {
            InitializeComponent();
        }

        public String Password
        {
            get
            {
                return this.PasswordBox.Text;
            }
        }

        private void OK_btn_Click(object sender, EventArgs e)
        {
            this.OK_btn.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    
    }
}
