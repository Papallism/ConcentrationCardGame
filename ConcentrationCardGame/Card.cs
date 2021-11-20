using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConcentrationCardGame
{
    public class Card : Button
    {
        public Image CardImage { get; set; }
        public bool IsMatched { get; set; }

        public Card()
        {
            this.Width = 100;
            this.Height = 100;
            this.BackgroundImageLayout = ImageLayout.Zoom;
            this.BackgroundImage = Properties.Resources.QuestionMark1024;
            this.IsMatched = false;
        }
    }
}
