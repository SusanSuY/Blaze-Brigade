using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Model;
using Model.WeaponModule;

namespace Model
{
    /// <summary>
    /// Buttons for the drop down menu buttons when selecting units.
    /// </summary>
    public class Button
    {
        private ButtonType buttonType;      // holds the button type
        private Vector2 pixelCoordinates;   // holds the coordinates of the menu button in question
        private Texture2D buttonImage;      // stores the texture for the button
        private Weapon weapons;

        /**
        Constructor for button. Button is by defaalt active, and has no item.
        * @param type What the button type is.
        * @param coordinates The pixel coordinate of the button.
        * @param image The texture for the button.
        */
        public Button(ButtonType type, Vector2 coordinates, Texture2D image)
        {
            buttonType = type;
            pixelCoordinates = coordinates;
            buttonImage = image;
            Active = true;
            hasItem = false;
        }
        /**
        sets and gets whether button is active.
        */
        public bool Active { get; set; }

        /**
        Sets and gets string name for item name.
        */
        public String item { get; set; }

        /**
        Sets and gets whether an item is currently bounded to button.
        */
        public bool hasItem { get; set; }

        /**
        Sets and gets weapon associated with the button.
        */
        public Weapon weapon
        {
            get
            {
                return weapons;
            }
            set
            {
                weapons = value;
                if (value != null)
                {
                    hasItem = true;
                }
            }
        }

        /**
        Returns the pixel coordinate of the button.
        */
        public Vector2 getPixelCoordinates()
        {
            return pixelCoordinates;
        }

        /**
        Returns the button type.
        */
        public ButtonType getButtonType()
        {
            return buttonType;
        }

        /**
        Returns the button image.
        */
        public Texture2D getImage()
        {
            return buttonImage;
        }

        /**
        Sets the pixelCoordinate of button.
        * @param x The x coordinate of the button.
        * @param y the y coordinate of the button.
        */
        public void setPixelCoordinates(int x, int y)
        {
            Vector2 coordinates = new Vector2(x, y);
            pixelCoordinates = coordinates;
        }
    }

    /**
    Enumerated list for the possible button types.
    */
    public enum ButtonType
    {
        Attack,
        AttackConfirm,
        Move,
        Items,
        Wait,
        Inventory1,
        Inventory2,
        Inventory3,
        Inventory4
    }
}

