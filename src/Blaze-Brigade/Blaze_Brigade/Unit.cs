using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using View;
using Model.WeaponModule;

/// <summary>
/// The model in MVC. These classes contain the structure of the game, and will be controlled by Controller, and displayed in View.
/// </summary>

namespace Model
{
    /// <summary>
    /// The module containing all unit related classes and interface.
    /// </summary>
    namespace UnitModule
    {
        /// <summary>
        /// Unit Interface for Warrior, Mage, and Archer. 
        /// </summary>

        /**
        * This is the interface for the 3 playable unit classes, where the only
        differences in the units will be their statistics and graphical assets
        */
        public interface Unit
        {
            #region Stats
            /**
            Sets and returns whether or not unit is alive
            */
            bool Alive { get; set; }

            /**
            Sets and returns a unit's HP. Should HP fall under 0, Unit's Alive Boolean should change to false
            */
            int Hp { get; set; }

            /**
            Sets and returns a unit's Strength \n
                \b Exceptions: \n
                -Negative strength will be treated as 0 in damage calculation, as damage dealt can not be negative
            */
            int Str { get; set; }

            /**
            Sets and returns a unit's Intelliegence \n
                \b Exceptions: \n
                -Negative strength will be treated as 0 in damage calculation, as damage dealt can not be negative
            */
            int Int { get; set; }

            /**
            Sets and returns a unit's Skill \n
                \b Exceptions: \n
                -Negative skill will not result in an error, but will most likely result in a 0% hit and crit rate
            */
            int Skill { get; set; }

            /**
            Sets and returns a unit's Speed \n
                \b Exceptions: \n
                -Negative skill will not result in an error as speed is only used for checking double attack boolean, which is binary
            */
            int Speed { get; set; }

            /**
            Sets and returns a unit's Defense \n
                \b Exceptions: \n
                -Negative defense will result in an attacker doing more damage than their attack
            */
            int Def { get; set; }

            /**
            Sets and returns a unit's Resistance \n
                \b Exceptions: \n
                -Negative resistance will result in an attacker doing more damage than their intelligence 
            */
            int Res { get; set; }

            /**
            Sets and returns a unit's Level. Currently does not have any use
            */
            int Level { get; set; }

            /**
            Returns the unit's movability range on grid (number of spaces the unit can move in one turn) \n
            \b Exceptions: \n
                -Negative movement will be treated as 0 in path finding algorithm
            */
            int getMovability();

            /**
            returns all stats as an array
            */
            int[] getStats();

            /**
            sets initial unit stats upon creation
            */
            void setInitialStats();
            #endregion

            /**
            Returns weapon the unit is currently equipping.
            */
            Weapon equippedWeapon { get; set; }

            // TODO void setEquipableWeapons(Weapon add);  // need to update the weapon array, put new weapon into it

            /**
            Indicates whether a button has already been previously selected or not.
            */
            bool isButtonActive(ButtonType buttonType);

            /**
            Sets the coordinates of menu buttons. One for loop will position the main Drop Down menu (potentailly
            containing attack, move, item and wait directly 32 pixels to the right of unit (so the tile to right of unit)
            , and for each active button, increment it downwards by 32 pixels (height of each button). The second 
            for loop is similiar and is for the inventory menu buttons, except it starts 160 pixels offsetted to
            right (to the right of the main drop down menu).
            * @param pixelCoordinates The pixel coordinates of the button.
            */
            void setButtonCoordinates(Vector2 pixelCoordinates);

            /**
            Sets and gets the current frame of the animation sequence.
            */
            int currentFrame { get; set; }

            /**
            Returns the sprite image of the unit.
            */
            Texture2D getSpriteImage();

            /**
            Returns the button texture at index i.
            */
            Texture2D getButtonImage(ButtonType buttonType);

            /**
            Returns the char info screen texture.
            */
            Texture2D getCharInfo();

            /**
            Returns the char attack info screen texture.
            */
            Texture2D getCharAttackInfo();

            /**
           Gets and sets unit's position by tile. The set also updates pixelCoordinate's location by making 
           that vector equivalent to position*32 (since each tile is 32x32). \n
               \b Exceptions: \n
               -Dead units will still have a position, but won't impact the rest of the game
           */
            Tuple<int, int> Position { get; set; }

            /**
            Returns the pixel coordinate of the unit,
            sets the pixel coordinate, and also sets Position (which is the tile location of that coordinate). \n
            \b Exceptions: \n
                -Dead units will still have a position, but won't impact the rest of the game.
            */
            Vector2 PixelCoordinates { get; set; }

            /**
            Returns the dropdown menu buttons of the unit.
            */
            Button[] getButtons();

            /**
             Method takes in the buttonType enum, then returns the object associated with that enum.
             * @param buttonType The button to return (Move, Attack, Item, Wait, and attack confirm).
             */
            Button getButtonType(ButtonType buttonType);

            /**
            Returns the current sprite frame in animation sequence. The rectangle starts at currentFrame * 32, where 
            32 is the sprite sheet offset between frames, and is 32 high and wide. \n
                \b Exceptions: \n
                -Assumes that each sprite frame is 32pixels wide.
            */
            Rectangle getCurrentFrame();

            /**
            TODO - Not yet used \n
            Returns array of equipable weapons. \n
                \b Exceptions: \n
                -If this array is empty, unit cannot equip any weapons.
            */
            Weapon[] getEquipableWeapons();

            /**
            Returns unit's class (warrior, mage, archer).
            */
            UnitType getClass();

            /**
            Returns the healthBar Texture.
            */
            Texture2D getHealthBar();

            /**
            Returns the unit's max HP.
            */
            int getMaxHp();

        }
        /**
        Defines the possible classes of a unit
        */
        public enum UnitType { Warrior, Archer, Mage };
        public enum Direction { Down, Left, Right, Up };
    }
}
