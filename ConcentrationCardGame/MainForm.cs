using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

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

        private Random rand = new Random();

        private AboutBoxForm aboutBox = new AboutBoxForm();
        
        private List<Image> images = new List<Image>()
        {
            Properties.Resources.image0, Properties.Resources.image1, Properties.Resources.image2, Properties.Resources.image3,
            Properties.Resources.image4, Properties.Resources.image5, Properties.Resources.image6, Properties.Resources.image7,
            Properties.Resources.image8, Properties.Resources.image9, Properties.Resources.image10, Properties.Resources.image11,
            Properties.Resources.image12, Properties.Resources.image13, Properties.Resources.image14, Properties.Resources.image15,
            Properties.Resources.image16,
        };

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
            numberOfMoves = 0;

            // Create buttons and add them to flow panel
            for (int i = 0; i < GetSelectedSizeInt(selectedSizeName) * GetSelectedRuleInt(selectedRuleName); i++)
            {
                Button button = new Button();
                button.Width = 100; //GetButtonDimensions(selectedSizeName);
                button.Height = 100; //GetButtonDimensions(selectedSizeName);
                button.BackgroundImageLayout = ImageLayout.Zoom;
                button.BackgroundImage = Properties.Resources.QuestionMark1024;
                button.Click += Button_Click;

                flowLayoutPanelCards.Controls.Add(button);
            }

            SetImagesToAllButtons();
        }

        // Function for clicking on a card
        private void Button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            // TODO: reveal image

            // Increment the number of total moves
            numberOfMoves++;
        }

        // Function to set button background image
        private void SetImagesToAllButtons()
        {
            List<Image> updatedImages = new List<Image>();
            // Add as many images as the size selected to the list
            updatedImages = images.GetRange(0, GetSelectedSizeInt(selectedSizeName));
            // Duplicate the list as many times as the rule selected
            for (int i = 1; i < GetSelectedRuleInt(selectedRuleName); i++)
            {
                updatedImages.AddRange(updatedImages);
            }

            // TODO: background image randomization
            foreach (Button button in flowLayoutPanelCards.Controls)
            {
                int randomPosition = 0;
                do
                {
                    randomPosition = rand.Next(0, GetSelectedSizeInt(selectedSizeName) * GetSelectedRuleInt(selectedRuleName));
                    button.BackgroundImage = updatedImages[randomPosition];
                } while (updatedImages[randomPosition] == null);
                updatedImages[randomPosition] = null;
            }
        }

        // Function to get the image dimensions according to the selected size item
        private int GetButtonDimensions(string sizeName)
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

        // Function to get the number of card copies according tot the selected rule item
        private int GetSelectedRuleInt(string ruleName)
        {
            switch (ruleName)
            {
                case "Match 2":
                    return 2;

                case "Match 3":
                    return 3;

                default:
                    return 0;
            }
        }

        // Function for New Game menu item
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadCards();
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