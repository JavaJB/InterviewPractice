using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NodePractice;
using Edges;

namespace NodesTests
{
    [TestClass]
    public class MyNodeTestSuite
    {
        // TODO: Increase code coverage through mutliple tests for different scenarios for each tested method.
        [TestMethod]
        public void MyNode_Test_EmptyConstructor_()
        {
            MyNode<int> n1 = new MyNode<int>();
            Assert.IsFalse(n1.VISITED);
            Assert.IsTrue(n1.VALUE == 0);
            Assert.IsTrue(n1.NEIGHBORS.Count == 0);
        }

        [TestMethod]
        public void MyNode_Test_OnlyValueConstructor_()
        {
            MyNode<int> n1 = new MyNode<int>(7);
            Assert.IsFalse(n1.VISITED);
            Assert.IsTrue(n1.VALUE == 7);
            Assert.IsTrue(n1.NEIGHBORS.Count == 0);
        }

        [TestMethod]
        public void MyNode_Test_ValueNeighborsICollConstructor_()
        {
            MyNode<int> n1 = new MyNode<int>(7);
            MyNode<int> n2 = new MyNode<int>(21);
            MyNode<int> n3 = new MyNode<int>(3);
            HashSet<MyNode<int>> nodes = new HashSet<MyNode<int>>() { n1, n2, n3 };
            MyNode<int> n4 = new MyNode<int>(42, nodes); //TODO: FIX: This throws NullReferenceException, why??

            Assert.IsTrue(n4.VALUE == 42);
            Assert.IsFalse(n4.VISITED);
            Assert.IsTrue(n4.NEIGHBORS.Equals(nodes));
        }

        [TestMethod]
        public void MyNode_Test_ValueNeighborsISetConstructor_()
        {
            MyNode<int> n1 = new MyNode<int>(7);
            MyNode<int> n2 = new MyNode<int>(21);
            MyNode<int> n3 = new MyNode<int>(3);
            HashSet<MyNode<int>> nodes = new HashSet<MyNode<int>>(){ n1, n2, n3 };
            MyNode<int> n4 = new MyNode<int>(42, new HashSet<MyNode<int>>() { n1, n2, n3 });
            Assert.IsTrue(n4.VALUE == 42);
            Assert.IsFalse(n4.VISITED);
            Assert.IsTrue(n4.NEIGHBORS.Equals(nodes));
        }

        [TestMethod]
        public void MyNode_Test_ToString_()
        {
            MyNode<string> n1 = new MyNode<string>("32");
            Assert.AreEqual(n1.ToString(), String.Format("NodePractice.MyNode`1[{0}]: {1}", typeof(String), "32"));

            MyNode<int> n2 = new MyNode<int>(32);
            Assert.AreEqual(n1.ToString(), String.Format("NodePractice.MyNode`1[{0}]: {1}", typeof(String), 32));
        }

        [TestMethod]
        public void MyNode_Test_AddConnection_()
        {
            MyNode<int> n1 = new MyNode<int>(31);
            MyNode<int> n2 = new MyNode<int>(32);
            Assert.IsTrue(n1.AddConnection(n2));
            Assert.IsTrue(n1.NEIGHBORS.Contains(n2));
            Assert.IsFalse(n2.NEIGHBORS.Contains(n1));
            Assert.IsTrue(n2.AddConnection(n1));
            Assert.IsTrue(n1.NEIGHBORS.Contains(n2));
            Assert.IsTrue(n2.NEIGHBORS.Contains(n1));
        }

        [TestMethod]
        public void MyNode_Test_AddConnections_()
        {
            MyNode<int> n1 = new MyNode<int>(31);
            MyNode<int> n2 = new MyNode<int>(32);
            MyNode<int> n3 = new MyNode<int>(33);
            MyNode<int> n4 = new MyNode<int>(34);
            MyNode<int> n5 = new MyNode<int>(35);
            MyNode<int> n6 = new MyNode<int>(36);
            List<MyNode<int>> nodes = new List<MyNode<int>>() { n2, n3, n4, n5, n6 };
            Assert.IsTrue(n1.AddConnections(nodes));
            Assert.IsTrue(n1.NEIGHBORS.Contains(n2));
            Assert.IsFalse(n2.NEIGHBORS.Contains(n1));
            Assert.IsTrue(n1.NEIGHBORS.Contains(n3));
            Assert.IsFalse(n3.NEIGHBORS.Contains(n1));
            Assert.IsTrue(n1.NEIGHBORS.Contains(n4));
            Assert.IsFalse(n4.NEIGHBORS.Contains(n1));
            Assert.IsTrue(n1.NEIGHBORS.Contains(n5));
            Assert.IsFalse(n5.NEIGHBORS.Contains(n1));
            Assert.IsTrue(n1.NEIGHBORS.Contains(n6));
            Assert.IsFalse(n6.NEIGHBORS.Contains(n1));
        }

