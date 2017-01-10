using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Model;
using View;
using Model.WeaponModule;
using Model.UnitModule;

namespace ViewTest
{
    [TestClass]
    public class ButtonTest
    {
        [TestMethod]
        public void weapon_ShouldSetAsExpected()
        {
            // setup of button and sword (weapon)
            Button button = new Button(ButtonType.Inventory1, new Vector2(0, 0), null);
            Weapon sword = new BronzeSword();

            Assert.IsFalse(button.hasItem); // check button initially has no item attached to it

            button.weapon = sword;          // attach sword to button

            Assert.AreEqual(sword, button.weapon);  // check button returns expected weapon
            Assert.IsTrue(button.hasItem);  // check button has item attached to it
        }

        [TestMethod]
        public void weapon_WeaponIsNull_HasItemShouldBeFalse()
        {
            // setup of button and sword (weapon)
            Button button = new Button(ButtonType.Inventory1, new Vector2(0, 0), null);

            Assert.IsFalse(button.hasItem); // check button initially has no item attached to it

            button.weapon = null;           // set button weapon to null

            Assert.AreEqual(null, button.weapon);  // check button returns null weapon (as expected)
            Assert.IsFalse(button.hasItem);  // check button does not have item attached to it
        }

        public void setPixelCoordinates_ShouldSetAsExpected()
        {
            Button button = new Button(ButtonType.Attack, new Vector2(0, 0), null);
            Assert.AreEqual(new Vector2(0, 0), button.getPixelCoordinates());

            button.setPixelCoordinates(500, 500);

            Assert.AreEqual(new Vector2(500, 500), button.getPixelCoordinates());
        }

        public void setPixelCoordinates_NegativeNumbers_ShouldSetAsExpected()
        {
            Button button = new Button(ButtonType.Attack, new Vector2(0, 0), null);
            Assert.AreEqual(new Vector2(0, 0), button.getPixelCoordinates());

            button.setPixelCoordinates(-500, -500);

            Assert.AreEqual(new Vector2(-500, -500), button.getPixelCoordinates());
        }

        public void ButtonType_EnumsShouldBeNonNull()
        {
            Assert.IsNotNull(ButtonType.Attack);
            Assert.IsNotNull(ButtonType.AttackConfirm);
            Assert.IsNotNull(ButtonType.Move);
            Assert.IsNotNull(ButtonType.Items);
            Assert.IsNotNull(ButtonType.Wait);
            Assert.IsNotNull(ButtonType.Inventory1);
            Assert.IsNotNull(ButtonType.Inventory2);
            Assert.IsNotNull(ButtonType.Inventory3);
            Assert.IsNotNull(ButtonType.Inventory4);
        }
    }
}
