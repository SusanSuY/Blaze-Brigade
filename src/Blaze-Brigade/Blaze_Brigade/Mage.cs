using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

using View;
using Model.WeaponModule;

namespace Model
{
    namespace UnitModule
    {
        /// <summary>
        /// The Mage model class, extends Unit.
        /// \n This Unit has strong magical capabilities, and is capable of powerful ranged magic attacks, but makes up with poor physical stats.
        /// </summary>
        public class Mage : Unit
        {

            private readonly int movability = 4;        // Archer movement is permanently set to 5
            private Weapon[] equipableWeapons;          // Array of all equipable weapons
            private int str;                            // unit strength
            private int intelligence;                   // unit intelliegence
            private int skill;                          // unit skill
            private int hp;                             // unit hp
            private Texture2D spriteImage;              // the char sprite
            private Vector2 pixelCoordinates;           // the pixel coordinate of the unit
            private Tuple<int, int> position;           // the tile location of the unit
            private Button[] buttons = new Button[9];   // the buttons associated with the unit
            private Texture2D charInfo, charAttackInfo, healthBar; // The character and attack info, and health bar textures
            private readonly int maxHp = 9;                        // The max HP of character

            /**
            * The constructor for Unit Mage. Stores all relevent data in model. 
            * @param spriteImage The character sprite texture.
            * @param unitButtons The Texture2D Array containing all the different textures for each button.
            * @param charInfo The character info popup texture.
            * @param charAttackInfo The character attack menu popup texture.
            * @param coordinates The unit's current vector coordinate on screen.
            * @param player The player of which the unit belongs to.
            */
            public Mage(Texture2D spriteImage, Button[] unitButtons, Texture2D charInfo,
                Texture2D charAttackInfo, Vector2 coordinates, Texture2D healthBar)
            {
                if (spriteImage != null)
                {
                    this.spriteImage = spriteImage;
                }
                if (unitButtons != null && unitButtons.Length == 9)
                {
                    buttons[0] = unitButtons[0];
                    buttons[1] = unitButtons[1];
                    buttons[2] = unitButtons[2];
                    buttons[3] = unitButtons[3];
                    buttons[4] = unitButtons[4];
                    buttons[5] = unitButtons[5];
                    buttons[6] = unitButtons[6];
                    buttons[7] = unitButtons[7];
                    buttons[8] = unitButtons[8];
                }
                if (charInfo != null)
                {
                    this.charInfo = charInfo;
                }
                if (charAttackInfo != null)
                {
                    this.charAttackInfo = charAttackInfo;
                }
                if (healthBar != null)
                {
                    this.healthBar = healthBar;
                }
                pixelCoordinates = coordinates;
                int positionX = (int)Math.Round(coordinates.X / 32);
                int positionY = (int)Math.Round(coordinates.Y / 32);
                position = new Tuple<int, int>(positionX, positionY);
                currentFrame = 1;
                if (unitButtons != null)
                {
                    setButtonCoordinates(pixelCoordinates);
                }
                setInitialStats();
            }

            /**
            Sets and returns whether or not unit is alive.
            */
            public bool Alive { get; set; }
            /**
            Sets and returns a unit's Speed. \n
                \b Exceptions: \n
                -Negative skill will not result in an error as speed is only used for checking double attack boolean, which is binary.
            */
            public int Speed { get; set; }
            /**
            Sets and returns a unit's Defense. \n
                \b Exceptions: \n
                -Negative defense will result in an attacker doing more damage than their attack.
            */
            public int Def { get; set; }
            /**
            Sets and returns a unit's Resistance. \n
                \b Exceptions: \n
                -Negative resistance will result in an attacker doing more damage than their intelligence. 
            */
            public int Res { get; set; }
            /**
            Sets and returns a unit's Level. Currently does not have any use.
            */
            public int Level { get; set; }

            /**
            Gets and sets the unit is currently equipping.
            */
            public Weapon equippedWeapon { get; set; }
            /**
            Gets and sets current frame the sprite is on. 
            */
            public int currentFrame { get; set; }

            /**
            Sets the initial unit stats before modifiers.
            */
            public void setInitialStats()
            {
                Alive = true;
                Level = 1;
                Hp = 9;
                Str = 3;
                Int = 1;
                Skill = 6;
                Speed = 8;
                Def = 6;
                Res = 6;
            }

