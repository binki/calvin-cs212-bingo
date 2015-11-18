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
        readonly string label;
        /// <summary>
        ///   Label of this edge.
        /// </summary>
        public string Label { get { return label; } }

        readonly GraphNode fromNode;
        GraphNode FromNode { get { return fromNode; } }
        readonly GraphNode toNode;
        GraphNode ToNode { get { return toNode; } }

        /// <summary>
        ///   The name of the “to” person in the relationship.
        /// </summary>
        public string To { get { return ToNode.Name; } }

        public GraphEdge(GraphNode from, GraphNode to, string label)
        {
            this.fromNode = from;
            this.toNode = to;
            this.label = label;
        }

        /// <summary>
        ///   return string form of edge
        /// </summary>
        public override string ToString()
        {
            return FromNode.Name + " --(" + Label + ")--> " + To;
        }
    }
}
