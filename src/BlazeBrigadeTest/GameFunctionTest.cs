using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using Model;
using Microsoft.Xna.Framework;
using System;
using Controller;
using System.Collections.Generic;
using View;
using Model.UnitModule;
using Model.MapModule;
using Model.WeaponModule;

namespace ControllerTest
{
    [TestClass]
    public class GameFunctionTest
    {
        [TestMethod]
        public void isEnemyUnitInRange_WhenEnemyInMeleeRange_ShouldReturnTrue()
        {
            Graph graph = new Graph(50, 32);  // create 50 by 32 node graph
            Player player = new Player();
            Mock<Unit> mockUnit = new Mock<Unit>();         // create mock object for unit
            Mock<Unit> mockEnemyUnit = new Mock<Unit>();    // create mock object for enemyUnit
            mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 15)); // mock position of unit to return (18, 15)
            mockUnit.Setup(Unit => Unit.equippedWeapon).Returns(new BronzeSword()); // mock equippedWeapon to return Bronze Sword (need this for melee weapon range)

            // check enemy unit to left of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(17, 15)); // mock position of enemy unit to return (17, 15)
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy to right of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(19, 15)); // mock position of enemy unit to return (19, 15)
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy above unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 14)); // mock position of enemy unit to return (17, 14)
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy below unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 16)); // mock position of enemy unit to return (17, 16)
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));
        }

        [TestMethod]
        public void isEnemyUnitInRange_WhenEnemyNotInMeleeRange_ShouldReturnFalse()
        {
            Graph graph = new Graph(50, 32);  // create 50 by 32 node graph
            Player player = new Player();
            Mock<Unit> mockUnit = new Mock<Unit>();         // create mock object for unit
            Mock<Unit> mockEnemyUnit = new Mock<Unit>();    // create mock object for enemyUnit
            mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 15)); // mock position of unit to return (18, 15)
            mockUnit.Setup(Unit => Unit.equippedWeapon).Returns(new BronzeSword()); // mock equippedWeapon to return Bronze Sword (need this for melee weapon range)

            // enemy is diagonal to warrior
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(19, 14)); // mock position of enemy unit to return (19, 14)
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // enemy is two spaces away from warrior
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(20, 15)); // mock position of enemy unit to return (20, 15)
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // enemy is far away from warrior
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(40, 15)); // mock position of enemy unit to return (40, 15)
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));
        }

        [TestMethod]
        public void isEnemyUnitInRange_WhenEnemyInMagicRange_ShouldReturnTrue()
        {
            Graph graph = new Graph(50, 32);  // create 50 by 32 node graph
            Player player = new Player();
            Mock<Unit> mockUnit = new Mock<Unit>();         // create mock object for unit
            Mock<Unit> mockEnemyUnit = new Mock<Unit>();    // create mock object for enemyUnit
            mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 15)); // mock position of unit to return (18, 15)
            mockUnit.Setup(Unit => Unit.equippedWeapon).Returns(new Fireball()); // mock equippedWeapon to return Fireball (need this for magic weapon range)

            // check enemy unit to left of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(17, 15)); // mock position of enemy unit to return (17, 15)
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy to right of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(19, 15)); // mock position of enemy unit to return (19, 15)
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy above unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 14)); // mock position of enemy unit to return (18, 14)
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy below unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 16)); // mock position of enemy unit to return (18, 16)
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy unit to two spaces left of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(16, 15)); // mock position of enemy unit to return (16, 15)
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy to two spaces right of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(20, 15)); // mock position of enemy unit to return (20, 15)
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy two spaces above unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 13)); // mock position of enemy unit to return (18, 13)
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy two spaces below unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 17)); // mock position of enemy unit to return (18, 17)
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy unit to top left of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(17, 14));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy to bottom left of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(17, 16));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy to top right of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(19, 14));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy to bottom right of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(19, 16));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));
        }

        [TestMethod]
        public void isEnemyUnitInRange_WhenEnemyNotInMagicRange_ShouldReturnFalse()
        {
            Graph graph = new Graph(50, 32);
            Player player = new Player();
            Mock<Unit> mockUnit = new Mock<Unit>();
            Mock<Unit> mockEnemyUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 15));
            mockUnit.Setup(Unit => Unit.equippedWeapon).Returns(new Fireball());

            // enemy is 3 spaces away from unit (diagonally)
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(19, 13));
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // enemy is 3 spaces away from unit (linearly)
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(21, 15));
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // enemy is far away from unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(40, 15));
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));
        }

        [TestMethod]
        public void isEnemyUnitInRange_WhenEnemyInShortBowRange_ShouldReturnTrue()
        {
            Graph graph = new Graph(50, 32);
            Player player = new Player();
            Mock<Unit> mockUnit = new Mock<Unit>();
            Mock<Unit> mockEnemyUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 15));
            mockUnit.Setup(Unit => Unit.equippedWeapon).Returns(new ShortBow());

            // check enemy unit to two spaces left of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(16, 15));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy to two spaces right of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(20, 15));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy two spaces above unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 13));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy two spaces below unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 17));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy unit to top left of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(17, 14));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy to bottom left of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(17, 16));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy to top right of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(19, 14));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy to bottom right of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(19, 16));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));
        }

        [TestMethod]
        public void isEnemyUnitInRange_WhenEnemyNotInShortBowRange_ShouldReturnFalse()
        {
            Graph graph = new Graph(50, 32);
            Player player = new Player();
            Mock<Unit> mockUnit = new Mock<Unit>();
            Mock<Unit> mockEnemyUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 15));
            mockUnit.Setup(Unit => Unit.equippedWeapon).Returns(new ShortBow());

            // check enemy unit to left of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(17, 15)); // mock position of enemy unit to return (17, 15)
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy to right of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(19, 15)); // mock position of enemy unit to return (19, 15)
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy above unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 14)); // mock position of enemy unit to return (17, 14)
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy below unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 16)); // mock position of enemy unit to return (17, 16)
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // enemy is 3 spaces away from unit (diagonally)
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(19, 13));
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // enemy is 3 spaces away from unit (linearly)
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(21, 15));
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // enemy is far away from unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(40, 15));
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));
        }

        [TestMethod]
        public void isEnemyUnitInRange_WhenEnemyInLongBowRange_ShouldReturnTrue()
        {
            Graph graph = new Graph(50, 32);
            Player player = new Player();
            Mock<Unit> mockUnit = new Mock<Unit>();
            Mock<Unit> mockEnemyUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 15));
            mockUnit.Setup(Unit => Unit.equippedWeapon).Returns(new LongBow());

            // check enemy unit to two spaces left of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(16, 15));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy to two spaces right of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(20, 15));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy two spaces above unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 13));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy two spaces below unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 17));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy unit to top left of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(17, 14));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy to bottom left of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(17, 16));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy to top right of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(19, 14));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy to bottom right of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(19, 16));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy 3 spaces away (linear)
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(21, 15));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(15, 15));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 18));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 12));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy 3 spaces away (diagonal)
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(19, 17));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(19, 13));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(20, 14));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(20, 16));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(17, 17));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(17, 13));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(16, 16));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(16, 14));
            Assert.IsTrue(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));
        }

        [TestMethod]
        public void isEnemyUnitInRange_WhenEnemyNotInLowBowRange_ShouldReturnFalse()
        {
            Graph graph = new Graph(50, 32);
            Player player = new Player();
            Mock<Unit> mockUnit = new Mock<Unit>();
            Mock<Unit> mockEnemyUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 15));
            mockUnit.Setup(Unit => Unit.equippedWeapon).Returns(new LongBow());

            // check enemy unit to left of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(17, 15)); // mock position of enemy unit to return (17, 15)
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy to right of unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(19, 15)); // mock position of enemy unit to return (19, 15)
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy above unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 14)); // mock position of enemy unit to return (17, 14)
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // check enemy below unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(18, 16)); // mock position of enemy unit to return (17, 16)
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // enemy is 4 spaces away from unit (diagonally)
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(19, 12));
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // enemy is 4 spaces away from unit (linearly)
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(22, 15));
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));

            // enemy is far away from unit
            mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(40, 15));
            Assert.IsFalse(GameFunction.isEnemyUnitInRange(graph, mockUnit.Object, mockEnemyUnit.Object));
        }

        [TestMethod]
        // test if startTurn performed all its functions
        public void startTurn_GameNotOver()
        {
            Player player1 = new Player();                  // create player 1 to pass into startTurn()
            Player player2 = new Player();                  // create player 2
            Mock<Camera> mockCamera = new Mock<Camera>();   // create mock object of camera
            Mock<Unit> mockUnit = new Mock<Unit>();         // create mock object for unit
            Button[] buttons = new Button[9];               // create buttons for mockUnit
            buttons[0] = new Button(ButtonType.Attack, new Vector2(0, 0), null);        // sets buttons
            buttons[1] = new Button(ButtonType.AttackConfirm, new Vector2(0, 0), null);
            buttons[2] = new Button(ButtonType.Inventory1, new Vector2(0, 0), null);
            buttons[3] = new Button(ButtonType.Inventory2, new Vector2(0, 0), null);
            buttons[4] = new Button(ButtonType.Inventory3, new Vector2(0, 0), null);
            buttons[5] = new Button(ButtonType.Inventory4, new Vector2(0, 0), null);
            buttons[6] = new Button(ButtonType.Items, new Vector2(0, 0), null);
            buttons[7] = new Button(ButtonType.Move, new Vector2(0, 0), null);
            buttons[8] = new Button(ButtonType.Wait, new Vector2(0, 0), null);
            foreach (Button button in buttons)
            {
                button.Active = false;  // set all button.Active to false to ensure assertions are correct
            }
            mockUnit.Setup(Unit => Unit.getButtons()).Returns(buttons); // mock mockUnit.getButtons() to return buttons
            GameState.Player1 = player1;    // set players in GameState
            GameState.Player2 = player2;
            GameState.currentPlayer = player1;
            player1.addUnit(mockUnit.Object);   // add mockUnit to players (so isGameOver() returns false)
            player2.addUnit(mockUnit.Object);

            // call function to test
            GameFunction.startTurn(player1, mockCamera.Object);

            // verify buttons are set to Active
            mockUnit.Verify(Unit => Unit.getButtons(), Times.Once());
            Assert.IsTrue(buttons[0].Active);
            Assert.IsTrue(buttons[1].Active);
            Assert.IsTrue(buttons[2].Active);
            Assert.IsTrue(buttons[3].Active);
            Assert.IsTrue(buttons[4].Active);
            Assert.IsTrue(buttons[5].Active);
            Assert.IsTrue(buttons[6].Active);
            Assert.IsTrue(buttons[7].Active);
            Assert.IsTrue(buttons[8].Active);

            // check that the function sets all states in GameState correctly
            Assert.IsNull(GameState.selectedUnit);
            Assert.IsNull(GameState.unitToAttack);
            Assert.IsFalse(GameState.dropDownMenuOpen);
            Assert.IsFalse(GameState.attackConfirmOpen);
            Assert.IsTrue(GameState.beforeMove);
        }

        [TestMethod]
        public void startTurn_GameIsOver()
        {
            Player player1 = new Player();                  // create player 1 to pass into startTurn()
            Player player2 = new Player();                  // create player 2
            Mock<Camera> mockCamera = new Mock<Camera>();   // create mock object of camera
            Mock<Unit> mockUnit = new Mock<Unit>();         // create mock object for unit
            Button[] buttons = new Button[9];               // create buttons for mockUnit
            buttons[0] = new Button(ButtonType.Attack, new Vector2(0, 0), null);        // sets buttons
            buttons[1] = new Button(ButtonType.AttackConfirm, new Vector2(0, 0), null);
            buttons[2] = new Button(ButtonType.Inventory1, new Vector2(0, 0), null);
            buttons[3] = new Button(ButtonType.Inventory2, new Vector2(0, 0), null);
            buttons[4] = new Button(ButtonType.Inventory3, new Vector2(0, 0), null);
            buttons[5] = new Button(ButtonType.Inventory4, new Vector2(0, 0), null);
            buttons[6] = new Button(ButtonType.Items, new Vector2(0, 0), null);
            buttons[7] = new Button(ButtonType.Move, new Vector2(0, 0), null);
            buttons[8] = new Button(ButtonType.Wait, new Vector2(0, 0), null);
            foreach (Button button in buttons)
            {
                button.Active = false;  // set all button.Active to false to ensure assertions are correct
            }
            mockUnit.Setup(Unit => Unit.getButtons()).Returns(buttons); // mock mockUnit.getButtons() to return buttons
            GameState.Player1 = player1;    // set players in GameState
            GameState.Player2 = player2;
            GameState.currentPlayer = player1;
            player1.addUnit(mockUnit.Object);   // add mockUnit to only player 1 (player 2 has 0 units so game is over)

            // call function to test
            GameFunction.startTurn(player1, mockCamera.Object);

            // verify buttons are set to Active
            mockUnit.Verify(Unit => Unit.getButtons(), Times.Once());
            Assert.IsTrue(buttons[0].Active);
            Assert.IsTrue(buttons[1].Active);
            Assert.IsTrue(buttons[2].Active);
            Assert.IsTrue(buttons[3].Active);
            Assert.IsTrue(buttons[4].Active);
            Assert.IsTrue(buttons[5].Active);
            Assert.IsTrue(buttons[6].Active);
            Assert.IsTrue(buttons[7].Active);
            Assert.IsTrue(buttons[8].Active);

            // check that the function sets all states in GameState correctly
            Assert.IsNull(GameState.selectedUnit);
            Assert.IsNull(GameState.unitToAttack);
            Assert.IsFalse(GameState.dropDownMenuOpen);
            Assert.IsFalse(GameState.attackConfirmOpen);
            Assert.IsTrue(GameState.beforeMove);
        }

        [TestMethod]
        public void startTurn_WhenNoUnits()
        {
            Player player1 = new Player();                  // create player 1 to pass into startTurn()
            Player player2 = new Player();                  // create player 2
            Mock<Camera> mockCamera = new Mock<Camera>();   // create mock object of camera
            GameState.Player1 = player1;    // set players in GameState
            GameState.Player2 = player2;
            GameState.currentPlayer = player1;

            // call function to test
            GameFunction.startTurn(player1, mockCamera.Object);

            // check that the function sets all states in GameState correctly
            Assert.IsNull(GameState.selectedUnit);
            Assert.IsNull(GameState.unitToAttack);
            Assert.IsFalse(GameState.dropDownMenuOpen);
            Assert.IsFalse(GameState.attackConfirmOpen);
            Assert.IsTrue(GameState.beforeMove);
        }

        [TestMethod]
        public void startTurn_WhenUnitHasNullButtonArray()
        {
            Player player1 = new Player();                  // create player 1 to pass into startTurn()
            Player player2 = new Player();                  // create player 2
            Mock<Camera> mockCamera = new Mock<Camera>();   // create mock object of camera
            Mock<Unit> mockUnit = new Mock<Unit>();         // create mock object for unit
            GameState.Player1 = player1;    // set players in GameState
            GameState.Player2 = player2;
            GameState.currentPlayer = player1;
            player1.addUnit(mockUnit.Object);   // add mockUnit to players (so isGameOver() returns false)
            player2.addUnit(mockUnit.Object);

            // call function to test
            GameFunction.startTurn(player1, mockCamera.Object);

            // verify buttons are set to Active
            mockUnit.Verify(Unit => Unit.getButtons(), Times.Once());

            // check that the function sets all states in GameState correctly
            Assert.IsNull(GameState.selectedUnit);
            Assert.IsNull(GameState.unitToAttack);
            Assert.IsFalse(GameState.dropDownMenuOpen);
            Assert.IsFalse(GameState.attackConfirmOpen);
            Assert.IsTrue(GameState.beforeMove);
        }

        [TestMethod]
        public void startTurn_WhenUnitButtonArrayHasNullButtons_ShouldThrowNullReferenceException()
        {
            try
            {
                Player player1 = new Player();                  // create player 1 to pass into startTurn()
                Player player2 = new Player();                  // create player 2
                Mock<Camera> mockCamera = new Mock<Camera>();   // create mock object of camera
                Mock<Unit> mockUnit = new Mock<Unit>();         // create mock object for unit
                Button[] buttons = new Button[9];               // create buttons for mockUnit (with null buttons)
                mockUnit.Setup(Unit => Unit.getButtons()).Returns(buttons); // mock mockUnit.getButtons() to return buttons
                GameState.Player1 = player1;    // set players in GameState
                GameState.Player2 = player2;
                GameState.currentPlayer = player1;
                player1.addUnit(mockUnit.Object);   // add mockUnit to players (so isGameOver() returns false)
                player2.addUnit(mockUnit.Object);

                // call function to test
                GameFunction.startTurn(player1, mockCamera.Object);
            }
            catch(Exception ex)
            {
                Assert.IsTrue(ex is NullReferenceException);
            }
        }

        [TestMethod]
        // test if units are set to inactive after clicking attack confirm or wait
        public void hasUnitFinishedActions_AttackConfirmOrWaitIsFalse_ShouldReturnTrue()
        {
            Mock<Unit> mockUnit = new Mock<Unit>();         // create mock object for unit

            // test for when attackConfirm is active, but wait is not
            mockUnit.Setup(Unit => Unit.isButtonActive(ButtonType.AttackConfirm)).Returns(true);
            mockUnit.Setup(Unit => Unit.isButtonActive(ButtonType.Wait)).Returns(false);
            Assert.IsTrue(GameFunction.hasUnitFinishedActions(mockUnit.Object));

            // test for when attackConfirm is not active, but wait is
            mockUnit.Setup(Unit => Unit.isButtonActive(ButtonType.AttackConfirm)).Returns(false);
            mockUnit.Setup(Unit => Unit.isButtonActive(ButtonType.Wait)).Returns(true);
            Assert.IsTrue(GameFunction.hasUnitFinishedActions(mockUnit.Object));
        }

        [TestMethod]
        public void hasUnitFinishedActions_AttackConfirmAndWaitIsTrue_ShouldReturnFalse()
        {
            Mock<Unit> mockUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.isButtonActive(ButtonType.AttackConfirm)).Returns(true);
            mockUnit.Setup(Unit => Unit.isButtonActive(ButtonType.Wait)).Returns(true);
            Assert.IsFalse(GameFunction.hasUnitFinishedActions(mockUnit.Object));
        }

        [TestMethod]
        public void hasUnitFinishedActions_AttackConfirmOrWaitIsNull_ShouldReturnTrue()
        {
            Mock<Unit> mockUnit = new Mock<Unit>();
            Assert.IsTrue(GameFunction.hasUnitFinishedActions(mockUnit.Object));
        }

        [TestMethod]
        public void hasUnitFinishedActions_UnitIsNull_ShouldThrowNullReferenceException()
        {
            try
            {
                GameFunction.hasUnitFinishedActions(null);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NullReferenceException);
            }
        }

        [TestMethod]
        // test if turn over is true when all units have went
        public void isTurnOver_ShouldReturnTrue()
        {
            Mock<Unit> mockUnit = new Mock<Unit>();

            // set unit actions to finished
            mockUnit.Setup(Unit => Unit.isButtonActive(ButtonType.AttackConfirm)).Returns(false);
            mockUnit.Setup(Unit => Unit.isButtonActive(ButtonType.Wait)).Returns(false);
            bool unitTurnOver = GameFunction.hasUnitFinishedActions(mockUnit.Object);
            
            // create a player, and add unit to player
            Player player1 = new Player();
            player1.addUnit(mockUnit.Object);
            GameState.currentPlayer = player1;
            
            // turn should be over since the player's only unit has already finished its action
            Assert.IsTrue(GameFunction.isTurnOver());
        }

        [TestMethod]
        // test if turn over is true when there are units left who haven't performed their action
        public void isTurnOver_ShouldReturnFalse()
        {
            Mock<Unit> mockUnit = new Mock<Unit>(); // mock unit
            
            // set unit actions to finished
            mockUnit.Setup(Unit => Unit.isButtonActive(ButtonType.AttackConfirm)).Returns(true);
            mockUnit.Setup(Unit => Unit.isButtonActive(ButtonType.Wait)).Returns(true);
            bool unitTurnOver = GameFunction.hasUnitFinishedActions(mockUnit.Object);
            
            // create a player, and add unit to player
            Player player1 = new Player();
            player1.addUnit(mockUnit.Object);
            GameState.currentPlayer = player1;
            
            // turn should be over since the player's only unit has already finished its action
            Assert.IsFalse(GameFunction.isTurnOver());
        }

        [TestMethod]
        public void isTurnOver_CurrentPlayerIsNull_ShouldThrowNullReferenceException()
        {
            try
            {
                GameFunction.isTurnOver();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NullReferenceException);
            }
        }

        [TestMethod]
        public void isTurnOver_CurrentPlayerHasNoUnits_ShouldReturnTrue()
        {
            Player player = new Player();
            GameState.currentPlayer = player;
            Assert.IsTrue(GameFunction.isTurnOver());
        }

        [TestMethod]
        // tests if game ends when player 1 has no units left
        public void isGameOver_Player1WithNoUnitsLeft_ShouldReturnTrue()
        {
            Player player = new Player();
            GameState.Player1 = player;
            Assert.IsTrue(GameFunction.isGameOver());
        }

        [TestMethod]
        // tests if game ends when player 2 has no units left
        public void isGameOver_Player2WithNoUnitsLeft_ShouldReturnTrue()
        {
            Player player = new Player();
            GameState.Player2 = player;
            Assert.IsTrue(GameFunction.isGameOver());
        }

        [TestMethod]
        // tests if game ends when both players have no units left
        public void isGameOver_BothPlayersHaveNoUnitsLeft_ShouldReturnTrue()
        {
            Player player1 = new Player();
            Player player2 = new Player();
            GameState.Player1 = player1;
            GameState.Player2 = player2;
            Assert.IsTrue(GameFunction.isGameOver());
        }

        [TestMethod]
        public void isGameOver_BothPlayersHaveUnits_ShouldReturnFalse()
        {
            Mock<Unit> mockUnit = new Mock<Unit>();
            Player player1 = new Player();
            Player player2 = new Player();
            player1.addUnit(mockUnit.Object);   // add mock unit to both players
            player2.addUnit(mockUnit.Object);
            GameState.Player1 = player1;
            GameState.Player2 = player2;
            Assert.IsFalse(GameFunction.isGameOver());
        }

        [TestMethod]
        public void isNodeAttackable_InMeleeRange_ShouldReturnTrue()
        {
            Mock<Unit> mockUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.equippedWeapon).Returns(new BronzeSword());

            // check bottom node
            Node node = new Node(10, 11);
            Assert.IsTrue(GameFunction.isNodeAttackable(mockUnit.Object, 10, 10, node));

            // check up node
            node = new Node(10, 9);
            Assert.IsTrue(GameFunction.isNodeAttackable(mockUnit.Object, 10, 10, node));

            // check left node
            node = new Node(9, 10);
            Assert.IsTrue(GameFunction.isNodeAttackable(mockUnit.Object, 10, 10, node));

            // check right node
            node = new Node(11, 10);
            Assert.IsTrue(GameFunction.isNodeAttackable(mockUnit.Object, 10, 10, node));
        }

        [TestMethod]
        public void isNodeAttackable_NotInMeleeRange_ShouldReturnFalse()
        {
            Mock<Unit> mockUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.equippedWeapon).Returns(new BronzeSword());

            // check far away node
            Node node = new Node(30, 10);
            Assert.IsFalse(GameFunction.isNodeAttackable(mockUnit.Object, 10, 10, node));
        }

        [TestMethod]
        public void setMovableNodes_ShouldReturnNonNull()
        {
            Graph graph = new Graph(50, 32);
            Mock<Unit> mockUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(10, 10));
            mockUnit.Setup(Unit => Unit.getMovability()).Returns(4);
            Assert.IsNotNull(GameFunction.setMovableNodes(graph, mockUnit.Object));
        }

        [TestMethod]
        public void setMovableNodes_NullGraph_ShouldThrowNullReferenceException()
        {
            try
            {
                Mock<Unit> mockUnit = new Mock<Unit>();
                mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(10, 10));
                mockUnit.Setup(Unit => Unit.getMovability()).Returns(4);
                GameFunction.setMovableNodes(null, mockUnit.Object);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NullReferenceException);
            }
        }

        [TestMethod]
        public void setMovableNodes_NullUnit_ShouldThrowNullReferenceException()
        {
            try
            {
                Graph graph = new Graph(50, 32);
                GameFunction.setMovableNodes(graph, null);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NullReferenceException);
            }
        }

        [TestMethod]
        public void getAttackableNodes_ShouldBeNonNull()
        {
            Graph graph = new Graph(50, 32);
            Mock<Unit> mockUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(10, 10));
            mockUnit.Setup(Unit => Unit.equippedWeapon).Returns(new BronzeSword());
            Assert.IsNotNull(GameFunction.getAttackableNodes(graph, mockUnit.Object));
        }

        [TestMethod]
        public void getAttackableNodes_NullGraph_ShouldThrowNullReferenceException()
        {
            try
            {
                Mock<Unit> mockUnit = new Mock<Unit>();
                mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(10, 10));
                mockUnit.Setup(Unit => Unit.getMovability()).Returns(4);
                GameFunction.getAttackableNodes(null, mockUnit.Object);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NullReferenceException);
            }
        }

        [TestMethod]
        public void getAttackableNodes_NullUnit_ShouldThrowNullReferenceException()
        {
            try
            {
                Graph graph = new Graph(50, 32);
                GameFunction.getAttackableNodes(graph, null);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NullReferenceException);
            }
        }

        [TestMethod]
        public void getAttackRangeAfterMoving_MeleeWeapon_ShouldBeNonNull()
        {
            Graph graph = new Graph(50, 32);
            Mock<Unit> mockUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(10, 10));
            mockUnit.Setup(Unit => Unit.equippedWeapon).Returns(new BronzeSword());
            LinkedList<Node> attackRange = GameFunction.getAttackRangeAfterMoving(graph, mockUnit.Object);
            Assert.IsNotNull(attackRange);
            
            // check that the attackable nodes contain nodes within attack range of melee weapon
            Assert.IsTrue(attackRange.Contains(graph.getNode(9, 10)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(11, 10)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(10, 9)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(10, 11)));
        }

        [TestMethod]
        public void getAttackRangeAfterMoving_MagicWeapon_ShouldBeNonNull()
        {
            Graph graph = new Graph(50, 32);
            Mock<Unit> mockUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(10, 10));
            mockUnit.Setup(Unit => Unit.equippedWeapon).Returns(new Fireball());
            LinkedList<Node> attackRange = GameFunction.getAttackRangeAfterMoving(graph, mockUnit.Object);
            Assert.IsNotNull(attackRange);

            // check that the attackable nodes contain nodes within attack range of magic weapon
            Assert.IsTrue(attackRange.Contains(graph.getNode(9, 10)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(11, 10)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(10, 9)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(10, 11)));
            // two range away
            Assert.IsTrue(attackRange.Contains(graph.getNode(8, 10)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(12, 10)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(10, 8)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(10, 12)));
            // diagonal
            Assert.IsTrue(attackRange.Contains(graph.getNode(9, 9)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(11, 11)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(9, 11)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(11, 9)));
        }

        [TestMethod]
        public void getAttackRangeAfterMoving_ShortBow_ShouldBeNonNull()
        {
            Graph graph = new Graph(50, 32);
            Mock<Unit> mockUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(10, 10));
            mockUnit.Setup(Unit => Unit.equippedWeapon).Returns(new ShortBow());
            LinkedList<Node> attackRange = GameFunction.getAttackRangeAfterMoving(graph, mockUnit.Object);
            Assert.IsNotNull(attackRange);

            // two range away
            Assert.IsTrue(attackRange.Contains(graph.getNode(8, 10)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(12, 10)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(10, 8)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(10, 12)));
            // diagonal
            Assert.IsTrue(attackRange.Contains(graph.getNode(9, 9)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(11, 11)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(9, 11)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(11, 9)));
        }

        [TestMethod]
        public void getAttackRangeAfterMoving_LongBow_ShouldBeNonNull()
        {
            Graph graph = new Graph(50, 32);
            Mock<Unit> mockUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(10, 10));
            mockUnit.Setup(Unit => Unit.equippedWeapon).Returns(new LongBow());
            LinkedList<Node> attackRange = GameFunction.getAttackRangeAfterMoving(graph, mockUnit.Object);
            Assert.IsNotNull(attackRange);

            // two range away
            Assert.IsTrue(attackRange.Contains(graph.getNode(8, 10)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(12, 10)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(10, 8)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(10, 12)));
            // three range away
            Assert.IsTrue(attackRange.Contains(graph.getNode(7, 10)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(13, 10)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(10, 7)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(10, 13)));
            // diagonal
            Assert.IsTrue(attackRange.Contains(graph.getNode(9, 9)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(11, 11)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(9, 11)));
            Assert.IsTrue(attackRange.Contains(graph.getNode(11, 9)));
        }

        [TestMethod]
        public void getAttackRangeAfterMoving_NullGraph_ShouldThrowNullReferenceException()
        {
            try
            {
                Mock<Unit> mockUnit = new Mock<Unit>();
                mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(10, 10));
                mockUnit.Setup(Unit => Unit.getMovability()).Returns(4);
                GameFunction.getAttackRangeAfterMoving(null, mockUnit.Object);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NullReferenceException);
            }
        }

        [TestMethod]
        public void getAttackRangeAfterMoving_NullUnit_ShouldThrowNullReferenceException()
        {
            try
            {
                Graph graph = new Graph(50, 32);
                GameFunction.getAttackRangeAfterMoving(graph, null);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NullReferenceException);
            }
        }

        [TestMethod]
        public void pathFinder_EndNodeIsObstructed_ShouldReturnNull()
        {
            Graph graph = new Graph(50, 32);
            Mock<Unit> mockUnit = new Mock<Unit>();
            Node start = graph.getNode(10, 10);
            Node end = graph.getNode(10, 14);
            end.isObstacle = true;

            // check path is null if end node is an obstacle
            Assert.IsNull(GameFunction.pathFinder(graph, mockUnit.Object, start, end));

            // check path is null if end node is occupied
            end.isObstacle = false;
            end.unitOnNode = mockUnit.Object;
            Assert.IsNull(GameFunction.pathFinder(graph, mockUnit.Object, start, end));
        }

        [TestMethod]
        public void pathFinder_PathAvailable_ShouldReturnNonNull()
        {
            Graph graph = new Graph(50, 32);
            Mock<Unit> mockUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.getMovability()).Returns(4);

            // check linear path
            Node start = graph.getNode(10, 10);
            Node end = graph.getNode(14, 10);
            Assert.IsNotNull(GameFunction.pathFinder(graph, mockUnit.Object, start, end));

            // check diagonal path
            start = graph.getNode(10, 10);
            end = graph.getNode(12, 12);
            Assert.IsNotNull(GameFunction.pathFinder(graph, mockUnit.Object, start, end));
        }

        [TestMethod]
        public void removeUnit_ShouldRemoveUnit()
        {
            Graph graph = new Graph(4, 4);      // set up graph of 4x4
            Player player = new Player();       // create player
            Mock<Unit> mockUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(2, 2));
            player.addUnit(mockUnit.Object);    // add unit to player

            GameFunction.removeUnit(graph, player, mockUnit.Object); // remove unit from node
            Assert.IsFalse(player.getUnits().Contains(mockUnit.Object)); // check unit is removed
        }

        [TestMethod]
        public void removeUnit_NullUnit_ShouldThrowNullReferenceException()
        {
            try
            {
                Graph graph = new Graph(50, 32);
                Player player = new Player();
                GameFunction.removeUnit(graph, player, null);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NullReferenceException);
            }
        }

        [TestMethod]
        public void removeUnit_NullGraph_ShouldThrowNullReferenceException()
        {
            try
            {
                Player player = new Player();
                Mock<Unit> mockUnit = new Mock<Unit>();
                mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(2, 2));
                player.addUnit(mockUnit.Object);    // add unit to player
                GameFunction.removeUnit(null, player, mockUnit.Object);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NullReferenceException);
            }
        }

        [TestMethod]
        public void removeUnit_NullPlayer_ShouldThrowNullReferenceException()
        {
            try
            {
                Graph graph = new Graph(50, 32);
                Mock<Unit> mockUnit = new Mock<Unit>();
                mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(2, 2));
                GameFunction.removeUnit(graph, null, mockUnit.Object);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NullReferenceException);
            }
        }

        [TestMethod]
        public void deselectUnit_ShouldSetGameState()
        {
            //set all states to true or have an item
            GameState.selectedUnit = new Mock<Unit>().Object;
            GameState.selectedEnemyUnit = new Mock<Unit>().Object;
            GameState.TurnState = TurnState.Attack;
            GameState.unitToAttack = new Mock<Unit>().Object;
            GameState.attackConfirmOpen = true;
            GameState.dropDownMenuOpen = true;
            GameState.attackSelect = true;
            GameState.inventoryOpen = true;

            //checks that deselect sets required GameState parameters
            GameFunction.deselectUnit();
            Assert.IsNull(GameState.selectedUnit);
            Assert.IsNull(GameState.selectedEnemyUnit);
            Assert.AreEqual(TurnState.Wait, GameState.TurnState);
            Assert.IsNull(GameState.unitToAttack);
            Assert.IsFalse(GameState.attackConfirmOpen);
            Assert.IsFalse(GameState.dropDownMenuOpen);
            Assert.IsFalse(GameState.attackSelect);
            Assert.IsFalse(GameState.inventoryOpen);
        }

        [TestMethod]
        public void getUnitOnNodeClicked_ShouldReturnExpectedUnit()
        {
            Player player = new Player();           // create player
            Mock<Unit> mockUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(2, 2));
            Node node = new Node(2, 2);             // create node and position
            player.addUnit(mockUnit.Object);        // add unit to player
            node.unitOnNode = mockUnit.Object;      // add unit to node
            Vector2 click = new Vector2(70, 70);    // click location
            
            // check unit returned is correct
            Assert.AreEqual(mockUnit.Object, GameFunction.getUnitOnNodeClicked(node, click, player));
        }

        [TestMethod]
        public void getUnitOnNodeClicked_ShouldReturnNull()
        {
            Player player = new Player();           // create player
            Mock<Unit> mockUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(2, 2));
            Node node = new Node(2, 2);             // create node and position
            player.addUnit(mockUnit.Object);        // add unit to player
            node.unitOnNode = mockUnit.Object;      // add unit to node
            Vector2 click = new Vector2(240, 240);    // click location

            // check unit returned is correct
            Assert.IsNull(GameFunction.getUnitOnNodeClicked(node, click, player));
        }

        [TestMethod]
        // TODO: currently impossible due to call on Animation class
        public void updateUnitPosition_ShouldUpdateUnitPosition()
        {
            //Mock<Unit> mockUnit = new Mock<Unit>();
            //mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(10, 10));
            //GameState.selectedUnit = mockUnit.Object;
            //Graph graph = new Graph(50, 32);
            //LinkedList<Node> path = new LinkedList<Node>();
            //path.AddLast(graph.getNode(10, 10));
            //path.AddLast(graph.getNode(11, 10));
            //path.AddLast(graph.getNode(12, 10));
            //path.AddLast(graph.getNode(13, 10));

            //Assert.IsNull(graph.getNode(13, 10).unitOnNode);

            //GameFunction.updateUnitPosition(graph, path);

            //Assert.IsNull(graph.getNode(10, 10).unitOnNode);
            //Assert.AreEqual(mockUnit, graph.getNode(13, 10).unitOnNode);
            //Assert.IsTrue(GameState.dropDownMenuOpen);
            //Assert.IsFalse(GameState.isAnimating);
        }

        [TestMethod]
        public void endTurn_ShouldSetGameState()
        {
            //initialize players currentPlayer & enemyPlayer and mockCamera
            Player player1 = new Player();
            Player player2 = new Player();
            Mock<Camera> mockCamera = new Mock<Camera>();
            Mock<Unit> mockUnit = new Mock<Unit>();
            GameState.currentPlayer = player1;
            GameState.enemyPlayer = player2;
            GameState.endTurnButton = true;
            GameState.transitionTurn = false;
            player2.addUnit(mockUnit.Object);

            //test if the currentPlayer and enemyPlayer correctly swapped
            GameFunction.endTurn(mockCamera.Object);
            Assert.AreSame(player2, GameState.currentPlayer);
            Assert.AreSame(player1, GameState.enemyPlayer);
        }

        [TestMethod]
        public void buttonAction_AttackButton()
        {
            Button button = new Button(ButtonType.Attack, new Vector2(0, 0), null);
            Graph graph = new Graph(50, 32);

            GameState.TurnState = TurnState.Wait;
            GameState.dropDownMenuOpen = true;
            GameState.attackSelect = false;
            GameState.inventoryOpen = true;

            GameFunction.buttonAction(button, graph);

            Assert.AreEqual(TurnState.Attack, GameState.TurnState);
            Assert.IsFalse(GameState.dropDownMenuOpen);
            Assert.IsTrue(GameState.attackSelect);
            Assert.IsFalse(GameState.inventoryOpen);
        }

        [TestMethod]
        // TODO: impossible due to calls to Sound and Animation
        public void buttonAction_AttackConfirmButton()
        {
            //Button button = new Button(ButtonType.AttackConfirm, new Vector2(0, 0), null);
            //Button unitButtonAttack = new Button(ButtonType.Attack, new Vector2(0, 0), null);
            //Button unitButtonMove = new Button(ButtonType.Move, new Vector2(0, 0), null);
            //Graph graph = new Graph(50, 32);
            //Mock<Unit> mockUnit = new Mock<Unit>();
            //Mock<Unit> mockEnemyUnit = new Mock<Unit>();
            //mockUnit.Setup(Unit => Unit.getButtonType(ButtonType.Move)).Returns(unitButtonMove);
            //mockUnit.Setup(Unit => Unit.getButtonType(ButtonType.Attack)).Returns(unitButtonAttack);
            //mockUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(10, 10));
            //mockEnemyUnit.Setup(Unit => Unit.Position).Returns(new Tuple<int, int>(11, 10));
            //mockUnit.Setup(Unit => Unit.equippedWeapon).Returns(new BronzeSword());
            //GameState.selectedUnit = mockUnit.Object;
            //GameState.unitToAttack = mockEnemyUnit.Object;

            //GameState.TurnState = TurnState.Wait;
            //GameState.dropDownMenuOpen = true;
            //GameState.attackSelect = false;
            //GameState.inventoryOpen = true;
            //unitButtonAttack.Active = true;
            //unitButtonMove.Active = true;

            //GameFunction.buttonAction(button, graph);

            //Assert.IsFalse(unitButtonMove.Active);
            //Assert.IsFalse(unitButtonAttack.Active);
            //Assert.AreEqual(TurnState.Attack, GameState.TurnState);
            //Assert.IsFalse(GameState.dropDownMenuOpen);
            //Assert.IsTrue(GameState.attackSelect);
            //Assert.IsFalse(GameState.inventoryOpen);
        }

        [TestMethod]
        public void buttonAction_MoveButton()
        {
            Button button = new Button(ButtonType.Move, new Vector2(0, 0), null);
            Graph graph = new Graph(50, 32);

            GameState.TurnState = TurnState.Wait;
            GameState.dropDownMenuOpen = true;
            GameState.inventoryOpen = true;

            GameFunction.buttonAction(button, graph);

            Assert.AreEqual(TurnState.Move, GameState.TurnState);
            Assert.IsFalse(GameState.dropDownMenuOpen);
            Assert.IsFalse(GameState.inventoryOpen);
        }

        [TestMethod]
        public void buttonAction_ItemsButton()
        {
            Button button = new Button(ButtonType.Items, new Vector2(0, 0), null);
            Graph graph = new Graph(50, 32);

            GameState.TurnState = TurnState.Wait;
            GameState.inventoryOpen = false;

            GameFunction.buttonAction(button, graph);

            Assert.AreEqual(TurnState.Items, GameState.TurnState);
            Assert.IsTrue(GameState.inventoryOpen);
        }

        [TestMethod]
        public void buttonAction_WaitButton()
        {
            Button button = new Button(ButtonType.Wait, new Vector2(0, 0), null);
            Graph graph = new Graph(50, 32);

            GameState.TurnState = TurnState.Move;
            button.Active = true;

            GameFunction.buttonAction(button, graph);

            Assert.AreEqual(TurnState.Wait, GameState.TurnState);
            Assert.IsFalse(button.Active);
        }

        [TestMethod]
        public void buttonAction_InventoryButtons()
        {
            Graph graph = new Graph(50, 32);
            Mock<Unit> mockUnit = new Mock<Unit>();
            GameState.selectedUnit = mockUnit.Object;

            // check inventory 1
            Button button = new Button(ButtonType.Inventory1, new Vector2(0, 0), null);
            button.weapon = new BronzeSword();
            button.hasItem = true;
            GameState.inventoryOpen = true;
            GameFunction.buttonAction(button, graph);
            Assert.IsFalse(GameState.inventoryOpen);

            // check inventory 2
            button = new Button(ButtonType.Inventory2, new Vector2(0, 0), null);
            button.weapon = new BronzeSword();
            button.hasItem = true;
            GameState.inventoryOpen = true;
            GameFunction.buttonAction(button, graph);
            Assert.IsFalse(GameState.inventoryOpen);

            // check inventory 3
            button = new Button(ButtonType.Inventory3, new Vector2(0, 0), null);
            button.weapon = new BronzeSword();
            button.hasItem = true;
            GameState.inventoryOpen = true;
            GameFunction.buttonAction(button, graph);
            Assert.IsFalse(GameState.inventoryOpen);

            // check inventory 4
            button = new Button(ButtonType.Inventory4, new Vector2(0, 0), null);
            button.weapon = new BronzeSword();
            button.hasItem = true;
            GameState.inventoryOpen = true;
            GameFunction.buttonAction(button, graph);
            Assert.IsFalse(GameState.inventoryOpen);
        }

        [TestMethod]
        public void isMagicalAttack_IsWarrior_ShouldReturnFalse()
        {
            Mock<Unit> mockUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.getClass()).Returns(UnitType.Warrior);
            Assert.IsFalse(GameFunction.isMagicalAttack(mockUnit.Object));
        }

        [TestMethod]
        public void isMagicalAttack_IsArcher_ShouldReturnFalse()
        {
            Mock<Unit> mockUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.getClass()).Returns(UnitType.Archer);
            Assert.IsFalse(GameFunction.isMagicalAttack(mockUnit.Object));
        }

        [TestMethod]
        public void isMagicalAttack_IsMage_ShouldReturnTrue()
        {
            Mock<Unit> mockUnit = new Mock<Unit>();
            mockUnit.Setup(Unit => Unit.getClass()).Returns(UnitType.Mage);
            Assert.IsTrue(GameFunction.isMagicalAttack(mockUnit.Object));
        }

        [TestMethod]
        public void isMagicalAttack_NoUnitType_ShouldReturnFalse()
        {
            Mock<Unit> mockUnit = new Mock<Unit>();
            Assert.IsFalse(GameFunction.isMagicalAttack(mockUnit.Object));
        }

        [TestMethod]
        public void isMagicalAttack_NullUnit_ShouldThrowNullReferenceException()
        {
            try
            {
                Assert.IsFalse(GameFunction.isMagicalAttack(null));
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is NullReferenceException);
            }
        }

        [TestMethod]
        public void getMenuButtonClicked_AttackButtonClicked()
        {
            // setup for attack button click
            Vector2 mouseCoordinates = new Vector2(320, 320);
            Camera camera = new Camera();
            Mock<Unit> mockUnit = new Mock<Unit>();
            GameState.selectedUnit = mockUnit.Object;
            Button[] buttons = new Button[1];
            Button button = new Button(ButtonType.Attack, new Vector2(300, 300), null);
            buttons[0] = button;
            mockUnit.Setup(Unit => Unit.getButtons()).Returns(buttons);
            GameState.dropDownMenuOpen = true;

            Assert.AreEqual(button, GameFunction.getMenuButtonClicked(mouseCoordinates, camera));
        }

        [TestMethod]
        public void getMenuButtonClicked_NoButtonClicked()
        {
            // setup for attack button click
            Vector2 mouseCoordinates = new Vector2(320, 320);
            Camera camera = new Camera();
            Mock<Unit> mockUnit = new Mock<Unit>();
            GameState.selectedUnit = mockUnit.Object;
            Button[] buttons = new Button[1];
            Button button = new Button(ButtonType.AttackConfirm, new Vector2(600, 300), null);
            buttons[0] = button;
            mockUnit.Setup(Unit => Unit.getButtons()).Returns(buttons);
            mockUnit.Setup(Unit => Unit.getButtonType(ButtonType.AttackConfirm)).Returns(button);
            GameState.dropDownMenuOpen = true;

            Assert.IsNull(GameFunction.getMenuButtonClicked(mouseCoordinates, camera));
        }

        [TestMethod]
        // TODO: impossible due to call to Game (which is not instantiated unless actual game is run)
        public void scrollCamera()
        {
            //Camera camera = new Camera();
            //GameFunction.scrollMap(camera, 50, 50);
        }
    }
}
