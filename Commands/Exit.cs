using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo.Commands
{
    [Export(typeof(Command))]
    class Exit : Command
    {
        public Exit()
        : base("exit", "Exit to shell")
        {
        }

        protected override bool RunCore(Program program, IList<string> arguments)
        {
            program.ShouldExit = true;
            return true;
        }
    }
}
