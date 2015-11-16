using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bingo
{
    /// <summary>
    /// Represents a labeled, directed edge in a RelationshipGraph
    /// </summary>
    class GraphEdge
    {
        /// <summary>
        ///   Label of this edge.
        /// </summary>
        public string Label { get; }

        GraphNode FromNode { get; }
        GraphNode ToNode { get; }

        /// <summary>
        ///   The name of the “to” person in the relationship.
        /// </summary>
        public string To { get { return ToNode.Name; } }

        public GraphEdge(GraphNode from, GraphNode to, string label)
        {
            FromNode = from;
            ToNode = to;
            Label = label;
        }

        /// <summary>
        ///   return string form of edge
        /// </summary>
        public override string ToString() => FromNode.Name + " --(" + Label + ")--> " + To;
    }
}
