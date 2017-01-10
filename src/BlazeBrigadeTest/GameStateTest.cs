using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace ModelTest
{
    [TestClass]
    public class GameStateTest
    {
        [TestMethod]
        public void screenDimensions_ShouldReturnExpected()
        {
            Assert.AreEqual(640, GameState.SCREEN_HEIGHT);
            Assert.AreEqual(960, GameState.SCREEN_WIDTH);
        }

        [TestMethod]
        public void CurrentPlayerDamageDealt_ShouldSetAsExpected()
        {
            // check positive number
            GameState.CurrentPlayerDamageDealt = "32";
            Assert.AreEqual("32", GameState.CurrentPlayerDamageDealt);

            // check zero
            GameState.CurrentPlayerDamageDealt = "0";
            Assert.AreEqual("Miss", GameState.CurrentPlayerDamageDealt);

            // check negative number
            GameState.CurrentPlayerDamageDealt = "-32";
            Assert.AreEqual("-32", GameState.CurrentPlayerDamageDealt);
        }

        [TestMethod]
        public void EnemyPlayerDamageDealt_ShouldSetAsExpected()
        {
            // check positive number
            GameState.EnemyPlayerDamageDealt = "32";
            Assert.AreEqual("32", GameState.EnemyPlayerDamageDealt);

            // check zero
            GameState.EnemyPlayerDamageDealt = "0";
            Assert.AreEqual("Miss", GameState.EnemyPlayerDamageDealt);

            // check negative number
            GameState.EnemyPlayerDamageDealt = "-32";
            Assert.AreEqual("-32", GameState.EnemyPlayerDamageDealt);
        }

        [TestMethod]
        public void GameMenuState_Enum_ShouldBeNonNull()
        {
            Assert.IsNotNull(GameMenuState.MainMenu);
            Assert.IsNotNull(GameMenuState.HowToPlay);
            Assert.IsNotNull(GameMenuState.HowToPlay2);
            Assert.IsNotNull(GameMenuState.HowToPlay3);
            Assert.IsNotNull(GameMenuState.Playing);
        }

        [TestMethod]
        public void TurnState_Enum_ShouldBeNonNull()
        {
            Assert.IsNotNull(TurnState.Wait);
            Assert.IsNotNull(TurnState.AttackMenu);
            Assert.IsNotNull(TurnState.Attack);
            Assert.IsNotNull(TurnState.Move);
            Assert.IsNotNull(TurnState.Items);
        }
    }
}
