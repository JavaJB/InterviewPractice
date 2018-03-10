using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphPractice;
using NodePractice;
using System.Collections.Generic;

namespace GraphsTests
{
    [TestClass]
    public class MyGraphTestSuite
    {
        //TODO: Look into test set up procedures to make a few graphs with specific test conditions in mind so that I don't have to keep copypastang the same code over and over jfc

        [TestMethod]
        public void Graph_Test_DefaultConstructor()
        {
            Graph<int, int> graph = new Graph<int, int>();
            Assert.AreEqual(graph.adjacencyListEdges.Count, 0);
            Assert.AreEqual(graph.adjacencyListNodes.Count, 0);
            Assert.AreEqual(graph.nodesInGraph.Count, 0);
        }

        [TestMethod]
        public void Graph_Test_BaseNodeConstructorNoNeighbors()
        {
            MyNode<string> node = new MyNode<string>("7");
            Graph<string, int> graph = new Graph<string, int>(node);

            Assert.AreEqual(graph.nodesInGraph.Count, 1);
            Assert.IsTrue(graph.nodesInGraph.Contains(node));

            Assert.AreEqual(graph.adjacencyListNodes.Keys.Count, 1);
            Assert.IsTrue(graph.adjacencyListNodes.ContainsKey(node));
            Assert.AreEqual(graph.adjacencyListNodes[node].Count, 0);

            Assert.AreEqual(graph.adjacencyListEdges.Keys.Count, 1);
            Assert.IsTrue(graph.adjacencyListEdges.ContainsKey(node));
            Assert.AreEqual(graph.adjacencyListEdges[node].Count, 0);
        }

        [TestMethod]
        public void Graph_Test_BaseNodeConstructorWithNeighbors()
        {
            //TODO: Implement recursive test
            MyNode<string> node = new MyNode<string>("1");
            HashSet<MyNode<string>> nodesAdded = new HashSet<MyNode<string>>() { node };

            //Making a small, multi-layer network of nodes
            for(int i = 2; i < 6; i++)
            {
                MyNode<string> newnode = new MyNode<string>(i.ToString());
                node.NEIGHBORS.Add(newnode);
                nodesAdded.Add(newnode);

                for (int ii = 0; ii < 2; ii++)
                {
                    MyNode<string> secondLayerNewnode = new MyNode<string>(((i*5)+ii).ToString());
                    newnode.NEIGHBORS.Add(secondLayerNewnode);
                    nodesAdded.Add(secondLayerNewnode);
                }
            }

            Graph<string, int> graph = new Graph<string, int>(node);

            // Ensuring the correct number of nodes exist in the graph
            Assert.AreEqual(graph.nodesInGraph.Count, 13); 
            // Ensuring the nodes in the graph are the correct nodes
            foreach(MyNode<string> addedNode in nodesAdded) { Assert.IsTrue(graph.nodesInGraph.Contains(addedNode)); }

            // Ensuring the correct number of nodes are keys in the adjlist for nodes
            Assert.AreEqual(graph.adjacencyListNodes.Keys.Count, 13);
            // Ensuring each node in the graph has at least one other node in its adjnode list as this is how I built the gemo graph
            foreach (MyNode<string> addedNode in nodesAdded) { Assert.IsTrue(graph.adjacencyListNodes[addedNode].Count > 0); } //not a great test

            // Ensuring correct number of nodes are keys in the adjEdge list
            Assert.AreEqual(graph.adjacencyListEdges.Keys.Count, 13);

            //TODO: add in test logic ensuring all edges have the correct node flow/set up, i'm being lazy right now
        }

        [TestMethod]
        public void Graph_Test_AdjNodeDictionaryConstructor()
        {
            // TODO: flesh this test out, same as above test
            MyNode<string> node = new MyNode<string>("1");
            HashSet<MyNode<string>> nodesAdded = new HashSet<MyNode<string>>() { node };

            //Making a small, multi-layer network of nodes
            for (int i = 2; i < 6; i++)
            {
                MyNode<string> newnode = new MyNode<string>(i.ToString());
                node.NEIGHBORS.Add(newnode);
                nodesAdded.Add(newnode);

                for (int ii = 0; ii < 2; ii++)
                {
                    MyNode<string> secondLayerNewnode = new MyNode<string>(((i * 5) + ii).ToString());
                    newnode.NEIGHBORS.Add(secondLayerNewnode);
                    nodesAdded.Add(secondLayerNewnode);
                }
            }
        }

        [TestMethod]
        public void Graph_Test_GetNeighbors()
        {
            //TODO: Implement GetNeighbors Tests
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Graph_Test_GetEdges()
        {
            //TODO: Implement GetEdges Tests
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Graph_Test_GetValueOfNode()
        {
            //TODO: Implement GetValue Tests
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Graph_Test_AddNode()
        {
            //TODO: Implement AddNode Tests
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Graph_Test_FindAllEdgesBetweenNodes()
        {
            //TODO: Implement FindAllEdgesBetweenNodes Tests
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Graph_Test_DeleteNode()
        {
            //TODO: Implement DeleteNode Tests
            throw new NotImplementedException();
        }

        //[TestMethod]
        //public void TestMethod1()
        //{
        //    //TODO: Implement Test
        //    MyNode<string> node = new MyNode<string>("7");
        //    Graph<string, int> graph = new Graph<string, int>();
        //    throw new NotImplementedException();
        //}

        //[TestMethod]
        //public void TestMethod1()
        //{
        //    //TODO: Implement Test
        //    MyNode<string> node = new MyNode<string>("7");
        //    Graph<string, int> graph = new Graph<string, int>();
        //    throw new NotImplementedException();
        //}
    }
}
