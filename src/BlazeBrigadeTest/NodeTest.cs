using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Model;
using Model.MapModule;
using Model.UnitModule;
using Moq;
using System;

namespace ModelTest
{
    namespace MapModuleTest
    {
        [TestClass]
        public class NodeTest
        {
            [TestMethod]
            public void Node_Constructor_ShouldWorkAsExpected()
            {
                Node node = new Node(5, 15);

                Assert.AreEqual(5, node.getPositionX());
                Assert.AreEqual(15, node.getPositionY());
                Assert.AreEqual(0, node.movabilityObstruction);
                Assert.IsFalse(node.isObstacle);
                Assert.IsFalse(node.isOccupied());
            }

            [TestMethod]
            public void Node_Constructor_ZeroNumbers_ShouldWorkAsExpected()
            {
                Node node = new Node(0, 0);

                Assert.AreEqual(0, node.getPositionX());
                Assert.AreEqual(0, node.getPositionY());
                Assert.AreEqual(0, node.movabilityObstruction);
                Assert.IsFalse(node.isObstacle);
                Assert.IsFalse(node.isOccupied());
            }

            [TestMethod]
            public void Node_Constructor_NegativeNumbers_ShouldThrowOverflowException()
            {
                try
                {
                    Node node = new Node(-1, -1);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex is System.OverflowException);
                }
            }

            [TestMethod]
            public void getPosition_ShouldReturnExpected()
            {
                // check positive number
                Node node = new Node(5, 5);
                Assert.AreEqual(new Vector2(160, 160), node.getPosition());

                // check zero
                Node zeroNode = new Node(0, 0);
                Assert.AreEqual(new Vector2(0, 0), zeroNode.getPosition());
            }

            [TestMethod]
            public void isOccupied_ShouldReturnTrue()
            {
                Node node = new Node(0, 0);
                Assert.IsFalse(node.isOccupied());
                node.unitOnNode = new Mock<Unit>().Object;
                Assert.IsTrue(node.isOccupied());
            }

            [TestMethod]
            public void isOccupied_SetUnitToNull_ShouldReturnFalse()
            {
                Node node = new Node(0, 0);
                Assert.IsFalse(node.isOccupied());
                node.unitOnNode = null;
                Assert.IsFalse(node.isOccupied());
            }
        }
    }
}