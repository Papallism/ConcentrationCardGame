﻿using System;
using System.Windows.Forms;

namespace ConcentrationCardGame
{
    public partial class MainForm : Form
    {
        private const int SMALL = 5;
        private const int MEDIUM = 11;
        private const int LARGE = 17;

        private string selectedSizeName = "Small";
        private string selectedRuleName = "Match 2";

        private int numberOfMoves = 0;

        private AboutBoxForm aboutBox = new AboutBoxForm();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadCards();
        }

        // Function for loading cards
        private void LoadCards()
        {
            // Clear all controls from the flow panel
            flowLayoutPanelCards.Controls.Clear();

            for (int i = 0; i < GetSelectedSizeInt(selectedSizeName) * 2; i++)
            {
                Button button = new Button();
                button.Width = GetImageDimensions(selectedSizeName);
                button.Height = GetImageDimensions(selectedSizeName);
                button.Image = GetImage(selectedSizeName);
                button.BackgroundImageLayout = ImageLayout.Zoom;
                button.Click += Button_Click;

                flowLayoutPanelCards.Controls.Add(button);
            }
        }

        // Function for clicking on a card
        private void Button_Click(object sender, EventArgs e)
        {
            // Increase move counter
            numberOfMoves++;
        }

        // Function to get the number of pairs according to the selected size item
        private int GetSelectedSizeInt(string sizeName)
        {
            switch (sizeName)
            {
                case "Small":
                    return SMALL;

                case "Medium":
                    return MEDIUM;

                case "Large":
                    return LARGE;

                default:
                    return 0;
            }
        }

        // Function to get the button image according to the selected size item
        private System.Drawing.Bitmap GetImage(string sizeName)
        {
            switch (sizeName)
            {
                case "Small":
                    return Properties.Resources.QuestionMark100;

                case "Medium":
                    return Properties.Resources.QuestionMark80;

                case "Large":
                    return Properties.Resources.QuestionMark60;

                default:
                    return null;
            }
        }

        // Function to get the image dimensions according to the selected size item
        private int GetImageDimensions(string sizeName)
        {
            switch (sizeName)
            {
                case "Small":
                    return 100;

                case "Medium":
                    return 80;

                case "Large":
                    return 60;

                default:
                    return 0;
            }
        }

        // Function for New Game menu item
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Restart (Load cards again?)
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
            var selectedSizeItem = sender as ToolStripMenuItem;
            selectedSizeName = selectedSizeItem.Text;

            // Uncheck all size items
            foreach (ToolStripMenuItem sizeItem in sizeToolStripMenuItem.DropDownItems)
            {
                sizeItem.Checked = false;
            }

            // Recheck the size item that was clicked
            selectedSizeItem.Checked = true;

            LoadCards();
        }

        // Function for selecting a rule from the menu
        private void RuleMenuItem_Click(object sender, EventArgs e)
        {
            // Get the rule item that was clicked
            var selectedRuleItem = sender as ToolStripMenuItem;
            selectedRuleName = selectedRuleItem.Text;

            // Uncheck all rule items
            foreach (ToolStripMenuItem ruleItem in ruleToolStripMenuItem.DropDownItems)
            {
                ruleItem.Checked = false;
            }

            // Recheck the rule item that was clicked
            selectedRuleItem.Checked = true;

            LoadCards();
        }

        // Function for showing AboutBoxForm if user clicks on About menu item
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutBox.Text = "About Concentration Game";
            aboutBox.ShowDialog();
        }
    }
}