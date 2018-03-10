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
    public class Graph <T1, T2> where T1 : IComparable<T1>, IEquatable<T1> where T2 : IComparable<T2>, IEquatable<T2>
    {
        public Dictionary<MyNode<T1>, HashSet<MyNode<T1>>> adjacencyListNodes { get; private set; }
        public Dictionary<MyNode<T1>, HashSet<Edge<T2, T1>>> adjacencyListEdges { get; private set; }
        public HashSet<MyNode<T1>> nodesInGraph { get; private set; } 

        /// <summary>
        /// No argument constructor for a Graph
        /// </summary>
        public Graph()
        {
            adjacencyListNodes = new Dictionary<MyNode<T1>, HashSet<MyNode<T1>>>();
            adjacencyListEdges = new Dictionary<MyNode<T1>, HashSet<Edge<T2, T1>>>();
            nodesInGraph = new HashSet<MyNode<T1>>();
        }

        /// <summary>
        /// Creates a graph containing only the provided node
        /// </summary>
        /// <param name="baseNode"></param>
        public Graph(MyNode<T1> baseNode)
        {
            adjacencyListNodes = new Dictionary<MyNode<T1>, HashSet<MyNode<T1>>>();
            adjacencyListEdges = new Dictionary<MyNode<T1>, HashSet<Edge<T2, T1>>>();
            nodesInGraph = new HashSet<MyNode<T1>>();
            adjacencyListNodes.Add(baseNode, baseNode.NEIGHBORS);
            nodesInGraph.Add(baseNode);
        }

        /// <summary>
        /// Single argument constructor of dictionary of nodes, creates a graph off of a seed dictionary of nodes.
        /// Vertices are created from the node adjacency list.
        /// </summary>
        /// <param name="nodes"></param>
        public Graph(Dictionary<MyNode<T1>, HashSet<MyNode<T1>>> adjacentNodes)
        {
            adjacencyListNodes = adjacentNodes;
            adjacencyListEdges = new Dictionary<MyNode<T1>, HashSet<Edge<T2, T1>>>();
            nodesInGraph = new HashSet<MyNode<T1>>();
            foreach (MyNode<T1> node in adjacencyListNodes.Keys)
            {
                foreach(MyNode<T1> adjNode in node.NEIGHBORS)
                {
                    Edge<T2, T1> vert = new Edge<T2, T1>(node, adjNode);
                    adjacencyListEdges[node].Add(vert);
                }
                nodesInGraph.Add(node);
            }
        }

        /// <summary>
        /// Returns the hashset of nodes neighboring this one.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public HashSet<MyNode<T1>> GetNeighbors(MyNode<T1> node)
        {
            return adjacencyListNodes[node];
        }

        /// <summary>
        /// Returns the hashset of edges connecting this node to adjacent nodes.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public HashSet<Edge<T2, T1>> GetEdges(MyNode<T1> node)
        {
            return adjacencyListEdges[node];
        }

        /// <summary>
        /// Returns the value of this node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns>value of supplied node</returns>
        public T1 GetValue(MyNode<T1> node)
        {
            return node.VALUE;
        }

        /// <summary>
        /// add a given node to the graph, only returns true if the add was successful
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool AddNode(MyNode<T1> node)
        {
            if (!nodesInGraph.Contains(node))
            {
                adjacencyListNodes.Add(node, node.NEIGHBORS);
                nodesInGraph.Add(node);
                if(node.NEIGHBORS.Count > 0)
                {
                    foreach (MyNode<T1> _node in node.NEIGHBORS)
                    {
                        Edge<T2, T1> edgeOutOf = new Edge<T2, T1>(node, _node);
                        Edge<T2, T1> edgeInto = new Edge<T2, T1>(_node, node);
                        adjacencyListEdges[node].Add(edgeOutOf);
                        adjacencyListEdges[_node].Add(edgeInto);
                    }
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
        public bool DeleteNode(MyNode<T1> node)
        {
            if (nodesInGraph.Contains(node))
            {
                bool allRemovesResolvedPositively = true;
                if(node.NEIGHBORS.Count > 0)
                {
                    foreach (MyNode<T1> adjNode in node.NEIGHBORS)
                    {
                        foreach (Edge<T2, T1> edgeToBeRemoved in FindAllEdgesBetweenNodes(adjNode, node))
                        {
                            bool result1 = adjacencyListEdges[adjNode].Remove(edgeToBeRemoved); //remove all verts from adj nodes to this node first
                            if (!result1) { allRemovesResolvedPositively = false; } //consider returning a tuple that has a list of the edges that didn't remove correctly
                        }
                        bool result2 = adjacencyListNodes[adjNode].Remove(node); //remove node from all adjnodes adjlist
                        if (!result2) { allRemovesResolvedPositively = false; }
                    }
                }
                //remove all adj info for nodes and verts for node
                bool result3 = adjacencyListNodes.Remove(node);
                bool result4 = adjacencyListEdges.Remove(node);
                if (!result3 || !result4) { allRemovesResolvedPositively = false; }
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
        private HashSet<Edge<T2, T1>> FindAllEdgesBetweenNodes(MyNode<T1> node1, MyNode<T1> node2)
        {
            if(!nodesInGraph.Contains(node1)) { throw new NodeNotFoundException(String.Format("{0} doesn't exist in this graph", node1.ToString())); }
            if(!nodesInGraph.Contains(node2)) { throw new NodeNotFoundException(String.Format("{0} doesn't exist in this graph", node2.ToString())); }

            HashSet<Edge<T2, T1>> foundVerts = new HashSet<Edge<T2, T1>>();
            if (adjacencyListNodes[node1].Contains(node2)) //there is a connection from node1 to node2
            {
                foreach(Edge<T2,T1> foundVert in adjacencyListEdges[node1])
                {
                    if(foundVert.HasSameNodes_InOrder(new Edge<T2, T1>(node1, node2))) //verts from node1 to node2, node order matters
                    {
                        foundVerts.Add(foundVert);
                    }
                }
                //commenting this section out as node order matters now
                //foreach (Edge<T2, T1> foundVert in adjacencyListEdges[node2])
                //{
                //    if (foundVert.HasSameNodes(new Edge<T2, T1>(node2, node1))) //verts from node2 to node1
                //    {
                //        foundVerts.Add(foundVert);
                //    }
                //}
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
        public Tuple<int, List<Edge<T2, T1>>, List<MyNode<T1>>> DegreesOfSeperation(MyNode<T1> node1, MyNode<T1> node2)
        {
            if(!nodesInGraph.Contains(node1)) 
            {
                //TODO: When allowing for disconnected sections of the graph, update this error statement
                throw new NodeNotFoundException(String.Format("Node: {0} not found in graph", node1.ToString()));
            }
            else if(!nodesInGraph.Contains(node2))
            {
                //TODO: When allowing for disconnected sections of the graph, update this error statement
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
        private Tuple<int, List<Edge<T2, T1>>, List<MyNode<T1>>> MyDijkstras(MyNode<T1> node1, MyNode<T1> node2)
        {
            // TODO: Implement logic to handle disconnected graphs
            // TODO: Implement logic to handle weighted connections
            // TODO: Implement logic to handle directed connections
            int degreeOfSep = 0;
            List<MyNode<T1>> nodesOnPath = new List<MyNode<T1>>();
            List<Edge<T2, T1>> edgesOnPath = new List<Edge<T2, T1>>();

            Dictionary<MyNode<T1>, double> nodeDistances = new Dictionary<MyNode<T1>, double>();
            HashSet<MyNode<T1>> toBeVisited = new HashSet<MyNode<T1>>();
            foreach (MyNode<T1> node in nodesInGraph)
            {
                toBeVisited.Add(node);
                nodeDistances.Add(node, Int32.MaxValue);
            }
            nodeDistances[node1] = 0; //setting starting node distance as 0
            MyNode<T1> previous = node1;
            while (toBeVisited.Count > 0)
            {
                MyNode<T1> nearestNode = NodeWithShortestDistance(nodeDistances);
                toBeVisited.Remove(nearestNode); //progressing towards condition to exit while loop
                //nodesOnPath.Add(nearestNode);
                degreeOfSep++;
                if(!nearestNode.Equals(node2)) //nearest node isn't the one we're looking for
                {
                    nodesOnPath.Add(nearestNode); //add nearest node to path
                    foreach (Edge<T2, T1> edge in adjacencyListEdges[previous])
                    {
                        if(edge.Node2.Equals(nearestNode)) //found an edge from current node to nearestEdge, TODO: need to account for when there are mutliple edges between these two nodes
                        {
                            edgesOnPath.Add(edge);
                        }
                    }
                }
                if (nearestNode.Equals(node2)) //found the desired node
                {
                    //add nearest node on the path, as the terminal node
                    nodesOnPath.Add(nearestNode);
                    foreach (Edge<T2, T1> edge in adjacencyListEdges[previous])
                    {
                        if (edge.Node2.Equals(nearestNode)) //found an edge from current node to nearestEdge which in this case is the terminal node, TODO: need to account for when there are mutliple edges between these two nodes
                        {
                            edgesOnPath.Add(edge); // TODO: choose edge with smallest weight etc.
                        }
                        return Tuple.Create(degreeOfSep, edgesOnPath, nodesOnPath);
                    }
                }
                
                foreach (MyNode<T1> node in nearestNode.NEIGHBORS) //updating the distance for neighbors of found nearest node
                {
                    double temp = nodeDistances[nearestNode] + DistanceBetweenNodes(nearestNode, node, false);
                    if(temp < nodeDistances[node])
                    {
                        nodeDistances[node] = temp;
                    }
                }
                previous = nearestNode; //now we will progress from the currently found nearest node
            }
            return Tuple.Create(degreeOfSep, edgesOnPath, nodesOnPath); // TODO: investigate what this statement can return
        }

        private MyNode<T1> NodeWithShortestDistance(Dictionary<MyNode<T1>, double> dict)
        {
            double min = Int32.MaxValue;
            MyNode<T1> nearestNode = new MyNode<T1>();
            foreach(MyNode<T1> node in dict.Keys)
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
        /// TODO: test conditional logic to ensure there is a connection between these two nodes
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        /// <param name="weighted"></param>
        /// <returns></returns>
        private double DistanceBetweenNodes(MyNode<T1> node1, MyNode<T1> node2, bool weighted)
        {
            double distance = -1;
            if(!weighted) //edges aren't weighted, so default distance length for an edge is 1
            {
                foreach(Edge<T2, T1> edgeBetween in adjacencyListEdges[node1])
                {
                    if(edgeBetween.Node1.Equals(node1) && edgeBetween.Node2.Equals(node2)) //edge's nodes match in order
                    {
                        distance = 1;
                    }
                }
            }
            else
            {
                // TODO: test that this works correctly
                foreach (Edge<T2, T1> edgeBetween in adjacencyListEdges[node1])
                {
                    if (edgeBetween.Node1.Equals(node1) && edgeBetween.Node2.Equals(node2))
                    { 
                        distance = edgeBetween.numericalValue; //relies on the type of this edge having the ability to have a numerical value
                    }
                }
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
