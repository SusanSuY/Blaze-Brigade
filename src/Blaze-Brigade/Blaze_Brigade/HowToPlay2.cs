using System;
using System.Windows.Forms;


namespace View
{
    namespace MenuModule
    {
        /// <summary>
        /// How to Play Menu 2 is opened with next is clicked on HowToPlay Menu
        /// </summary>
        public partial class HowToPlay2 : Form
        {
            /**
             * boolean that checks if quit button is clicked
            */
            public bool quit = false;

            /**
             * boolean that checks if next button is clicked
            */
            public bool next = false;

            /**
            Constructor for HowToPlay2 window
            */
            public HowToPlay2()
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
            /**
            checks if user clicks quit
            * @params sender the object to check
            * @params e The event to check for
            */
            private void next_Click(object sender, EventArgs e)
            {
                next = true;    // check if user clicks next
            }
            /**
            checks if Game State is no longer inside How To Play
            */
            public void setNextFalse()
            {
                next = false;
            }
            /**
            returns if quit button is currently clicked
            */
            public Boolean getNext()
            {
                return next;
            }
        }
    }
}
