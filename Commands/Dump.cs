using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo.Commands
{
    [Export(typeof(Command))]
    class Dump : Command
    {
        public Dump()
        : base("dump", "Print out the graph")
        {
        }

        protected override bool RunCore(Program program, IList<string> arguments)
        {
            program.RelationshipGraph.Dump();
            return true;
        }
    }
}
