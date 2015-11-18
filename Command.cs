using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo
{
    abstract class Command
    {
        readonly string help;
        public string Help { get { return help; } }
        readonly int minArguments;
        int MinArguments { get { return minArguments; } }
        readonly string name;
        public string Name { get { return name; } }
        protected Command(string name, string help, int minArguments = 0)
        {
            this.help = help;
            this.minArguments = minArguments;
            this.name = name;
        }

        /// <summary>Run this commandsummary>
        /// <returns>false if help should be shown, true otherwise</returns>
        public bool Run(Program program, IList<string> arguments)
        {
            if (arguments.Count < MinArguments)
            {
                Console.Error.WriteLine(Name + " requires " + MinArguments + ". You provided " + arguments.Count + ".");
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
