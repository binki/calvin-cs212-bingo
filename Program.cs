using System;
using System.Collections.Generic;
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

        public IReadOnlyDictionary<string, Command> Commands { get; }

        Command HelpCommand { get; }

        Program()
        {
            HelpCommand = new Commands.Help();
            Commands = new[] {
                new Commands.Dump(),
                new Commands.Exit(),
                new Commands.Friends(),
                HelpCommand,
                new Commands.Read(),
                new Commands.Show(),
            }.ToDictionary(command => command.Name);
        }

        // accept, parse, and execute user commands
        private void commandLoop()
        {
            Console.WriteLine("Welcome to Harry’s Dutch Bingo Parlor!");

            while (!ShouldExit)
            {
                Console.WriteLine();
                Console.Write("Enter a command: ");
                var line = Console.ReadLine();
                var commandWords = Regex.Split(line, @"\s+");        // split input into array of words
                var commandName = commandWords[0];

                Command command;
                if (!Commands.TryGetValue(commandName, out command))
                {
                    // illegal command
                    Console.Error.WriteLine($"Command not found: {commandName}.");
                    command = HelpCommand;
                }

                var arguments = commandWords.Skip(1).ToArray();
                while (!command.Run(this, arguments))
                    // If the previous command indicated that help should be shown
                    // (through its arguments validation), show help.
                    command = HelpCommand;
            }
        }

        static void Main(string[] args)
        {
            new Program().commandLoop();
        }
    }
}
