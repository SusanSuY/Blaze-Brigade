using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using Model;
using Microsoft.Xna.Framework;
using Model.UnitModule;

namespace ViewTest
{
    [TestClass]
    public class AnimationTest
    {
        [TestMethod]
        // TODO: impossible due to call on Game.Instance; Game cannot be initialized outside of running the game (depends on XNA Framework)
        public void attackAnimation_ReturnToPreviousLocation_ShouldBehaveAsExpected()
        {
            //Warrior unit = new Warrior();
            //unit.PixelCoordinates = new Vector2(64, 64);
            ////Mock<Controller.Game> mockGame = new Mock<Controller.Game>();
            ////TODO setup Game.Instance.Tick() to do nothing

            ////test attacking right
            //View.Animation.attackAnimation(Direction.Right, unit); //animate unit attacking Right
            //Assert.AreEqual(unit.PixelCoordinates, new Vector2(64, 64));

            ////test attacking left
            //View.Animation.attackAnimation(Direction.Left, unit); //animate unit attacking Right
            //Assert.AreEqual(unit.PixelCoordinates, new Vector2(64, 64));

            ////test attacking up
            //View.Animation.attackAnimation(Direction.Up, unit); //animate unit attacking Right
            //Assert.AreEqual(unit.PixelCoordinates, new Vector2(64, 64));

            ////test attacking down
            //View.Animation.attackAnimation(Direction.Down, unit); //animate unit attacking Right
            //Assert.AreEqual(unit.PixelCoordinates, new Vector2(64, 64));
        }


        [TestMethod]
        // TODO: impossible due to call on Game.Instance; Game cannot be initialized outside of running the game (depends on XNA Framework)
        public void animateUnitPosition_MoveToNewLocation_ShouldBehaveAsExpected()
        {
            //Unit unit = new Warrior();
            //unit.PixelCoordinates = new Vector2(288, 320);
            //Graph graph = new Graph(50, 32);
            //Node node1 = new Node(9, 10);
            //Node node2 = new Node(10, 10);
            //node1.unitOnNode = unit;
            //graph.setNode(node1, 9, 10);

            ////set unit and node so that unit moves right
            //View.Animation.animateUnitPosition(graph, unit, node2);
            //Vector2 nodeVectorPixelCoordinates = (node2.getPosition());
            //Assert.AreEqual(unit.PixelCoordinates, nodeVectorPixelCoordinates);

            }

        [TestMethod]
        // Test unit returns to original location after attacking Right, Left, Up, and Down
        public void animate_ChangesToCorrectFrame_ShouldBehaveAsExpected()
        {
            Mock<Unit> mockUnit = new Mock<Unit>();
            Warrior unit = new Warrior(null, null, null, null, Vector2.Zero, null);

            // check animation for unit moving down
            unit.currentFrame = 0;                  // frame 1 of walking down
            View.Animation.animate(Direction.Down, unit);
            Assert.AreEqual(unit.currentFrame, 1);  // should be frame 2 of walking down

            unit.currentFrame = 1;                  // frame 2 of walking down
            View.Animation.animate(Direction.Down, unit);
            Assert.AreEqual(unit.currentFrame, 2);  // should be frame 3 of walking down

            unit.currentFrame = 2;                  // frame 3 of walking down
            View.Animation.animate(Direction.Down, unit);
            Assert.AreEqual(unit.currentFrame, 0);  // should be frame 1 of walking down

            // check animation for unit moving left
            unit.currentFrame = 3;                  // frame 1 of walking left
            View.Animation.animate(Direction.Left, unit);
            Assert.AreEqual(unit.currentFrame, 4);  // should be frame 2 of walking left

            unit.currentFrame = 4;                  // frame 2 of walking left
            View.Animation.animate(Direction.Left, unit);
            Assert.AreEqual(unit.currentFrame, 5);  // should be frame 3 of walking left

            unit.currentFrame = 5;                  // frame 3 of walking left
            View.Animation.animate(Direction.Left, unit);
            Assert.AreEqual(unit.currentFrame, 3);  // should be frame 1 of walking left

            // check animation for unit moving right
            unit.currentFrame = 6;                  // frame 1 of walking down
            View.Animation.animate(Direction.Right, unit);
            Assert.AreEqual(unit.currentFrame, 7);  // should be frame 2 of walking right

            unit.currentFrame = 7;                  // frame 2 of walking right
            View.Animation.animate(Direction.Right, unit);
            Assert.AreEqual(unit.currentFrame, 8);  // should be frame 3 of walking right

            unit.currentFrame = 8;                  // frame 3 of walking right
            View.Animation.animate(Direction.Right, unit);
            Assert.AreEqual(unit.currentFrame, 6);  // should be frame 1 of walking right

            // check animation for unit moving up
            unit.currentFrame = 9;                  // frame 1 of walking up
            View.Animation.animate(Direction.Up, unit);
            Assert.AreEqual(unit.currentFrame, 10); // should be frame 2 of walking up

            unit.currentFrame = 10;                 // frame 2 of walking up
            View.Animation.animate(Direction.Up, unit);
            Assert.AreEqual(unit.currentFrame, 11); // should be frame 3 of walking up

            unit.currentFrame = 11;                 // frame 3 of walking up
            View.Animation.animate(Direction.Up, unit);
            Assert.AreEqual(unit.currentFrame, 9); // should be frame 1 of walking up
        }
    }
}
