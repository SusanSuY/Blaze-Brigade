using Microsoft.Xna.Framework;
using Model.MapModule;
using Model.UnitModule;
using System;
using System.Collections.Generic;

namespace Model
{
    /// <summary>
    /// This class holds states in the scope of the entire gameplay.
    /// </summary>
    public static class GameState
    {
        public static readonly int SCREEN_HEIGHT = 640;
        public static readonly int SCREEN_WIDTH = 960;

        /**
        Sets and gets player 1 (blue team).
        */
        public static Player Player1 { get; set; }
        /**
        Sets and gets player 2 (red team).
        */
        public static Player Player2 { get; set; }
        /**
        Sets and gets a unit.
        */
        public static Unit selectedUnit { get; set; }
        /**
        Sets and gets the selected enemy unit.
        */
        public static Unit selectedEnemyUnit { get; set; }
        /**
        Sets and gets the unit to attack.
        */
        public static Unit unitToAttack { get; set; }
        /**
        Sets and gets the current player.
        */
        public static Player currentPlayer { get; set; }
        /**
        Sets and gets the enemy player.
        */
        public static Player enemyPlayer { get; set; }
        /**
        Sets and gets whether drop down menu should be open.
        */
        public static bool dropDownMenuOpen { get; set; }
        /**
        Sets and gets whether attackConfirm menu should be open.
        */
        public static bool attackConfirmOpen { get; set; }
        /**
        Sets and gets whether player is currently selecting unit to attack.
        */
        public static bool attackSelect { get; set; }
        /**
        Sets and gets whether inventory menu should be open.
        */
        public static bool inventoryOpen { get; set; }
        /**
        Sets and gets whether end turn button is active.
        **/
        public static bool endTurnButton { get; set; }
        /**
        Sets and gets if a unit has moved yet or not. beforeMOve is true before unit moves, false after it moves. Used to determine what tiles are highlighted.
        */
        public static bool beforeMove { get; set; }
        /**
        Sets and gets whether an animation sequence is currently on screen.
        */
        public static bool isAnimating { get; set; }
        /**
        Sets and gets whether game is over.
        */
        public static bool gameOver { get; set; }
        /**
        Sets and gets if game should exit.
        */
        public static bool exitGameClicked { get; set; }
        /**
        Sets and gets end turn button position.
        */
        public static Vector2 endTurnButtonLocation { get; set; }
        /**
        sets and gets whether it is currently transitioning turns.
        */
        public static bool transitionTurn { get; set; }
        /**
        Sets and gets the current TurnState of the selected unit.
        */
        public static TurnState TurnState { get; set; }
        /**
        Sets and gets whether damage dealt number pops up for current player.
        */
        public static bool currentPlayerDamagePopup { get; set; }
        /**
        Stores the damage dealt by current player in their most recent attack.
        */
        private static String currentPlayerDamageDealt;
        /**
        Sets and gets the current player's last attack as a string. For setting, an int is taken in, and parsed to String.
        If the damage dealt is 0, the String stored is changed to "Miss".
        */
        public static String CurrentPlayerDamageDealt
        {
            get
            {
                return currentPlayerDamageDealt;
            }
            set
            {
                int damage = Int32.Parse(value);
                if (damage == 0)
                {
                    currentPlayerDamageDealt = "Miss";
                }
                else
                {
                    currentPlayerDamageDealt = value;
                }
            }
        }

        /**
        Sets and gets the current unit controlled in a 2nd location, for printing damage popup AFTER selectedUnit has been updated, since damage Popup should appear for a few seconds after action has finished.
        */
        public static Unit lastAttackingUnit { get; set; }
        /**
        Sets and gets whether damage dealt number pops up for enemy player.
        */
        public static bool enemyPlayerDamagePopup { get; set; }
        /**
        Stores the damage dealt by enemy player in their most recent attack.
        */
        private static String enemyPlayerDamageDealt;
        /**
        Gets the enemy unit's last defending attack as a string. For setting, an int is taken in, and parsed to String.
        If the damage dealt is 0, the String stored is changed to "Miss".
        */
        public static String EnemyPlayerDamageDealt
        {
            get
            {
                return enemyPlayerDamageDealt;
            }
            set
            {
                int damage = Int32.Parse(value);
                if (damage == 0)
                {
                    enemyPlayerDamageDealt = "Miss";
                }
                else
                {
                    enemyPlayerDamageDealt = value;
                }
            }
        }
        /**
        Sets and gets the current unit controlled in a 2nd location, for printing damage popup AFTER unitToAttack has been updated, since damage Popup should appear for a few seconds after action has finished.
        */
        public static Unit lastDefendingUnit { get; set; }
        /**
        Sets and gets movable nodes that can be retrieved without calling path finding.
        */
        public static LinkedList<Node> moveableNodes { get; set; }
        /**
        Sets and gets the winning player.
        */
        public static int winningPlayer { get; set; }
    }
    /**
    Enumerated list for different possible Game States.
    */
    public enum GameMenuState
    {
        MainMenu,       // menu screen
        HowToPlay,      // Instruction Screen 1
        HowToPlay2,     // Instruction Screen 2
        HowToPlay3,     // Instructions Screen 3
        Playing        // Playing game Screen
    }
    /**
    Enumerated list for what the current turn state is (per unit).
    */
    public enum TurnState
    {
        Wait,
        AttackMenu,
        Attack,
        Move,
        Items
    }
}
