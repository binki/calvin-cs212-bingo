using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bingo
{
    /// <summary>
    /// Represents a directed labeled graph with a string name at each node
    /// and a string label for each edge.
    /// </summary>
    class RelationshipGraph
    {
        /*
         *  This data structure contains a list of nodes (each of which has
         *  an adjacency list) and a dictionary (hash table) for efficiently 
         *  finding nodes by name
         */
        List<GraphNode> nodes = new List<GraphNode>();

        /// <summary>
        ///   All the nodes in the graph.
        /// </summary>
        public IReadOnlyList<GraphNode> Nodes { get { return nodes; } }

        Dictionary<string, GraphNode> nodeDict = new Dictionary<string, GraphNode>();

        /// <summary>
        ///   Ensure a node with the given name exists, creating if necessary.
        /// </summary>
        public void AddNode(string name)
        {
            if (!nodeDict.ContainsKey(name))
            {
                GraphNode n = new GraphNode(name);
                nodes.Add(n);
                nodeDict.Add(name, n);
            }
        }

        /// <summary>
        ///   Adds the edge, creating endpoint nodes if necessary.
        ///   Edge is added to adjacency list of the from node.
        /// </summary>
        public void AddEdge(string fromName, string toName, string relationship)
        {
            // create the node if it doesn't already exist
            AddNode(fromName);
            // now fetch a reference to the node
            var fromNode = nodeDict[fromName];
            AddNode(toName);
            var toNode = nodeDict[toName];
            fromNode.AddIncidentEdge(new GraphEdge(fromNode, toNode, relationship));
        }

        /// <summary>
        ///   Get a node by name. O(1).
        /// </summary>
        /// <returns>node if it exists or null</returns>
        public GraphNode GetNode(string name)
        {
            if (nodeDict.ContainsKey(name))
                return nodeDict[name];
            else
                return null;
        }

        /// <summary>
        ///   Print a text representation graph.
        /// </summary>
        public void Dump()
        {
            foreach (GraphNode n in nodes)
            {
                Console.Write(n);
            }
        }
    }
}
