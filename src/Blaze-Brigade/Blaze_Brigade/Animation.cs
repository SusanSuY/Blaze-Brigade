using Model;
using System.Threading;
using Microsoft.Xna.Framework;
using Model.UnitModule;
using Model.MapModule;

namespace View
{
    /// <summary>
    /// Static class containing all animation methods
    /// </summary>
    public class Animation
    {
        /**
        Animates attack of unit in specified direction. The animation consists of moving 10 pixels towards that direction, then moving back to original location. 
        Upon execution, gameState isAnimating is set to true, and false after it is finished. \n
            \b Exceptions: \n
            - The unit passed in must be alive, otherwise nothing will be shown to screen.
            - This function will not function properly if a direction other then Up, Down, Left or Right is given.
            \param direction The direction of the attack - it can be Up, Down, Left, or Right.
            \param unit The unit to be animated.
        */
        public static void attackAnimation(Direction direction, Unit unit)
        {
            GameState.isAnimating = true;
            float originalLocationX = unit.PixelCoordinates.X;
            float originalLocationY = unit.PixelCoordinates.Y;

            #region Attack Right
            if (direction == Direction.Right) //attack right
            {
                for (float i = originalLocationX; i <= originalLocationX + 8; i++)
                {
                    unit.PixelCoordinates = new Vector2(unit.PixelCoordinates.X + 1, unit.PixelCoordinates.Y);
                    Controller.Game.Instance.Tick();
                    Thread.Sleep(10);
                }

                for (float i = unit.PixelCoordinates.X; i > originalLocationX; i--)
                {
                    unit.PixelCoordinates = new Vector2(unit.PixelCoordinates.X - 1, unit.PixelCoordinates.Y);
                    Controller.Game.Instance.Tick();
                    Thread.Sleep(10);
                }
            }
            #endregion

            #region Attack Left
            else if (direction == Direction.Left) //attack left
            {
                for (float i = originalLocationX; i >= originalLocationX - 8; i--)
                {
                    unit.PixelCoordinates = new Vector2(unit.PixelCoordinates.X - 1, unit.PixelCoordinates.Y);
                    Controller.Game.Instance.Tick();
                    Thread.Sleep(10);
                }

                for (float i = unit.PixelCoordinates.X; i < originalLocationX; i++)
                {
                    unit.PixelCoordinates = new Vector2(unit.PixelCoordinates.X + 1, unit.PixelCoordinates.Y);
                    Controller.Game.Instance.Tick();
                    Thread.Sleep(10);
                }
            }
            #endregion

            #region Attack Down
            else if (direction == Direction.Down) //attack down
            {
                for (float i = originalLocationY; i <= originalLocationY + 8; i++)
                {
                    unit.PixelCoordinates = new Vector2(unit.PixelCoordinates.X, unit.PixelCoordinates.Y + 1);
                    Controller.Game.Instance.Tick();
                    Thread.Sleep(10);
                }

                for (float i = unit.PixelCoordinates.Y; i > originalLocationY; i--)
                {
                    unit.PixelCoordinates = new Vector2(unit.PixelCoordinates.X, unit.PixelCoordinates.Y - 1);
                    Controller.Game.Instance.Tick();
                    Thread.Sleep(10);
                }
            }
            #endregion

            #region Attack Up
            else if (direction == Direction.Up) //attack up
            {
                for (float i = originalLocationY; i >= originalLocationY - 8; i--)
                {
                    unit.PixelCoordinates = new Vector2(unit.PixelCoordinates.X, unit.PixelCoordinates.Y - 1);
                    Controller.Game.Instance.Tick();
                    Thread.Sleep(10);
                }

                for (float i = unit.PixelCoordinates.Y; i < originalLocationY; i++)
                {
                    unit.PixelCoordinates = new Vector2(unit.PixelCoordinates.X, unit.PixelCoordinates.Y + 1);
                    Controller.Game.Instance.Tick();
                    Thread.Sleep(10);
                }
            }
            #endregion

            GameState.isAnimating = false;
        }

