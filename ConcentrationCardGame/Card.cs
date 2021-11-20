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
        public bool IsMatched { get; set; }
        public Image CardImage { get; set; }

        public Card()
        {
            this.IsMatched = false;
            this.BackgroundImageLayout = ImageLayout.Zoom;
            this.BackgroundImage = Properties.Resources.QuestionMark1024;
        }
    }
}
