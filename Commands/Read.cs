using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bingo.Commands
{
    class Read : Command
    {
        public Read()
        : base("read", "[filename] Read a relationship graph from a file")
        {
        }

        protected override bool RunCore(Program program, IList<string> arguments)
        {
            // Read RelationshipGraph whose filename is passed in as a parameter.
            // Build a RelationshipGraph in RelationshipGraph rg

            var filename = arguments[0];

            // create a new RelationshipGraph object
            var rg = new RelationshipGraph();

            // name of person currently being read
            string name = "";
            int numPeople = 0;
            string[] values;
            Console.Write("Reading file " + filename + "\n");
            try
            {
                string input = System.IO.File.ReadAllText(filename);// read file
                input = input.Replace("\r", ";");                   // get rid of nasty carriage returns
                input = input.Replace("\n", ";");                   // get rid of nasty new lines
                string[] inputItems = Regex.Split(input, @";\s*");  // parse out the relationships (separated by ;)
                foreach (string item in inputItems)
                {
                    if (item.Length > 2)                            // don't bother with empty relationships
                    {
                        values = Regex.Split(item, @"\s*:\s*");     // parse out relationship:name
                        if (values[0] == "name")                    // name:[personname] indicates start of new person
                        {
                            name = values[1];                       // remember name for future relationships
                            rg.AddNode(name);                       // create the node
                            numPeople++;
                        }
                        // Keys defining relationships start with “has” followed
                        // by a relationship name.
                        else if (values[0].StartsWith("has"))
                        {
                            // Extract relationship name from after “has”. E.g.,
                            // hasFriend.
                            var relationship = values[0].Substring("has".Length);
                            // PascalCase -> camelCase. E.g., Friend -> friend.
                            if (relationship.Length > 0)
                                relationship = char.ToLowerInvariant(relationship[0]) + relationship.Substring(1);
                            rg.AddEdge(name, values[1], relationship); // add relationship (name1, name2, relationship)

                            // handle symmetric relationships -- add the other way
                            if (relationship == "spouse" || relationship == "friend")
                                rg.AddEdge(values[1], name, relationship);

                            // for parent relationships add child as well
                            else if (relationship == "parent")
                                rg.AddEdge(values[1], name, "child");
                            else if (relationship == "child")
                                rg.AddEdge(values[1], name, "parent");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Unable to read file {0}: {1}\n", filename, e.ToString());
            }
            Console.WriteLine(numPeople + " people read");

            program.RelationshipGraph = rg;
            return true;
        }
    }
}
