using System;
using System.Windows.Forms;


namespace View
{
    namespace MenuModule
    {
        /// <summary>
        /// How to Play Menu 3 is opened with next is clicked on HowToPlay2 Menu 
        /// </summary>
        public partial class HowToPlay3 : Form
        {
            /**
             * boolean that checks if quit button is clicked
            */
            public bool quit = false;

            /**
            Constructor for HowToPlay3 window
            */
            public HowToPlay3()
            {
                InitializeComponent();
            }
            /**
            checks if user clicks quit
            * @params sender the object to check
            * @params e The event to check for
            */
            private void Exit_Click(object sender, EventArgs e)
            {
                quit = true;
            }
            /**
            checks if Game State is no longer inside How To Play
            */
            public void setQuitFalse()
            {
                quit = false;
            }
            /**
            returns if quit button is currently clicked
            */
            public Boolean getQuit()
            {
                return quit;
            }
        }
    }
}
