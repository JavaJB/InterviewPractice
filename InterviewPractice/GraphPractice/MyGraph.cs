using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NodePractice;
using Edges;

namespace GraphPractice
{
    /// <summary>
    /// Custom Graph class that is a "Node-First" graph.
    /// </summary>
    /// <typeparam name="T1">Node-type</typeparam>
    /// <typeparam name="T2">Edge-type</typeparam>
    public class Graph <T1, T2> where T1 : IComparable<T1> where T2 : IComparable<T2> //TODO: Make Graph inherit IComparable<T>
    {
        public Dictionary<Node<T1>, HashSet<Node<T1>>> adjacencyListNodes { get; private set; }
        public Dictionary<Node<T1>, HashSet<Edge<T2, T1>>> adjacencyListEdges { get; private set; }
        public HashSet<Node<T1>> nodesInGraph = new HashSet<Node<T1>>(); //TODO: change graph implementation to include nodesInGraph hashset
        
        /// <summary>
        /// No argument constructor of a Graph, creates a Graph with a default size of 10 nodes
        /// </summary>
        public Graph()
        {
            adjacencyListNodes = new Dictionary<Node<T1>, HashSet<Node<T1>>>();
            adjacencyListEdges = new Dictionary<Node<T1>, HashSet<Edge<T2, T1>>>();
        }

        /// <summary>
        /// Creates a graph containing only the provided node
        /// </summary>
        /// <param name="baseNode"></param>
        public Graph(Node<T1> baseNode)
        {
            adjacencyListNodes = new Dictionary<Node<T1>, HashSet<Node<T1>>>();
            adjacencyListEdges = new Dictionary<Node<T1>, HashSet<Edge<T2, T1>>>();
            adjacencyListNodes.Add(baseNode, baseNode.NEIGHBORS);
        }

        /// <summary>
        /// Single argument constructor of dictionary of nodes, creates a graph off of a seed dictionary of nodes.
        /// Vertices are created from the node adjacency list.
        /// </summary>
        /// <param name="nodes"></param>
        public Graph(Dictionary<Node<T1>, HashSet<Node<T1>>> adjacentNodes)
        {
            adjacencyListNodes = adjacentNodes;
            adjacencyListEdges = new Dictionary<Node<T1>, HashSet<Edge<T2, T1>>>();
            foreach(Node<T1> node in adjacencyListNodes.Keys)
            {
                foreach(Node<T1> adjNode in node.NEIGHBORS)
                {
                    Edge<T2, T1> vert = new Edge<T2, T1>(node, adjNode);
                    adjacencyListEdges[node].Add(vert);
                }
                
            }
        }

        /// <summary>
        /// Returns the hashset of nodes neighboring this one.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public HashSet<Node<T1>> GetNeighbors(Node<T1> node)
        {
            return adjacencyListNodes[node];
        }

        /// <summary>
        /// Returns the value of this node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns>value of supplied node</returns>
        public T1 GetValue(Node<T1> node)
        {
            return node.VALUE;
        }

