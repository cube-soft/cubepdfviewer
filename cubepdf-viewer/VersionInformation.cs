using System;
using System.Windows.Forms;

namespace Cube
{
    public partial class VersionInformation : Form
    {
        const string registry = @"Software\CubeSoft\CubePDF Viewer";
        const string registryKey = "Version";

        public VersionInformation() {
            InitializeComponent();
            var regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(registry, false);
            var version = regkey.GetValue(registryKey, "unknown");
            this.Version.Text = "version " + version;
            Logo.Image = Cube.Properties.Resources.cubepdf_viewer.ToBitmap();
        }

        private void cubePDFLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start(cubePDFLink.Text);
        }

        private void OKButton_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
