using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using Model;
using Microsoft.Xna.Framework;
using System;
using View;
using Model.UnitModule;
using Model.WeaponModule;

namespace ModelTest
{
    namespace UnitModuleTest
    {
        [TestClass]
        public class ArcherTest
        {
            [TestMethod]
            public void Str_ShouldReturnExpected()
            {
                Archer unit = new Archer(null, null, null, null, Vector2.Zero, null);
                unit.equippedWeapon = new ShortBow();
                Assert.AreEqual(10, unit.Str);
            }

            [TestMethod]
            public void Str_NullWeapon_ShouldThrowNullReferenceException()
            {
                try
                {
                    Archer unit = new Archer(null, null, null, null, Vector2.Zero, null);
                    int str = unit.Str;
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex is NullReferenceException);
                }
            }

            [TestMethod]
            public void Int_ShouldReturnExpected()
            {
                Archer unit = new Archer(null, null, null, null, Vector2.Zero, null);
                unit.equippedWeapon = new ShortBow();
                Assert.AreEqual(1, unit.Int);
            }

            [TestMethod]
            public void Int_NullWeapon_ShouldThrowNullReferenceException()
            {
                try
                {
                    Archer unit = new Archer(null, null, null, null, Vector2.Zero, null);
                    int intel = unit.Int;
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex is NullReferenceException);
                }
            }

            [TestMethod]
            public void Skill_ShouldReturnExpected()
            {
                Archer unit = new Archer(null, null, null, null, Vector2.Zero, null);
                unit.equippedWeapon = new ShortBow();
                Assert.AreEqual(16, unit.Skill);
            }

