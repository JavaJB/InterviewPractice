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
    public class Node<T> where T : IComparable<T> //TODO: Make Node inherit IComparable<T>
    {
        public T VALUE { get; private set; }
        public HashSet<Node<T>> NEIGHBORS { get; private set; }
        public bool VISITED { get; set; } // any possible security issues from allowing public setting?

        /// <summary>
        /// Basic constructor takes in a generic value to define this node.
        /// For the case when you simply want to create a node with a value.
        /// </summary>
        /// <param name="val"></param>
        public Node(T val)
        {
            VALUE = val;
            //NEIGHBORSWITHCONNECTIONS = new Dictionary<Node<T1, T2>, Vertex<T2>>();
            NEIGHBORS = new HashSet<Node<T>>();
            //CONNECTIONS = new HashSet<Vertex<T2>>(){ new Vertex<T2>(vertex) };
        }

        /// <summary>
        /// Creates a node with the supplied value, establishes supplied list of neighbors and connections.
        /// Assumes that for each index i, the corresp node and connection in neighbors and connections match.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="neighbors"></param>
        /// <param name="connections"></param>
        public Node(T val, ICollection<Node<T>> neighbors)
        {
            VALUE = val;
            List<Node<T>> neighs = neighbors.ToList();
            for (int i = 0; i < neighbors.Count; i++)
            {
                NEIGHBORS.Add(neighs[i]);
            }
        }

        /// <summary>
        /// Takes in a value and collection of neigboring nodes
        /// </summary>
        /// <param name="val"></param>
        /// <param name="neighborNodes"></param>
        public Node(T val, Collection<Node<T>> neighborNodes)
        {
            VALUE = val;
            foreach (Node<T> node in neighborNodes)
            {
                NEIGHBORS.Add(node);
            }
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
        public bool AddConnection(Node<T> nodeToBeConnectedWith)
        {
            return NEIGHBORS.Add(nodeToBeConnectedWith);
        }

        /// <summary>
        /// Adds a list of nodes to this node's neighbors
        /// </summary>
        /// <param name="nodesToBeConnectedWith"></param>
        /// <returns>True: all nodes were added succesfully; False: at least one node wasn't added succesfully</returns>
        public bool AddConnections(Collection<Node<T>> nodesToBeConnectedWith)
        {
            bool allAddsWentThrough = true;
            foreach(Node<T> node in nodesToBeConnectedWith) { if (!NEIGHBORS.Add(node)) { allAddsWentThrough = false; } }
            return allAddsWentThrough;
        }

        /// <summary>
        /// Removes a node from this node's Neighbors 
        /// </summary>
        /// <param name="nodeToBeDisconnected"></param>
        /// <returns></returns>
        public bool RemoveConnection(Node<T> nodeToBeDisconnected)
        {
            return NEIGHBORS.Remove(nodeToBeDisconnected);
        }

        /// <summary>
        /// Removes a list of nodes from this nodes Neighbors
        /// </summary>
        /// <param name="nodesToBeDisconnected"></param>
        /// <returns></returns>
        public bool RemoveConnections(Collection<Node<T>> nodesToBeDisconnected)
        {
            bool allRemovalsSuccesfull = true;
            foreach(Node<T> node in nodesToBeDisconnected) { if (!NEIGHBORS.Remove(node)) { allRemovalsSuccesfull = false; } }
            return allRemovalsSuccesfull;
        }

        /// <summary>
        /// Removes ALL nodes from this node's Neighbors
        /// </summary>
        public void RemoveAllNeighbors()
        {
            NEIGHBORS = new HashSet<Node<T>>();
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
