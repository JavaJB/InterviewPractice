using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NodePractice;
using Vertices;

namespace GraphPractice
{
    /// <summary>
    /// Custom Graph class that is a "Node-First" graph.
    /// </summary>
    /// <typeparam name="T1">Node-type</typeparam>
    /// <typeparam name="T2">Vertex-type</typeparam>
    public class Graph <T1, T2> where T1 : IComparable<T1> where T2 : IComparable<T2> //TODO: Make Graph inherit IComparable<T>
    {
        public Dictionary<Node<T1>, HashSet<Node<T1>>> adjacencyListNodes { get; private set; }
        public Dictionary<Node<T1>, HashSet<Vertex<T2, T1>>> adjacencyListVertices { get; private set; }
        
        /// <summary>
        /// No argument constructor of a Graph, creates a Graph with a default size of 10 nodes
        /// </summary>
        public Graph()
        {
            adjacencyListNodes = new Dictionary<Node<T1>, HashSet<Node<T1>>>();
            adjacencyListVertices = new Dictionary<Node<T1>, HashSet<Vertex<T2, T1>>>();
        }

        /// <summary>
        /// Creates a graph containing only the provided node
        /// </summary>
        /// <param name="baseNode"></param>
        public Graph(Node<T1> baseNode)
        {
            adjacencyListNodes = new Dictionary<Node<T1>, HashSet<Node<T1>>>();
            adjacencyListVertices = new Dictionary<Node<T1>, HashSet<Vertex<T2, T1>>>();
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
            adjacencyListVertices = new Dictionary<Node<T1>, HashSet<Vertex<T2, T1>>>();
            foreach(Node<T1> node in adjacencyListNodes.Keys)
            {
                foreach(Node<T1> adjNode in node.NEIGHBORS)
                {
                    Vertex<T2, T1> vert = new Vertex<T2, T1>(node, adjNode);
                    adjacencyListVertices[node].Add(vert);
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
                    Vertex<T2, T1> newVert1 = new Vertex<T2, T1>(node, _node);
                    Vertex<T2, T1> newVert2 = new Vertex<T2, T1>(_node, node);
                    adjacencyListVertices[node].Add(newVert1);
                    adjacencyListVertices[_node].Add(newVert2);
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
                    foreach(Vertex<T2, T1> vertToBeRemoved in FindAllVerticesBetweenNodes(adjNode, node))
                    {
                        bool result1 = adjacencyListVertices[adjNode].Remove(vertToBeRemoved); //remove all verts from adj nodes to node
                        if(!result1) { allRemovesResolvedPositively = false; }
                    }
                    bool result2 = adjacencyListNodes[adjNode].Remove(node); //remove node from all adjnodes adjlist
                    if(!result2) { allRemovesResolvedPositively = false; }
                }
                //remove all adj info for nodes and verts for node
                bool result3 = adjacencyListNodes.Remove(node);
                bool result4 = adjacencyListVertices.Remove(node);
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
        private HashSet<Vertex<T2, T1>> FindAllVerticesBetweenNodes(Node<T1> node1, Node<T1> node2)
        {
            if(!adjacencyListNodes.ContainsKey(node1)) { throw new NodeNotFoundException(String.Format("{0} doesn't exist in this graph", node1.ToString())); }
            if (!adjacencyListNodes.ContainsKey(node2)) { throw new NodeNotFoundException(String.Format("{0} doesn't exist in this graph", node2.ToString())); }

            HashSet<Vertex<T2, T1>> foundVerts = new HashSet<Vertex<T2, T1>>();
            if (adjacencyListNodes[node1].Contains(node2)) //there is a connection from node1 to node2
            {
                foreach(Vertex<T2,T1> foundVert in adjacencyListVertices[node1])
                {
                    if(foundVert.HasSameNodes(new Vertex<T2, T1>(node1, node2))) //verts from node1 to node2
                    {
                        foundVerts.Add(foundVert);
                    }
                }
                foreach (Vertex<T2, T1> foundVert in adjacencyListVertices[node2])
                {
                    if (foundVert.HasSameNodes(new Vertex<T2, T1>(node2, node1))) //verts from node2 to node1
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
        /// Implements a DFS to establish connections between nodes.
        /// </summary>
        /// <param name="node1">Starting node</param>
        /// <param name="node2">Node we are "searching" for, method terminates once this node is located</param>
        /// <returns></returns>
        public Tuple<int, List<Vertex<T2, T1>>> DegreesOfSeperation(Node<T1> node1, Node<T1> node2)
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
                List<Vertex<T2,T1>> connectionList = new List<Vertex<T2,T1>>();
                int degreesOfSep = 0;
                return DepthFirstSearch(node1, node2, Tuple.Create(degreesOfSep, connectionList));
            }
        }
        
        /// <summary>
        /// Recursive implementation of DFS between 2 nodes
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <returns>A tuple containing the degrees of seperation of the two nodes and a detailing the connections between the two nodes.
        /// Returns an "empty" tuple (0, new List) if the connection wasn't found</returns>
        private Tuple<int, List<Vertex<T2, T1>>> DepthFirstSearch(Node<T1> node1, Node<T1> node2, Tuple<int, List<Vertex<T2,T1>>> connectionInfo)
        {
            // TODO: Implement logic to handle disconnected graphs
            // TODO: Implement logic to handle weighted connections
            // TODO: Implement logic to handle directed connections
            if (node1.Equals(node2)) //Base case
            {
                // TODO: Implement correct base case scenario
                return connectionInfo;
            }
            else 
            {
                node1.VISITED = true;
                if (node1.NEIGHBORS.Count > 0) //not a leaf/terminal node
                {
                    foreach (Node<T1> node in node1.NEIGHBORS.Keys)
                    {
                        if(!node.VISITED)
                        {
                            List<T2> connectionList = connectionInfo.Item2; //using the found matched node, pull the corresp. value as this is the connection for these two nodes
                            connectionList.Add(node1.NEIGHBORS[node]);
                            int degrees = connectionInfo.Item1 + 1;
                            Tuple<int, List<T2>> nextConnectionInfo = Tuple.Create(degrees, connectionList);
                            if (node.Equals(node2))
                            {
                                return nextConnectionInfo;
                            }
                            else
                            {
                                DepthFirstSearch(node, node2, nextConnectionInfo);
                            }
                        }
                    } 
                }
                else // leafy green node
                {
                    if (!node1.Equals(node2))
                    {
                        //goal is to back step a layer in the recursion
                        List<T2> connectionList = connectionInfo.Item2; // get connection list to redact connection between this level and prervious level
                        connectionList.Remove(); //essentially want to remove the connection we added in jumping into this level
                        int degrees = connectionInfo.Item1 - 1; // decrement degrees of sep
                        Tuple<int, List<T2>> nextConnectionInfo = Tuple.Create(degrees, connectionList); // then ensure the tuple we are passing back has the correct info
                        return connectionInfo; //exit this recursive call with the updated tuple
                    }
                    else //node 1 == node2, so we've mapped a connection between the two original nodes
                    {
                        return Tuple.Create(connectionInfo.Item1 + 1, connectionInfo.Item2);
                    }
                }
            }
            // TODO: Ensure logic is correct with final (currently) commented out return case of "empty" Tuple
            //return Tuple.Create(0, new List<T2>()); // Connection wasn't found between these two nodes in the graph, return an "empty" tuple.
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
