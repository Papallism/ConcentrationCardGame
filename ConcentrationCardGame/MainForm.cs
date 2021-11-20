using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;

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
        
        private List<Card> clickedCards = new List<Card>();

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

        // Function for creating and loading cards
        private void LoadCards()
        {
            // Clear list, counter and all controls from the flow panel
            numberOfMoves = 0;
            clickedCards.Clear();
            flowLayoutPanelCards.Controls.Clear();

            // Create cards and add them to flow panel
            for (int i = 0; i < GetSelectedSizeInt(selectedSizeName) * GetSelectedRuleInt(selectedRuleName); i++)
            {
                Card card = new Card();
                card.Width = GetButtonDimensions(selectedSizeName);
                card.Height = GetButtonDimensions(selectedSizeName);
                card.Click += Card_Click;

                flowLayoutPanelCards.Controls.Add(card);
            }

            SetImagesToAllButtons();
        }

        // Function for when a card is clicked
        private void Card_Click(object sender, EventArgs e)
        {
            var card = sender as Card;
            var cardsMatch = true;

            numberOfMoves++;
            clickedCards.Add(card);
            card.BackgroundImage = card.CardImage;

            // If user turns as many cards as the selected rule, check if the cards match
            if (clickedCards.Count == GetSelectedRuleInt(selectedRuleName))
            {
                // Compare images of turned cards
                Image firstCardImage = clickedCards.First().CardImage;
                for (int i = 1; i < GetSelectedRuleInt(selectedRuleName); i++)
                {
                    if (!clickedCards[i].CardImage.Equals(firstCardImage))
                    {
                        cardsMatch = false;
                    }
                }

                // If turned cards do not match, hide them
                if (!cardsMatch)
                {
                    foreach (Card turnedCard in clickedCards)
                    {
                        turnedCard.BackgroundImage = Properties.Resources.QuestionMark1024;
                    }
                }
                // Else if turned cards match, set their IsMatched property to true
                else
                {
                    foreach (Card turnedCard in clickedCards)
                    {
                        turnedCard.IsMatched = true;
                    }
                }

                // Clear the list of currently revealed cards
                clickedCards.Clear();
            }

            CheckIfGameFinished();
        }

        // Function to check if the game is over
        private void CheckIfGameFinished()
        {
            var allCardsMatched = true;

            // Check if all cards have been matched
            foreach (Card card in flowLayoutPanelCards.Controls)
            {
                if (!card.IsMatched)
                {
                    allCardsMatched = false;
                }
            }

            if (allCardsMatched)
            {
                MessageBox.Show($"Congratulation, you matched all the cards in {numberOfMoves} moves!",
                                "You won!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        // Function to randomly set the background image for all cards
        private void SetImagesToAllButtons()
        {
            var buttonImageList = new List<Image>();
            // Add as many unique images as the size selected to the list
            buttonImageList = images.GetRange(0, GetSelectedSizeInt(selectedSizeName));
            // Duplicate the list as many times as the rule selected
            for (int i = 1; i < GetSelectedRuleInt(selectedRuleName); i++)
            {
                buttonImageList.AddRange(buttonImageList);
            }

            // Card image randomization
            foreach (Card card in flowLayoutPanelCards.Controls)
            {
                int randomPosition = 0;
                do
                {
                    randomPosition = rand.Next(0, GetSelectedSizeInt(selectedSizeName) * GetSelectedRuleInt(selectedRuleName));
                    card.CardImage = buttonImageList[randomPosition];
                } while (buttonImageList[randomPosition] == null);
                buttonImageList[randomPosition] = null;
            }
        }

        // Function to get the image dimensions as an integer number, according to the selected size item
        private int GetButtonDimensions(string sizeName)
        {
            switch (sizeName)
            {
                case "Small":
                    return 150;

                case "Medium":
                    return 125;

                case "Large":
                    return 100;

                default:
                    return 0;
            }
        }

        // Function to get the number of pairs as an integer number, according to the selected size item
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

        // Function to get the number of card copies as an integer, according tot the selected rule item
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