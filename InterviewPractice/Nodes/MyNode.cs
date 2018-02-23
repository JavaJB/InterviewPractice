using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace NodePractice
{
    /// <summary>
    /// Custom Node class for use in "Node-First" graphs, in which we create two nodes before creating a connection between them
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class MyNode<T> : IComparable<MyNode<T>>, IEquatable<MyNode<T>> where T : IComparable<T>, IEquatable<T>
    {   //TODO: Implement try-catch logic throughout class to utilize NodeConstructionExceptions
        public T VALUE { get; private set; }
        public HashSet<MyNode<T>> NEIGHBORS { get; private set; }
        public bool VISITED { get; set; } // any possible security issues from allowing public setting?

        /// <summary>
        /// no parameter constructor
        /// </summary>
        public MyNode()
        {
            NEIGHBORS = new HashSet<MyNode<T>>();
            VISITED = false;
        }
        /// <summary>
        /// Basic constructor takes in a generic value to define this node.
        /// For the case when you simply want to create a node with a value.
        /// </summary>
        /// <param name="val"></param>
        public MyNode(T val)
        {
            VALUE = val;
            NEIGHBORS = new HashSet<MyNode<T>>();
            VISITED = false;
        }

        /// <summary>
        /// Creates a node with the user defined value, user defined IColl<T> of neighbors.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="neighbors"></param>
        public MyNode(T val, ICollection<MyNode<T>> neighbors)
        {
            VALUE = val;
            foreach (MyNode<T> node in neighbors) { NEIGHBORS.Add(node); }
            VISITED = false;
        }

        /// <summary>
        /// Takes in a value and set of neigboring nodes
        /// </summary>
        /// <param name="val"></param>
        /// <param name="neighborNodes"></param>
        public MyNode(T val, ISet<MyNode<T>> neighborNodes)
        {
            VALUE = val;
            foreach (MyNode<T> node in neighborNodes) { NEIGHBORS.Add(node); }
            VISITED = false;
        }

        public MyNode(T val, IEnumerable<MyNode<T>> neighborNodes)
        {
            VALUE = val;
            foreach (MyNode<T> node in neighborNodes) { NEIGHBORS.Add(node); }
            VISITED = false;
        }

        /// <summary>
        /// Overriding default ToString().
        /// </summary>
        /// <returns>default ToString() plus node.VALUE.ToString()</returns>
        public override string ToString()
        {
            return base.ToString() + ": " + VALUE.ToString();
        }

        /// <summary>
        /// Adds a single node to this node's neighbors
        /// </summary>
        /// <param name="nodeToBeConnectedWith"></param>
        /// <returns>True: addition was succesful; False: addition was not successful</returns>
        public bool AddConnection(MyNode<T> nodeToBeConnectedWith)
        {
            return NEIGHBORS.Add(nodeToBeConnectedWith);
        }

        /// <summary>
        /// Adds a list of nodes to this node's neighbors
        /// </summary>
        /// <param name="nodesToBeConnectedWith"></param>
        /// <returns>True: all nodes were added succesfully; False: at least one node wasn't added succesfully</returns>
        public bool AddConnections(ICollection<MyNode<T>> nodesToBeConnectedWith)
        {
            bool allAddsWentThrough = true;
            foreach(MyNode<T> node in nodesToBeConnectedWith) { if (!NEIGHBORS.Add(node)) { allAddsWentThrough = false; } }
            return allAddsWentThrough;
        }

        public bool AddConnections(IEnumerable<MyNode<T>> nodesToBeConnectedWith)
        {
            bool allAddsWentThrough = true;
            foreach (MyNode<T> node in nodesToBeConnectedWith) { if (!NEIGHBORS.Add(node)) { allAddsWentThrough = false; } }
            return allAddsWentThrough;
        }

        /// <summary>
        /// Removes a node from this node's Neighbors 
        /// </summary>
        /// <param name="nodeToBeDisconnected"></param>
        /// <returns></returns>
        public bool RemoveConnection(MyNode<T> nodeToBeDisconnected)
        {
            return NEIGHBORS.Remove(nodeToBeDisconnected);
        }

        /// <summary>
        /// Removes a list of nodes from this nodes Neighbors
        /// </summary>
        /// <param name="nodesToBeDisconnected"></param>
        /// <returns></returns>
        public bool RemoveConnections(ICollection<MyNode<T>> nodesToBeDisconnected)
        {
            bool allRemovalsSuccesfull = true;
            foreach(MyNode<T> node in nodesToBeDisconnected) { if (!NEIGHBORS.Remove(node)) { allRemovalsSuccesfull = false; } }
            return allRemovalsSuccesfull;
        }

        public bool RemoveConnections(IEnumerable<MyNode<T>> nodesToBeDisconnected)
        {
            bool allRemovalsSuccesfull = true;
            foreach (MyNode<T> node in nodesToBeDisconnected) { if (!NEIGHBORS.Remove(node)) { allRemovalsSuccesfull = false; } }
            return allRemovalsSuccesfull;
        }

        /// <summary>
        /// Removes ALL nodes from this node's Neighbors set.
        /// WARNING! should only be used by encompassing Graph
        /// WARNING! If edge connections aren't updated this will create issues in encompassing Graph
        /// </summary>
        public void RemoveAllNeighbors()
        {
            NEIGHBORS = new HashSet<MyNode<T>>();
        }

        /// <summary>
        /// Sum of CompareTo methods for VALUE and NEIGHBORS.Count.
        /// Useful for quick comparison of nodes
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(MyNode<T> other)
        {
            return VALUE.CompareTo(other.VALUE) + NEIGHBORS.Count.CompareTo(other.NEIGHBORS.Count);
        }

        /// <summary>
        /// Creates a Tuple of two ints that are the results of the comparison of the VALUE and NEIGHBOR.Count values
        /// for both nodes. Allows for more informed analysis of two nodes than vanilla CompareTo.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>VALUE.CompareTo(other.Value), NEIGHBORS.Count.CompareTo(other.NEIGHBORS.Count)</returns>
        public Tuple<int, int> ExpandedCompareTo(MyNode<T> other)
        {
            return Tuple.Create(VALUE.CompareTo(other.VALUE), NEIGHBORS.Count.CompareTo(other.NEIGHBORS.Count));
        }

        /// <summary>
        /// Checks all node values for equality
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(MyNode<T> other)
        {
            return (VALUE.Equals(other.VALUE) && VISITED.Equals(other.VISITED) && NEIGHBORS.Equals(other.NEIGHBORS));
        }
    }


    /// <summary>
    /// Custom exception created for constructing nodes
    /// </summary>
    public class NodeConstructionException: Exception
    {
        public NodeConstructionException() : base()
        {

        }

        public NodeConstructionException(string message) : base(message)
        {

        } 

        public NodeConstructionException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
