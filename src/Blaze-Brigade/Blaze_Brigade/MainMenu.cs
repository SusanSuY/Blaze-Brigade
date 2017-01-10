using System;
using System.Windows.Forms;

/// <summary>
/// The view in MVC. These classes deal with the view that the user sees in the game.
/// </summary>

namespace View
{
    /// <summary>
    /// The Menu Module containing all menu related classes.
    /// </summary>
    namespace MenuModule
    {
        /// <summary>
        /// The Main Menu class. This window is displayed upon starting game, and can link you to
        /// HowToPlay playing the Game.
        /// </summary>
        public partial class MainMenu : Form
        {
            /**
             * boolean that checks if start button is clicked
            */
            public bool start = false;

            /**
             * boolean that checks if instruct button is clicked
            */
            public bool instruct = false;

            /**
             * boolean that checks if quit button is clicked
            */
            public bool quit = false;

            /**
            * boolean that checks if load button is clicked
            */
            public bool load = false;

            /**
            Constructor for Main Menu window
            */
            public MainMenu()
            {
                InitializeComponent();
            }

            /**
            checks if user clicks start
            * @params sender the object to check
            * @params e The event to check for
            */
            private void Start_Click(object sender, EventArgs e)
            {
                start = true; // check if user starts game
            }

            /**
            check if user wants instructions and clicks on HowToPlay button
            * @params sender the object to check
            * @params e The event to check for
            */
            private void howToPlay_Click(object sender, EventArgs e)
            {
                instruct = true;
            }

            /**
            sets instruct to false when no longer on how-to-play gameState
            */
            public void setInstructFalse()
            {
                instruct = false;
            }

            /**
            returns current instruct boolean
            */
            public Boolean getInstruct()
            {
                return instruct;
            }

            /**
            checks if exit is clicked
            */
            private void Exit_Click(object sender, EventArgs e)
            {
                quit = true;    // check if user quits
            }
        }
    }
}
