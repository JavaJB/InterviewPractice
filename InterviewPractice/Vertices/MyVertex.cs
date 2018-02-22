using System;
using System.Collections;
using System.Collections.Generic;
using NodePractice;

namespace Vertices
{
    /// <summary>
    /// Custom Vertex class for use in "Node-first" graphs, in which two nodes will be made before a connection (vertex)
    /// is made to connect them.
    /// </summary>
    /// <typeparam name="T1">Type for this vertex</typeparam>
    /// <typeparam name="T2">Type for the nodes this vertex connects</typeparam>
    public class Vertex<T1, T2> where T1: IComparable<T1> where T2: IComparable<T2> //TODO: make Vertex inherit IComparbale<T>
    {
        private T1 value;
        private bool directed; //for future feature addition?
        private bool weighted; //for future feature addition?
        public Node<T2> Node1 { get; private set; }
        public Node<T2> Node2 { get; private set; }

        public Vertex(Node<T2> _node1, Node<T2> _node2)
        {
            Node1 = _node1;
            Node2 = _node2;
        }
        public Vertex(T1 val, Node<T2> _node1, Node<T2> _node2)
        {
            value = val;
            Node1 = _node1;
            Node2 = _node2;
        }

        public bool HasSameNodes(Vertex<T1, T2> otherVertex)
        {
            //TODO: rewrite this method with stronger equality check between the checked nodes
            return (Node1.Equals(otherVertex) && (Node2.Equals(otherVertex.Node2)));
        }
    }
}
