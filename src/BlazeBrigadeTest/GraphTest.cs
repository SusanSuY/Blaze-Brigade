using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Model;
using Model.MapModule;
using System;

namespace ModelTest
{
    namespace MapModuleTest
    {
        [TestClass]
        public class GraphTest
        {
            [TestMethod]
            public void Graph_Constructor_ShouldWorkAsExpected()
            {
                Graph graph = new Graph(50, 32);

                Assert.AreEqual(50, graph.Width);
                Assert.AreEqual(32, graph.Height);
                Assert.AreEqual(1600, graph.NumberOfNodes);
            }

            [TestMethod]
            public void Graph_Constructor_NegativeNumbers_ShouldThrowOverflowException()
            {
                try
                {
                    Graph graph = new Graph(-50, -32);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(ex is System.OverflowException);
                }
            }

            [TestMethod]
            public void Graph_Constructor_ZeroNumbers_ShouldWorkAsExpected()
            {
                Graph graph = new Graph(0, 0);

                Assert.AreEqual(0, graph.Width);
                Assert.AreEqual(0, graph.Height);
                Assert.AreEqual(0, graph.NumberOfNodes);
            }

            [TestMethod]
            public void getSetNode_ShouldReturnExpected()
            {
                Graph graph = new Graph(10, 10);    // initialize graph

                // check node with proper (x, y)
                Node node = new Node(5, 5);
                graph.setNode(node, 5, 5);      // setting node with matching (x, y) == (5, 5)
                Assert.ReferenceEquals(node, graph.getNode(new Vector2(5, 5)));     // check getNode that takes in Vector2
                Assert.ReferenceEquals(node, graph.getNode(new Tuple<int, int>(5, 5))); // check getNode that takes in Tuple<int, int>

                // check node with improper (x, y)
                Node improperNode = new Node(0, 0);
                graph.setNode(improperNode, 5, 5);  // setting node with (0, 0) into (5, 5) position on graph (improper)
                Assert.ReferenceEquals(improperNode, graph.getNode(new Vector2(5, 5)));
                Assert.ReferenceEquals(improperNode, graph.getNode(new Tuple<int, int>(5, 5)));
            }
        }
    }
}