using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bingo
{
    /// <summary>
    /// Represents a node in a RelationshipGraph
    /// </summary>
    class GraphNode
    {
        /// <summary>
        ///   The node’s name.
        /// </summary>
        public string Name { get; }

        readonly List<GraphEdge> edges;
        public IReadOnlyList<GraphEdge> Edges { get { return edges; } }

        public GraphNode(string name)
        {
            Name = name;
            edges = new List<GraphEdge>();
        }

	/// <summary>
        ///   Add an edge if its equivalent does not already exist.
        /// </summary>
        public void AddIncidentEdge(GraphEdge e)
        {
            // We silently ignore duplicate edges.
            if (Edges.Any(edge => edge.ToString() == e.ToString()))
                return;

            edges.Add(e);
        }

        // return a list of outgoing edges of specified label
        public List<GraphEdge> GetEdges(string label)
        {
            List<GraphEdge> list = new List<GraphEdge>();
            foreach (GraphEdge e in Edges)
                if (e.Label == label)
                    list.Add(e);
            return list;
        }

        // return text form of node, including outgoing edges
        public override string ToString()
        {
            string result = Name + "\n";
            foreach (GraphEdge e in Edges)
            {
                result = result + "  " + e.ToString() + "\n";
            }
            return result;
        }
    }
}