            [TestMethod]
            public void Skill_NullWeapon_ShouldThrowNullReferenceException()
            {
                try
                {
                    Archer unit = new Archer(null, null, null, null, Vector2.Zero, null);
                    int skill = unit.Skill;
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex is NullReferenceException);
                }
            }

            [TestMethod]
            public void Hp_ShouldReturnExpected()
            {
                Archer unit = new Archer(null, null, null, null, Vector2.Zero, null);

                // test that Alive is set to false
                unit.Alive = true;
                unit.Hp = -5;
                Assert.IsFalse(unit.Alive);
                Assert.AreEqual(-5, unit.Hp);

                // test that Alive is not set to false
                unit.Alive = true;
                unit.Hp = 5;
                Assert.IsTrue(unit.Alive);
                Assert.AreEqual(5, unit.Hp);
            }

            [TestMethod]
            public void getStats_ShouldReturnExpected()
            {
                Archer unit = new Archer(null, null, null, null, Vector2.Zero, null);
                unit.equippedWeapon = new ShortBow();
                int[] stats = unit.getStats();
                Assert.AreEqual(1, stats[0]);
                Assert.AreEqual(10, stats[1]);
                Assert.AreEqual(1, stats[2]);
                Assert.AreEqual(16, stats[3]);
                Assert.AreEqual(8, stats[4]);
                Assert.AreEqual(6, stats[5]);
                Assert.AreEqual(6, stats[6]);
            }

            [TestMethod]
            public void Position_ShouldBehaveAsExpected()
            {
                Archer unit = new Archer(null, null, null, null, Vector2.Zero, null);
                unit.Position = new Tuple<int, int>(10, 10);
                Assert.AreEqual(new Vector2(320, 320), unit.PixelCoordinates);
                Assert.AreEqual(new Tuple<int, int>(10, 10), unit.Position);
            }

            [TestMethod]
            public void PixelCoordinates_ShouldBehaveAsExpected()
            {
                Archer unit = new Archer(null, null, null, null, Vector2.Zero, null);
                unit.PixelCoordinates = new Vector2(320, 320);
                Assert.AreEqual(new Tuple<int, int>(10, 10), unit.Position);
                Assert.AreEqual(new Vector2(320, 320), unit.PixelCoordinates);
            }

            [TestMethod]
            public void getCurrentFrame_ShouldReturnExpected()
            {
                Archer unit = new Archer(null, null, null, null, Vector2.Zero, null);
                Rectangle screenBounds = new Rectangle(32, 0, 32, 32);
                Assert.AreEqual(screenBounds, unit.getCurrentFrame());
            }

            [TestMethod]
            public void isButtonActive_ShouldReturnExpected()
            {
                Button[] buttons = new Button[9];
                Button attackButton = new Button(ButtonType.Attack, Vector2.Zero, null);
                Button moveButton = new Button(ButtonType.Move, Vector2.Zero, null);
                Button itemsButton = new Button(ButtonType.Items, Vector2.Zero, null);
                Button waitButton = new Button(ButtonType.Wait, Vector2.Zero, null);
                Button attackConfirmButton = new Button(ButtonType.AttackConfirm, Vector2.Zero, null);
                Button inventory1Button = new Button(ButtonType.Inventory1, Vector2.Zero, null);
                Button inventory2Button = new Button(ButtonType.Inventory2, Vector2.Zero, null);
                Button inventory3Button = new Button(ButtonType.Inventory3, Vector2.Zero, null);
                Button inventory4Button = new Button(ButtonType.Inventory4, Vector2.Zero, null);
                attackButton.Active = false;
                moveButton.Active = true;
                itemsButton.Active = false;
                waitButton.Active = true;
                attackConfirmButton.Active = false;
                inventory1Button.Active = false;
                inventory2Button.Active = true;
                inventory3Button.Active = false;
                inventory4Button.Active = false;
                buttons[0] = attackButton;
                buttons[1] = moveButton;
                buttons[2] = itemsButton;
                buttons[3] = waitButton;
                buttons[4] = attackConfirmButton;
                buttons[5] = inventory1Button;
                buttons[6] = inventory2Button;
                buttons[7] = inventory3Button;
                buttons[8] = inventory4Button;
                Warrior unit = new Warrior(null, buttons, null, null, Vector2.Zero, null);

                Assert.IsFalse(unit.isButtonActive(ButtonType.Attack));
                Assert.IsFalse(unit.isButtonActive(ButtonType.AttackConfirm));
                Assert.IsTrue(unit.isButtonActive(ButtonType.Move));
                Assert.IsFalse(unit.isButtonActive(ButtonType.Items));
                Assert.IsTrue(unit.isButtonActive(ButtonType.Wait));
                Assert.IsFalse(unit.isButtonActive(ButtonType.Inventory1));
                Assert.IsTrue(unit.isButtonActive(ButtonType.Inventory2));
                Assert.IsFalse(unit.isButtonActive(ButtonType.Inventory3));
                Assert.IsFalse(unit.isButtonActive(ButtonType.Inventory4));
            }

            [TestMethod]
            public void isButtonActive_NullButtons_ShouldThrowNullReferenceException()
            {
                try
                {
                    Warrior warrior = new Warrior(null, null, null, null, Vector2.Zero, null);
                    warrior.isButtonActive(ButtonType.Attack);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex is NullReferenceException);
                }
            }

            [TestMethod]
            public void getButtonType_ShouldReturnExpected()
            {
                Button[] buttons = new Button[9];
                Button attackButton = new Button(ButtonType.Attack, Vector2.Zero, null);
                Button moveButton = new Button(ButtonType.Move, Vector2.Zero, null);
                Button itemsButton = new Button(ButtonType.Items, Vector2.Zero, null);
                Button waitButton = new Button(ButtonType.Wait, Vector2.Zero, null);
                Button attackConfirmButton = new Button(ButtonType.AttackConfirm, Vector2.Zero, null);
                Button inventory1Button = new Button(ButtonType.Inventory1, Vector2.Zero, null);
                Button inventory2Button = new Button(ButtonType.Inventory2, Vector2.Zero, null);
                Button inventory3Button = new Button(ButtonType.Inventory3, Vector2.Zero, null);
                Button inventory4Button = new Button(ButtonType.Inventory4, Vector2.Zero, null);
                attackButton.Active = false;
                moveButton.Active = true;
                itemsButton.Active = false;
                waitButton.Active = true;
                attackConfirmButton.Active = false;
                inventory1Button.Active = false;
                inventory2Button.Active = true;
                inventory3Button.Active = false;
                inventory4Button.Active = false;
                buttons[0] = attackButton;
                buttons[1] = moveButton;
                buttons[2] = itemsButton;
                buttons[3] = waitButton;
                buttons[4] = attackConfirmButton;
                buttons[5] = inventory1Button;
                buttons[6] = inventory2Button;
                buttons[7] = inventory3Button;
                buttons[8] = inventory4Button;
                Warrior unit = new Warrior(null, buttons, null, null, Vector2.Zero, null);

                Assert.ReferenceEquals(attackButton, unit.getButtonType(ButtonType.Attack));
                Assert.ReferenceEquals(attackConfirmButton, unit.getButtonType(ButtonType.AttackConfirm));
                Assert.ReferenceEquals(moveButton, unit.getButtonType(ButtonType.Move));
                Assert.ReferenceEquals(itemsButton, unit.getButtonType(ButtonType.Items));
                Assert.ReferenceEquals(waitButton, unit.getButtonType(ButtonType.Wait));
                Assert.ReferenceEquals(inventory1Button, unit.getButtonType(ButtonType.Inventory1));
                Assert.ReferenceEquals(inventory2Button, unit.getButtonType(ButtonType.Inventory2));
                Assert.ReferenceEquals(inventory3Button, unit.getButtonType(ButtonType.Inventory3));
                Assert.ReferenceEquals(inventory4Button, unit.getButtonType(ButtonType.Inventory4));
            }

            [TestMethod]
            public void getButtonType_NullButtons_ShouldThrowNullReferenceException()
            {
                try
                {
                    Warrior warrior = new Warrior(null, null, null, null, Vector2.Zero, null);
                    warrior.getButtonType(ButtonType.Attack);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex is NullReferenceException);
                }
            }

            [TestMethod]
            public void setButtonCoordinates_ShouldBehaveAsExpected()
            {
                Button[] buttons = new Button[9];
                Button attackButton = new Button(ButtonType.Attack, Vector2.Zero, null);
                Button moveButton = new Button(ButtonType.Move, Vector2.Zero, null);
                Button itemsButton = new Button(ButtonType.Items, Vector2.Zero, null);
                Button waitButton = new Button(ButtonType.Wait, Vector2.Zero, null);
                Button attackConfirmButton = new Button(ButtonType.AttackConfirm, Vector2.Zero, null);
                Button inventory1Button = new Button(ButtonType.Inventory1, Vector2.Zero, null);
                Button inventory2Button = new Button(ButtonType.Inventory2, Vector2.Zero, null);
                Button inventory3Button = new Button(ButtonType.Inventory3, Vector2.Zero, null);
                Button inventory4Button = new Button(ButtonType.Inventory4, Vector2.Zero, null);
                attackButton.Active = false;
                moveButton.Active = true;
                itemsButton.Active = true;
                waitButton.Active = true;
                attackConfirmButton.Active = false;
                inventory1Button.Active = true;
                inventory2Button.Active = true;
                inventory3Button.Active = false;
                inventory4Button.Active = false;
                buttons[0] = attackButton;
                buttons[1] = moveButton;
                buttons[2] = itemsButton;
                buttons[3] = waitButton;
                buttons[4] = attackConfirmButton;
                buttons[5] = inventory1Button;
                buttons[6] = inventory2Button;
                buttons[7] = inventory3Button;
                buttons[8] = inventory4Button;
                Warrior unit = new Warrior(null, buttons, null, null, Vector2.Zero, null);

                unit.setButtonCoordinates(Vector2.Zero);
                Assert.AreEqual(new Vector2(32, 0), moveButton.getPixelCoordinates());
                Assert.AreEqual(new Vector2(32, 32), itemsButton.getPixelCoordinates());
                Assert.AreEqual(new Vector2(32, 64), waitButton.getPixelCoordinates());
                Assert.AreEqual(new Vector2(160, 64), inventory1Button.getPixelCoordinates());
                Assert.AreEqual(new Vector2(160, 96), inventory2Button.getPixelCoordinates());
            }

            [TestMethod]
            public void setButtonCoordinates_NullButtons_ShouldThrowNullReferenceException()
            {
                try
                {
                    Warrior warrior = new Warrior(null, null, null, null, Vector2.Zero, null);
                    warrior.setButtonCoordinates(Vector2.Zero);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex is NullReferenceException);
                }
            }
        }
    }
}
