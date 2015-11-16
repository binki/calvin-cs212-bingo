using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo.Commands
{
    [Export(typeof(Command))]
    class Show : Command
    {
        public Show()
        : base("show", "[personname] Show the relationships a person is involved in", 1)
        {
        }

        protected override bool RunCore(Program program, IList<string> arguments)
        {
            var name = arguments[0];
            var person = program.RelationshipGraph.GetNode(name);
            if (person != null)
                Console.WriteLine(person);
            else
                Console.Write("{0} not found", name);
            return true;
        }
    }
}