            /**
            Sets and gets the new strength value.
            \n Gets the effective strength -> Unit strength + weapon strength \n
                \b Exceptions: \n
                -Negative strength will be treated as 0 in damage calculation, as damage dealt can not be negative.
            */
            public int Str
            {
                get
                {
                    return str + equippedWeapon.modStr;
                }
                set
                {
                    str = value;
                }
            }

            /**
            Sets and gets the new intelligence value.
            \n Gets the effective intelligence -> Unit intelligence + weapon intelligence \n
                \b Exceptions: \n
                -Negative strength will be treated as 0 in damage calculation, as damage dealt can not be negative.
            */
            public int Int
            {
                get
                {
                    return intelligence + equippedWeapon.modInt;
                }
                set
                {
                    intelligence = value;
                }
            }

            /**
            Sets and gets the new skill value.
            \n Gets the effective skill -> Unit skill + weapon skill \n
                \b Exceptions: \n
                -Negative skill will not result in an error, but will most likely result in a 0% hit and crit rate.
            */
            public int Skill
            {
                get
                {
                    return skill + equippedWeapon.modSkill;
                }
                set
                {
                    skill = value;
                }
            }

            /**
             Sets and get a unit's HP. Should HP fall under 0, Unit's Alive Boolean should change to false.
             */
            public int Hp
            {
                get
                {
                    return hp;
                }
                set
                {
                    if (value <= 0)
                    {
                        this.Alive = false;
                    }

                    hp = value;
                }
            }

            /**
            Returns the unit's movability range on grid (number of spaces the unit can move in one turn). \n
            \b Exceptions: \n
                -Negative movement will be treated as 0 in path finding algorithm.
            */
            public int getMovability()
            {
                return movability;
            }

            /**
            Returns all stats as an array, where the index in array corresponds to stats in this order: \n
                Level, Strength, Int, Skill, Speed, Def, Res.
            */
            public int[] getStats()
            {
                int[] allStats = new int[7];
                allStats[0] = Level;
                allStats[1] = Str;
                allStats[2] = Int;
                allStats[3] = Skill;
                allStats[4] = Speed;
                allStats[5] = Def;
                allStats[6] = Res;
                return allStats;
            }

            /**
            Returns the list weapons the unit can equip (TODO).
            */
            public Weapon[] getEquipableWeapons()
            {
                return equipableWeapons;
            }

            /**
            Returns the unit's class.
            */
            public UnitType getClass()
            {
                return UnitType.Mage;
            }

            /**
           Returns the sprite image of the unit.
           */
            public Texture2D getSpriteImage()
            {
                return spriteImage;
            }

            /**
            This method returns the texture associated with the bunttonType passed in, by going through a switch
            case and matching it.
                @param buttonType The buttontype that was clicked.
            */
            public Texture2D getButtonImage(ButtonType buttonType)
            {
                switch (buttonType)
                {
                    case ButtonType.Attack: // if attack clicked
                        return buttons[0].getImage();
                    case ButtonType.Move: // if moved is clicked
                        return buttons[1].getImage();
                    case ButtonType.Items: // if item is clicked
                        return buttons[2].getImage();
                    case ButtonType.Wait: // if wait is clicked
                        return buttons[3].getImage();
                    case ButtonType.AttackConfirm: // if attack confirm is clicked
                        return buttons[4].getImage();
                    case ButtonType.Inventory1: // if item1 clicked
                        return buttons[5].getImage();
                    case ButtonType.Inventory2: // if item2 is clicked
                        return buttons[6].getImage();
                    case ButtonType.Inventory3: // if item3 is clicked
                        return buttons[7].getImage();
                    case ButtonType.Inventory4: // if item4 is clicked
                        return buttons[8].getImage();
                    default:
                        return null;
                }
            }

            /**
            This method takes in the buttonType specified, and checks if that button is currently active by calling
            the getter in button.
                @param buttonType The buttontype that was clicked.
            */
            public bool isButtonActive(ButtonType buttonType)
            {
                switch (buttonType)
                {
                    case ButtonType.Attack:
                        return buttons[0].Active;
                    case ButtonType.Move:
                        return buttons[1].Active;
                    case ButtonType.Items:
                        return buttons[2].Active;
                    case ButtonType.Wait:
                        return buttons[3].Active;
                    case ButtonType.AttackConfirm:
                        return buttons[4].Active;
                    case ButtonType.Inventory1:
                        return buttons[5].Active;
                    case ButtonType.Inventory2:
                        return buttons[6].Active;
                    case ButtonType.Inventory3:
                        return buttons[7].Active;
                    case ButtonType.Inventory4:
                        return buttons[8].Active;
                    default:
                        return false;
                }
            }

