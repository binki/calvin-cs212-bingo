using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Bingo
{
    class Program
    {
        public RelationshipGraph RelationshipGraph { get; set; } = new RelationshipGraph();
        public bool ShouldExit { get; set; }

        [ImportMany(typeof(Command))]
        public IEnumerable<Command> Commands { get; set; }

        // accept, parse, and execute user commands
        private void commandLoop()
        {
            var commandMap = Commands.ToDictionary(command => command.Name);
            var helpCommand = commandMap["help"];
            Console.WriteLine("Welcome to Harry’s Dutch Bingo Parlor!");

            while (!ShouldExit)
            {
                Console.WriteLine();
                Console.Write("Enter a command: ");
                var line = Console.ReadLine();
                var commandWords = Regex.Split(line, @"\s+");        // split input into array of words
                var commandName = commandWords[0];

                Command command;
                if (!commandMap.TryGetValue(commandName, out command))
                {
                    // illegal command
                    Console.Error.WriteLine($"Command not found: {commandName}.");
                    command = helpCommand;
                }

                var arguments = commandWords.Skip(1).ToArray();
                while (!command.Run(this, arguments))
                    // If the previous command indicated that help should be shown
                    // (through its arguments validation), show help.
                    command = helpCommand;
            }
        }

        static void Main(string[] args)
        {
            using (var catalog = new AssemblyCatalog(typeof(Program).Assembly))
            {
                using (var compositionContainer = new CompositionContainer(catalog))
                {
                    var program = new Program();
                    compositionContainer.ComposeParts(program);
                    program.commandLoop();
                }
            }
        }
    }
}
