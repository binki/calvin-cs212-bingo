using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo
{
    abstract class Command
    {
        public string Help { get; }
        int MinArguments { get; }
        public string Name { get; }
        protected Command(string name, string help, int minArguments = 0)
        {
            Help = help;
            MinArguments = minArguments;
            Name = name;
        }

        /// <summary>Run this commandsummary>
        /// <returns>false if help should be shown, true otherwise</returns>
        public bool Run(Program program, IList<string> arguments)
        {
            if (arguments.Count < MinArguments)
            {
                Console.Error.WriteLine($"{Name} requires at least {MinArguments} argument(s). You provided {arguments.Count}.");
                return false;
            }

            return RunCore(program, arguments);
        }

        /// <summary>
        ///   Actually perform the action. Returning false causes help
        ///   to be displayed.
        /// </summary>
        /// <returns>false if there was an invocation error (help should be shown), true otherwise (regardless of success of operation)</returns>
        protected abstract bool RunCore(Program program, IList<string> arguments);
    }
}