        /// <summary>
        /// add a given node to the graph, only returns true if the add was successful
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool Add(Node<T1> node)
        {
            if (!adjacencyListNodes.ContainsKey(node))
            {
                adjacencyListNodes.Add(node, node.NEIGHBORS);
                foreach(Node<T1> _node in node.NEIGHBORS)
                {
                    Edge<T2, T1> newVert1 = new Edge<T2, T1>(node, _node);
                    Edge<T2, T1> newVert2 = new Edge<T2, T1>(_node, node);
                    adjacencyListEdges[node].Add(newVert1);
                    adjacencyListEdges[_node].Add(newVert2);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Delete a given node from the graph, if the graph contains the node before method call
        /// </summary>
        /// <param name="node"></param>
        /// <returns>True if the node existed in the graph and was subsequently deleted, false otherwise</returns>
        public bool Delete(Node<T1> node)
        {
            if (adjacencyListNodes.ContainsKey(node))
            {
                bool allRemovesResolvedPositively = true;
                foreach(Node<T1> adjNode in node.NEIGHBORS)
                {
                    foreach(Edge<T2, T1> vertToBeRemoved in FindAllVerticesBetweenNodes(adjNode, node))
                    {
                        bool result1 = adjacencyListEdges[adjNode].Remove(vertToBeRemoved); //remove all verts from adj nodes to node
                        if(!result1) { allRemovesResolvedPositively = false; }
                    }
                    bool result2 = adjacencyListNodes[adjNode].Remove(node); //remove node from all adjnodes adjlist
                    if(!result2) { allRemovesResolvedPositively = false; }
                }
                //remove all adj info for nodes and verts for node
                bool result3 = adjacencyListNodes.Remove(node);
                bool result4 = adjacencyListEdges.Remove(node);
                if(!result3 || !result4) { allRemovesResolvedPositively = false; }
                return allRemovesResolvedPositively;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Finds all vertices that connect node1 and node2
        /// </summary>
        /// <param name="node1">start node in connection</param>
        /// <param name="node2">end node in connection</param>
        /// <returns>HashSet containing all vertices that connect from node1 to node2 and vice versa</returns>
        private HashSet<Edge<T2, T1>> FindAllVerticesBetweenNodes(Node<T1> node1, Node<T1> node2)
        {
            if(!adjacencyListNodes.ContainsKey(node1)) { throw new NodeNotFoundException(String.Format("{0} doesn't exist in this graph", node1.ToString())); }
            if (!adjacencyListNodes.ContainsKey(node2)) { throw new NodeNotFoundException(String.Format("{0} doesn't exist in this graph", node2.ToString())); }

            HashSet<Edge<T2, T1>> foundVerts = new HashSet<Edge<T2, T1>>();
            if (adjacencyListNodes[node1].Contains(node2)) //there is a connection from node1 to node2
            {
                foreach(Edge<T2,T1> foundVert in adjacencyListEdges[node1])
                {
                    if(foundVert.HasSameNodes(new Edge<T2, T1>(node1, node2))) //verts from node1 to node2
                    {
                        foundVerts.Add(foundVert);
                    }
                }
                foreach (Edge<T2, T1> foundVert in adjacencyListEdges[node2])
                {
                    if (foundVert.HasSameNodes(new Edge<T2, T1>(node2, node1))) //verts from node2 to node1
                    {
                        foundVerts.Add(foundVert);
                    }
                }
            }
            return foundVerts;
        }

        /// <summary>
        /// Returns a Tuple containing the int count of degrees of seperation, and a list of type T
        /// that elaborates on the connection path between the two supplied nodes. 
        /// Implements Dijkstra's to establish connections between nodes.
        /// </summary>
        /// <param name="node1">Starting node</param>
        /// <param name="node2">Node we are "searching" for, method terminates once this node is located</param>
        /// <returns></returns>
        public Tuple<int, List<Edge<T2, T1>>, List<Node<T1>>> DegreesOfSeperation(Node<T1> node1, Node<T1> node2)
        {
            if(!adjacencyListNodes.ContainsKey(node1)) 
            {
                throw new NodeNotFoundException(String.Format("Node: {0} not found in graph", node1.ToString()));
            }
            else if(!adjacencyListNodes.ContainsKey(node2))
            {
                throw new NodeNotFoundException(String.Format("Node: {0} not found in graph", node2.ToString()));
            }
            else
            {
                return MyDijkstras(node1, node2);
            }
        }
        
        /// <summary>
        /// Implementation of Dijkstra's between 2 nodes
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <returns>A tuple containing the degrees of seperation of the two nodes and a detailing the connections between the two nodes.
        /// Returns an "empty" tuple (0, new List) if the connection wasn't found</returns>
        private Tuple<int, List<Edge<T2, T1>>, List<Node<T1>>> MyDijkstras(Node<T1> node1, Node<T1> node2)
        {
            // TODO: Implement logic to handle disconnected graphs
            // TODO: Implement logic to handle weighted connections
            // TODO: Implement logic to handle directed connections
            int degreeOfSep = 0;
            List<Node<T1>> nodesOnPath = new List<Node<T1>>();
            List<Edge<T2, T1>> edgesOnPath = new List<Edge<T2, T1>>();

            Dictionary<Node<T1>, int> nodeDistances = new Dictionary<Node<T1>, int>();
            HashSet<Node<T1>> toBeVisited = new HashSet<Node<T1>>();
            foreach (Node<T1> node in adjacencyListNodes.Keys)
            {
                toBeVisited.Add(node);
                nodeDistances.Add(node, Int32.MaxValue);
            }
            nodeDistances[node1] = 0;
            Node<T1> previous = node1;
            while (toBeVisited.Count > 0)
            {
                Node<T1> nearestNode = NodeWithShortestDistance(nodeDistances);
                toBeVisited.Remove(nearestNode);
                nodesOnPath.Add(nearestNode);
                degreeOfSep++;
                if(!nearestNode.Equals(node1))
                {
                    foreach(Edge<T2, T1> edge in adjacencyListEdges[previous])
                    {
                        if(edge.Node2.Equals(nearestNode))
                        {
                            edgesOnPath.Add(edge);
                        }
                    }
                }
                
                foreach (Node<T1> node in nearestNode.NEIGHBORS)
                {
                    int temp = nodeDistances[nearestNode] + DistanceBetweenNodes(nearestNode, node, false);
                    if(temp < nodeDistances[node])
                    {
                        nodeDistances[node] = temp;
                    }
                }
                previous = nearestNode;
            }
            return Tuple.Create(degreeOfSep, edgesOnPath, nodesOnPath);
        }

        private Node<T1> NodeWithShortestDistance(Dictionary<Node<T1>, int> dict)
        {
            int min = Int32.MaxValue;
            Node<T1> nearestNode = new Node<T1>();
            foreach(Node<T1> node in dict.Keys)
            {
                if (dict[node] < min)
                {
                    min = dict[node];
                    nearestNode = node;
                }
            }
            return nearestNode;
        }

        /// <summary>
        /// Helper method for finding distance between two adjacent nodes
        /// TODO: add conditional logic to ensure there is a connection between these two nodes
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <param name="weighted"></param>
        /// <returns></returns>
        private int DistanceBetweenNodes(Node<T1> node1, Node<T1> node2, bool weighted)
        {
            int distance = 0;
            if(!weighted)
            {
                distance = 1;
            }
            else
            {
                throw new NotImplementedException("Weighted graph not implemented yet.");
            }
            return distance;
        }
    }

    public class NodeNotFoundException : Exception
    {
        public NodeNotFoundException() : base()
        {
        }

        public NodeNotFoundException(string message) : base(message)
        {   
        }

        public NodeNotFoundException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
