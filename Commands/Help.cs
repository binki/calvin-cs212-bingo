using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo.Commands
{
    class Help : Command
    {
        public Help()
        : base("help", "Show this help")
        {
        }

        protected override bool RunCore(Program program, IList<string> arguments)
        {
            Console.WriteLine("The following commands are available:");
            Console.WriteLine();
            foreach (var command in program.Commands.OrderBy(c => c.Name))
                Console.WriteLine($"{command.Name} {command.Help}");
            return true;
        }
    }
}
