using System;
using System.Windows.Forms;

namespace ConcentrationCardGame
{
    public partial class MainForm : Form
    {
        private AboutBoxForm aboutBox = new AboutBoxForm();

        public MainForm()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutBox.Text = "About Concentration Game";
            aboutBox.ShowDialog();
        }
    }
}