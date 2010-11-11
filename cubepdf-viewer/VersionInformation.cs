using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Cube
{
    public partial class VersionInformation : Form
    {
        const string registry = @"Software\CubeSoft\CubePDF Viewer";
        const string registryKey = "Version";
        public VersionInformation()
        {
            InitializeComponent();
            var regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(registry, false);
            var version = regkey.GetValue(registryKey, "unknown");
            this.Version.Text = "version " + version;
            Logo.Size = new Size(64, 64);
            Logo.Image = Cube.Properties.Resources.cubepdf_viewer.ToBitmap();
            Logo.Location = new Point(10, 10);
        }

        private void cubePDFLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(cubePDFLink.Text);
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
