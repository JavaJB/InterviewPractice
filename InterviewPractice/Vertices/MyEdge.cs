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
        private bool directed; //for future feature addition
        private bool weighted; //for future feature addition
        public MyNode<T2> Node1 { get; private set; }
        public MyNode<T2> Node2 { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_node1"></param>
        /// <param name="_node2"></param>
        public Edge(MyNode<T2> _node1, MyNode<T2> _node2)
        {
            Node1 = _node1;
            Node2 = _node2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <param name="_node1"></param>
        /// <param name="_node2"></param>
        public Edge(T1 val, MyNode<T2> _node1, MyNode<T2> _node2)
        {
            value = val;
            Node1 = _node1;
            Node2 = _node2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="otherEdge"></param>
        /// <returns></returns>
        public bool HasSameNodes(Edge<T1, T2> otherEdge)
        {
            return (Node1.Equals(otherEdge.Node1) && (Node2.Equals(otherEdge.Node2)) && value.Equals(otherEdge.value));
        }

        /// <summary>
        /// Equals implementation for Edge
        /// </summary>
        /// <param name="other"></param>
        /// <returns>true if this edge's value, 1st, and 2nd nodes all equal the same fields for other edge</returns>
        public bool Equals(Edge<T1, T2> other)
        {
            return (value.Equals(other.value) && Node1.Equals(other.Node1) && Node2.Equals(other.Node2));
        }

        /// <summary>
        /// CompareTo for edges
        /// </summary>
        /// <param name="other"></param>
        /// <returns>CompareTo for values if both edges have same node, otherwise returns -2 (different nodes)</returns>
        public int CompareTo(Edge<T1, T2> other)
        {
            if(HasSameNodes(other))
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
