using System;
using System.Windows.Forms;

namespace ConcentrationCardGame
{
    public partial class SplashScreenForm : Form
    {
        public SplashScreenForm()
        {
            InitializeComponent();
        }

        private void timerSplashScreen_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}