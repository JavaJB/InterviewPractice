using System;
using System.Collections;
using System.Collections.Generic;
using NodePractice;

namespace Edges
{
    /// <summary>
    /// Custom Vertex class for use in "Node-first" graphs, in which two nodes will be made before a connection (vertex)
    /// is made to connect them.
    /// </summary>
    /// <typeparam name="T1">Type for this vertex</typeparam>
    /// <typeparam name="T2">Type for the nodes this vertex connects</typeparam>
    public class Edge<T1, T2> : IComparable<Edge<T1, T2>>, IEquatable<Edge<T1, T2>> where T1: IComparable<T1>, IEquatable<T1> where T2: IComparable<T2>, IEquatable<T2>
    {
        public T1 value { get; private set; }
        public double numericalValue { get; private set; }
        private bool directed; //for future feature addition
        private bool weighted; //for future feature addition
        public MyNode<T2> Node1 { get; private set; } //Node 1 is considered the "start" node for any given connection. so an edge runs from node 1 to node 2
        public MyNode<T2> Node2 { get; private set; }

        /// <summary>
        /// Default constructor for an edge
        /// </summary>
        /// <param name="_node1"></param>
        /// <param name="_node2"></param>
        public Edge(MyNode<T2> _node1, MyNode<T2> _node2)
        {
            Node1 = _node1;
            Node2 = _node2;
            directed = false;
            weighted = false; //false is defaul value for bool, still setting them for clarity
            value = default(T1);
        }

        /// <summary>
        /// Constructor for an edge that takes in both nodes and an edge value
        /// </summary>
        /// <param name="val"></param>
        /// <param name="_node1"></param>
        /// <param name="_node2"></param>
        public Edge(T1 val, MyNode<T2> _node1, MyNode<T2> _node2)
        {
            value = val;
            Node1 = _node1;
            Node2 = _node2;
            directed = false;
            weighted = true; //values can be used for weights, not a neccesity though. depends on what type value is
        }

        /// <summary>
        /// Method for comparing this edge to "otherEdge"
        /// </summary>
        /// <param name="otherEdge"></param>
        /// <returns>True if nodes for each edge match up exactly (this.Node1 == other.Node1 etc.)</returns>
        public bool HasSameNodes_InOrder(Edge<T1, T2> otherEdge)
        {
            return (Node1.Equals(otherEdge.Node1) && (Node2.Equals(otherEdge.Node2)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="otherEdge"></param>
        /// <returns></returns>
        public bool HasSameNodes_NotInOrder(Edge<T1, T2> otherEdge)
        {
            return HasSameNodes_InOrder(otherEdge) || (Node1.Equals(otherEdge.Node2) && Node2.Equals(otherEdge.Node1));
        }

        /// <summary>
        /// Equals implementation for Edge
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if this edge's value, 1st, and 2nd nodes all equal the same fields for other edge</returns>
        public bool Equals(Edge<T1, T2> other)
        {
            return (value.Equals(other.value) && HasSameNodes_InOrder(other));
        }

        /// <summary>
        /// CompareTo for edges
        /// </summary>
        /// <param name="other"></param>
        /// <returns>CompareTo for values if both edges have same node, otherwise returns -2 (different nodes)</returns>
        public int CompareTo(Edge<T1, T2> other)
        {
            if(HasSameNodes_InOrder(other))
            {
                return value.CompareTo(other.value);
            }
            else
            {
                return -2;
            }
        }
    }
}
