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

        // Function for Exit menu item
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Confirm if user wants to exit the application
            DialogResult userReply = MessageBox.Show("Are you sure you want to exit?",
                                                     "Exit",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Warning);

            // If user clicks on yes, close the application
            if (userReply == DialogResult.Yes)
            {
                this.Close();
            }
        }

        // Function for selecting a size from the menu
        private void SizeMenuItem_Click(object sender, EventArgs e)
        {
            // Get the size item that was clicked
            var selectedSize = sender as ToolStripMenuItem;

            // Uncheck all size items
            foreach (ToolStripMenuItem sizeItem in sizeToolStripMenuItem.DropDownItems)
            {
                sizeItem.Checked = false;
            }

            // Recheck the size item that was clicked
            selectedSize.Checked = true;
        }

        // Function for selecting a rule from the menu
        private void RuleMenuItem_Click(object sender, EventArgs e)
        {
            // Get the rule item that was clicked
            var selectedRule = sender as ToolStripMenuItem;

            // Uncheck all rule items
            foreach (ToolStripMenuItem ruleItem in ruleToolStripMenuItem.DropDownItems)
            {
                ruleItem.Checked = false;
            }

            // Recheck the rule item that was clicked
            selectedRule.Checked = true;
        }

        // Function for showing AboutBoxForm if user clicks on About menu item
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutBox.Text = "About Concentration Game";
            aboutBox.ShowDialog();
        }
    }
}