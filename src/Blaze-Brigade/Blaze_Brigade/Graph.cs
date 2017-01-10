using System;
using Microsoft.Xna.Framework;

namespace Model
{
    /// <summary>
    /// The Map Module, containing all classes related to setting up the working game map.
    /// </summary>
    namespace MapModule
    {
        /// <summary>
        /// Structure that represents the game map.
        /// </summary>

        /**
        * Programmatical representation of the map grid.
        * Composed of Nodes that represent each tile on the grid.
        */
        public class Graph
        {
            private Node[,] nodes;

            /**
            Creates a graph for each tile in the game, using the passed in parameter Width and Height
                \param x Width of the graph.
                \param y Height of the graph.
            */
            public Graph(int x, int y)
            {
                Width = x;
                Height = y;
                NumberOfNodes = x * y;

                // initialize nodes inside the graph
                nodes = new Node[x, y];
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < y; j++)
                    {
                        nodes[i, j] = new Node(i, j);
                    }
                }
            }

            /**
            Returns the total number of nodes in the graph.
            */
            public int NumberOfNodes { get; }
            /**
            Returns the width of the graph (by number of nodes).
            */
            public int Width { get; }
            /**
            Returns the height of the graph (by number of nodes).
            */
            public int Height { get; }

            /**
            Returns the node on the graph at the specified position (by nodes).
                \param x X position of the node (by nodes).
                \param y Y position of the node (by nodes).
            */
            public Node getNode(int x, int y)
            {
                return nodes[x, y];
            }

            /**
            Returns the node on the graph at the specified position (by pixel coordinates).
               \param pixelCoordinates Pixel coordinates of the node, which contains the X coordinate and Y coordinate.
           */
            public Node getNode(Vector2 pixelCoordinates)
            {
                int x = (int)Math.Floor(pixelCoordinates.X / 32);
                int y = (int)Math.Floor(pixelCoordinates.Y / 32);
                return nodes[x, y];
            }

            /**
            Returns the node on the graph at the specified position (by nodes).
               \param position Position of the node, which contains the X position and Y position (by nodes).
           */
            public Node getNode(Tuple<int, int> position)
            {
                return nodes[position.Item1, position.Item2];
            }

            /**
            Sets the node at the specified position on the graph to the specified node.
               \param node Node to set.
               \param x X position of the node (by nodes).
               \param y Y position of the node (by nodes).
           */
            public void setNode(Node node, int x, int y)
            {
                nodes[x, y] = node;
            }
        }
    }
}
