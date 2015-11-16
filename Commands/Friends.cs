using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo.Commands
{
    /// <summary>
    ///   Show a person's friends.
    /// </summary>
    class Friends : Command
    {
        public Friends()
        : base("friends", "[personname] List person’s friends", 1)
        {
        }

        protected override bool RunCore(Program program, IList<string> arguments)
        {
            var name = arguments[0];
            var person = program.RelationshipGraph.GetNode(name);
            if (person == null)
            {
                Console.WriteLine($"{name} not found.");
                return true;
            }

            Console.WriteLine($"{name}’s friends: {string.Join(" ", from e in person.GetEdges("friend") select e.To)}");
            return true;
        }
    }
}