        /**
        Animates unit movement from current unit location to future unit location. Unit moves through the inbetween nodes obtained from pathing algorith by first moving horizontally, then vertically.
        For horizontally, the unit will move right if the future position is greater in the x direction, otherwise it will move left. For vertical, the unit will move down if future y position is
        greater then current, otherwise it will move up. animate is called to animate the walking from node to node, and the direction parameter in that call is determined by which direction the 
        unit is moving. A tracker is kept for walking sounds, which counts up by 1 each time and plays a walking sound everytime it reaches 5. \n
            \b Exception: \n
            - Thie function will only execute if the original and new location are different, otherwise nothing will happen.
            - The function assumes that the the path between original location and new location is valid.
            \param graph The graph of the game map.
            \param unit The unit to be animated.
            \param node The node to move to.
        */
        public static void animateUnitPosition(Graph graph, Unit unit, Node node)
        {
            int nodePixelX = node.getPositionX() * 32;
            int nodePixelY = node.getPositionY() * 32;
            int pixelDifference = 4;
            graph.getNode(unit.Position).unitOnNode = (null);
            int walkSoundDelayCounter = 5;

            #region Horizontal Movement
            while (unit.PixelCoordinates.X != nodePixelX)
            {
                //keep a tracker so that walking sound only plays once every 5 steps (300millieseconds), otherwise sound is repeated too quickly
                if (walkSoundDelayCounter == 5)
                {
                    Sounds.walkingSound();
                    walkSoundDelayCounter = 0;
                }
                else
                {
                    walkSoundDelayCounter++;
                }

                if (unit.PixelCoordinates.X < nodePixelX)
                {
                    animate(Direction.Right, unit);
                    unit.PixelCoordinates = new Vector2(unit.PixelCoordinates.X + pixelDifference, unit.PixelCoordinates.Y);
                    Controller.Game.Instance.Tick();
                    Thread.Sleep(40);
                }
                else
                {
                    animate(Direction.Left, unit);
                    unit.PixelCoordinates = new Vector2(unit.PixelCoordinates.X - pixelDifference, unit.PixelCoordinates.Y);
                    Controller.Game.Instance.Tick();
                    Thread.Sleep(40);
                }
            }
            #endregion

            #region Vertical Movement
            while (unit.PixelCoordinates.Y != nodePixelY)
            {
                //keep a tracker so that walking sound only plays once every 5 steps (300millieseconds), otherwise sound is repeated too quickly
                if (walkSoundDelayCounter == 5)
                {
                    Sounds.walkingSound();
                    walkSoundDelayCounter = 0;
                }
                else
                {
                    walkSoundDelayCounter++;
                }

                if (unit.PixelCoordinates.Y < nodePixelY)
                {
                    animate(Direction.Down, unit);
                    unit.PixelCoordinates = new Vector2(unit.PixelCoordinates.X, unit.PixelCoordinates.Y + pixelDifference);
                    Controller.Game.Instance.Tick();
                    Thread.Sleep(40);
                }
                else
                {
                    animate(Direction.Up, unit);
                    unit.PixelCoordinates = new Vector2(unit.PixelCoordinates.X, unit.PixelCoordinates.Y - pixelDifference);
                    Controller.Game.Instance.Tick();
                    Thread.Sleep(40);
                }
            }
            #endregion
        }

        /**
        Animate sprite to "walk" in the specified direction. This is done by cycling through 3 frames. For each direction, there is a frame with right foot forward, one with both at neutral, 
        and one with left forward. The "walking" animation is done by cycling through the 3 frames. \n
        \b Exceptions: \n
            - The unit passed in must be alive, otherwise nothing will be shown to screen.
            - This function will not function properly if a direction other then Up, Down, Left or Right is given.
        * @param direction The direction the unit is moving in.
        */
        public static void animate(Direction direction, Unit unit)
        {

            #region Walking Down
            if (direction == Direction.Down) // for going down
            {
                if (unit.currentFrame > 2) // if unit isnt already going down, set the sprite to default facing downwards
                {
                    unit.currentFrame = 1;
                }
                else
                {
                    if (unit.currentFrame == 2) // if unit has reached end of going down walk cycle, go back to beginning
                    {
                        unit.currentFrame = 0;
                    }
                    else
                    {
                        unit.currentFrame = unit.currentFrame + 1; // increment through walk cycle
                    }
                }
            }
            #endregion

            #region Walking Left
            else if (direction == Direction.Left) // for going left
            {
                if ((unit.currentFrame < 3) || (unit.currentFrame > 5)) // if unit isnt already going left, set the sprite to default facing left
                {
                    unit.currentFrame = 4;
                }
                else
                {
                    if (unit.currentFrame == 5) // if unit has reached end of going down walk cycle, go back to beginning
                    {
                        unit.currentFrame = 3;
                    }
                    else
                    {
                        unit.currentFrame = unit.currentFrame + 1; // increment through walk cycle
                    }
                }
            }
            #endregion

            #region Walking Right
            else if (direction == Direction.Right) // for going right
            {
                if ((unit.currentFrame < 6) || (unit.currentFrame > 8)) // if unit isnt already going right, set the sprite to default facing right
                {
                    unit.currentFrame = 7;
                }
                else
                {
                    if (unit.currentFrame == 8)// if unit has reached end of going down walk cycle, go back to beginning
                    {
                        unit.currentFrame = 6;
                    }
                    else
                    {
                        unit.currentFrame = unit.currentFrame + 1; // increment through walk cycle
                    }
                }
            }
            #endregion

            #region Walking Up
            else if (direction == Direction.Up) // for going up
            {
                if (unit.currentFrame < 9) // if unit isnt already going up, set the sprite to default facing upwards
                {
                    unit.currentFrame = 10;
                }
                else
                {
                    if (unit.currentFrame == 11)// if unit has reached end of going down walk cycle, go back to beginning
                    {
                        unit.currentFrame = 9;
                    }
                    else
                    {
                        unit.currentFrame = unit.currentFrame + 1; // increment through walk cycle
                    }
                }
            }
            #endregion
        }
    }
}