        [TestMethod]
        public void MyNode_Test_RemoveConnection_()
        {
            MyNode<int> n1 = new MyNode<int>(31);
            MyNode<int> n2 = new MyNode<int>(32);
            MyNode<int> n3 = new MyNode<int>(33);
            MyNode<int> n4 = new MyNode<int>(34);
            MyNode<int> n5 = new MyNode<int>(35);
            MyNode<int> n6 = new MyNode<int>(36);
            List<MyNode<int>> nodes = new List<MyNode<int>>() { n2, n3, n4, n5, n6 };
            n1.AddConnections(nodes);

            Assert.IsTrue(n1.RemoveConnection(n2));
            Assert.IsFalse(n1.NEIGHBORS.Contains(n2));
            Assert.IsTrue(n1.RemoveConnection(n3));
            Assert.IsFalse(n1.NEIGHBORS.Contains(n3));
            Assert.IsTrue(n1.RemoveConnection(n4));
            Assert.IsFalse(n1.NEIGHBORS.Contains(n4));
            Assert.IsTrue(n1.RemoveConnection(n5));
            Assert.IsFalse(n1.NEIGHBORS.Contains(n5));
            Assert.IsTrue(n1.RemoveConnection(n6));
            Assert.IsFalse(n1.NEIGHBORS.Contains(n6));
        }

        [TestMethod]
        public void MyNode_Test_RemoveConnections_()
        {
            MyNode<int> n1 = new MyNode<int>(31);
            MyNode<int> n2 = new MyNode<int>(32);
            MyNode<int> n3 = new MyNode<int>(33);
            MyNode<int> n4 = new MyNode<int>(34);
            MyNode<int> n5 = new MyNode<int>(35);
            MyNode<int> n6 = new MyNode<int>(36);
            List<MyNode<int>> nodes = new List<MyNode<int>>() { n2, n3, n4, n5, n6 };
            n1.AddConnections(nodes);

            List<MyNode<int>> subnodesEven = new List<MyNode<int>>() { n2, n4, n6 };
            Assert.IsTrue(n1.RemoveConnections(subnodesEven));
            Assert.IsFalse(n1.NEIGHBORS.Contains(n2));
            Assert.IsFalse(n1.NEIGHBORS.Contains(n4));
            Assert.IsFalse(n1.NEIGHBORS.Contains(n6));

            List<MyNode<int>> subnodesOdd = new List<MyNode<int>>() { n3, n5 };
            Assert.IsTrue(n1.RemoveConnections(subnodesOdd));
            Assert.IsFalse(n1.NEIGHBORS.Contains(n3));
            Assert.IsFalse(n1.NEIGHBORS.Contains(n5));
        }

        [TestMethod]
        public void MyNode_Test_RemoveAllNeighbors_()
        {
            MyNode<int> n1 = new MyNode<int>(31);
            MyNode<int> n2 = new MyNode<int>(32);
            MyNode<int> n3 = new MyNode<int>(33);
            MyNode<int> n4 = new MyNode<int>(34);
            MyNode<int> n5 = new MyNode<int>(35);
            MyNode<int> n6 = new MyNode<int>(36);
            List<MyNode<int>> nodes = new List<MyNode<int>>() { n2, n3, n4, n5, n6 };
            n1.AddConnections(nodes);

            n1.RemoveAllNeighbors();
            Assert.IsTrue(n1.NEIGHBORS.Count == 0);
        }

        //[TestMethod]
        //public void MyNode_Test_AddConnection_()
        //{
        //}

        //[TestMethod]
        //public void MyNode_Test_AddConnection_()
        //{
        //}

        //[TestMethod]
        //public void MyNode_Test_AddConnection_()
        //{
        //}

        //[TestMethod]
        //public void MyNode_Test_AddConnection_()
        //{
        //}

        //[TestMethod]
        //public void MyNode_Test_AddConnection_()
        //{
        //}

        //[TestMethod]
        //public void MyNode_Test_AddConnection_()
        //{
        //}
    }
}
