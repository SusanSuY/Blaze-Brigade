using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using Model.UnitModule;

namespace ModelTest
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void Player_Constructor_ShouldBehaveAsExpected()
        {
            Player player = new Player();
            Assert.IsNotNull(player.getUnits());
            Assert.AreEqual(0, player.getNumOfUnits());
        }

        [TestMethod]
        public void getNumOfUnits_ShouldReturnExpected()
        {
            Player player = new Player();
            Assert.AreEqual(0, player.getNumOfUnits());
            player.addUnit(new Mock<Unit>().Object);
            Assert.AreEqual(1, player.getNumOfUnits());
            player.addUnit(new Mock<Unit>().Object);
            Assert.AreEqual(2, player.getNumOfUnits());
            player.addUnit(new Mock<Unit>().Object);
            Assert.AreEqual(3, player.getNumOfUnits());
            player.addUnit(new Mock<Unit>().Object);
            Assert.AreEqual(4, player.getNumOfUnits());
            player.addUnit(new Mock<Unit>().Object);
            Assert.AreEqual(5, player.getNumOfUnits());
        }

        [TestMethod]
        public void ownsUnit_ShouldReturnTrue()
        {
            Mock<Unit> mockUnit = new Mock<Unit>();
            Player player = new Player();
            player.addUnit(mockUnit.Object);
            Assert.IsTrue(player.ownsUnit(mockUnit.Object));
        }

        [TestMethod]
        public void ownsUnit_ShouldReturnFalse()
        {
            Player player = new Player();
            Assert.IsFalse(player.ownsUnit(new Mock<Unit>().Object));
        }

        [TestMethod]
        public void removeUnit_ShouldBehaveAsExpected()
        {
            Player player = new Player();
            Mock<Unit> mockUnit = new Mock<Unit>();
            player.addUnit(mockUnit.Object);
            Assert.IsTrue(player.ownsUnit(mockUnit.Object));
            player.removeUnit(mockUnit.Object);
            Assert.IsFalse(player.ownsUnit(mockUnit.Object));
        }

        [TestMethod]
        public void removeUnit_RemoveUnownedUnit_ShouldDoNothing()
        {
            Player player = new Player();
            player.addUnit(new Mock<Unit>().Object);
            Assert.AreEqual(1, player.getNumOfUnits());
            player.removeUnit(new Mock<Unit>().Object);
            Assert.AreEqual(1, player.getNumOfUnits());
        }

        [TestMethod]
        public void removeUnit_RemoveNullUnit_ShouldDoNothing()
        {
            Player player = new Player();
            player.addUnit(new Mock<Unit>().Object);
            Assert.AreEqual(1, player.getNumOfUnits());
            player.removeUnit(null);
            Assert.AreEqual(1, player.getNumOfUnits());
        }
    }
}