            /**
            Returns the char info screen texture.
            */
            public Texture2D getCharInfo()
            {
                return charInfo;
            }

            /**
            Returns the char attack info screen texture.
            */
            public Texture2D getCharAttackInfo()
            {
                return charAttackInfo;
            }

            /**
            Gets and sets unit's position by tile. The set also updates pixelCoordinate's location by making 
            that vector equivalent to position*32 (since each tile is 32x32). \n
                \b Exceptions: \n
                -Dead units will still have a position, but won't impact the rest of the game.
            */
            public Tuple<int, int> Position
            {
                get
                {
                    return position;
                }
                set
                {
                    position = value;
                    pixelCoordinates = new Vector2(value.Item1 * 32, value.Item2 * 32);
                }
            }

            /**
            Returns the pixel coordinate of the unit.
            \n sets the pixel coordinate, and also sets Position (which is the tile location of that coordinate). \n
            \b Exceptions: \n
                -Dead units will still have a position, but won't impact the rest of the game.
            */
            public Vector2 PixelCoordinates
            {
                get
                {
                    return pixelCoordinates;
                }
                set
                {
                    pixelCoordinates = value;
                    int positionX = (int)Math.Round(PixelCoordinates.X / 32);
                    int positionY = (int)Math.Round(PixelCoordinates.Y / 32);
                    position = new Tuple<int, int>(positionX, positionY);
                }
            }

            /**
            Returns the dropdown menu buttons of the unit.
            */
            public Button[] getButtons()
            {
                return buttons;
            }

            /**
            This method takes in the buttonType enum, then returns the object associated with that enum.
            * @param buttonType The button to return (Move, Attack, Item, Wait, and attack confirm).
            */
            public Button getButtonType(ButtonType buttonType)
            {
                switch (buttonType)
                {
                    case ButtonType.Attack:
                        return buttons[0];
                    case ButtonType.Move:
                        return buttons[1];
                    case ButtonType.Items:
                        return buttons[2];
                    case ButtonType.Wait:
                        return buttons[3];
                    case ButtonType.AttackConfirm:
                        return buttons[4];
                    case ButtonType.Inventory1:
                        return buttons[5];
                    case ButtonType.Inventory2:
                        return buttons[6];
                    case ButtonType.Inventory3:
                        return buttons[7];
                    case ButtonType.Inventory4:
                        return buttons[8];
                    default:
                        return null;
                }
            }

            /**
            Sets the coordinates of menu buttons. One for loop will position the main Drop Down menu (potentailly
            containing attack, move, item and wait directly 32 pixels to the right of unit (so the tile to right of unit)
            , and for each active button, increment it downwards by 32 pixels (height of each button). The second 
            for loop is similiar and is for the inventory menu buttons, except it starts 160 pixels offsetted to
            right (to the right of the main drop down menu).
            * @param pixelCoordinates The pixel coordinate of the button.
            */
            public void setButtonCoordinates(Vector2 pixelCoordinates)
            {
                int offsetInactive = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (!buttons[i].Active)
                    {
                        offsetInactive -= 32;
                    }
                    buttons[i].setPixelCoordinates((int)(pixelCoordinates.X + 32), (int)(pixelCoordinates.Y + (i * 32) + offsetInactive));
                }
                buttons[4].setPixelCoordinates(330, 120);
                for (int i = 5; i < 9; i++)
                {
                    buttons[i].setPixelCoordinates((int)(pixelCoordinates.X + 160), (int)(pixelCoordinates.Y + (i * 32 - 96)));
                }
            }

            /**
            returns the current sprite frame in animation sequence. The rectangle starts at currentFrame * 32, where 
            32 is the sprite sheet offset between frames, and is 32 high and wide. \n
                \b Exceptions: \n
                -Assumes that each sprite frame is 32pixels wide.
            */
            public Rectangle getCurrentFrame() //return current frame the sprite is on
            {
                Rectangle screenBounds = new Rectangle(currentFrame * 32, 0, 32, 32);
                return screenBounds;
            }

            /**
             Returns the healthbar texture.
             */
            public Texture2D getHealthBar()
            {
                return healthBar;
            }

            /**
            Returns the character's max HP.
            */
            public int getMaxHp()
            {
                return maxHp;
            }
        }
    }
}