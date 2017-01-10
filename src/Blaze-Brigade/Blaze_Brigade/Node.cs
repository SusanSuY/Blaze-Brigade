using Microsoft.Xna.Framework;
using Model.UnitModule;

namespace Model
{
    namespace MapModule
    {
        /// <summary>
        /// Structure that represents a tile on the game map grid.
        /// </summary>

        /**
        * Programmatical representation of a tile on the map grid.
        * Holds information pertaining to the tile.
        */
        public class Node
        {
            /**
            Creates a node at position x,y, with default set to not being an obstacle or movabilityObstruction.
                \param x X position of the node on the graph (by node).
                \param y Y position of the node on the graph (by node).
            */
            public Node(int x, int y)
            {
                positionX = x;
                positionY = y;
                movabilityObstruction = 0;      // defaults to no hindrance in unit movement
                isObstacle = false;             // default to non-obstacle tile
            }
            /**
            Index for hindrance of the movability of a unit.
                The higher the index, the less a unit can move through the tile.
                Set to 0 by default (no hindrance).
            */
            public int movabilityObstruction { get; set; } // TODO
                                                           /**
                                                           Indicates whether a unit can stand inside the tile.
                                                           */
            public bool isObstacle { get; set; }
            /**
            Gets and sets the unit that is on the node.
            */
            public Unit unitOnNode { get; set; }
            /**
            X position of the node on the grid.
            */
            private int positionX;
            /**
            Y position of the node on the grid.
            */
            private int positionY;
            /**
            Returns the Vector position of the node on the graph.
            */
            public Vector2 getPosition()
            {
                Vector2 position = new Vector2(getPositionX() * 32, getPositionY() * 32);
                return position;
            }

            /**
            Returns the X position of the node on the graph.
            */
            public int getPositionX()
            {
                return positionX;
            }

            /**
            Returns the Y position of the node on the graph.
            */
            public int getPositionY()
            {
                return positionY;
            }

            /**
            Indicates whether the node is occupied by a unit.
            */
            public bool isOccupied()
            {
                if (unitOnNode != null)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
