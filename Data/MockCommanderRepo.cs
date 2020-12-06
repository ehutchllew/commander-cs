using System.Collections.Generic;
using commander.Models;

namespace commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
            new Command { Id = 0, HowTo = "Boil an Egg", Line = "Boil Water", Platform = "Kettle & Pan" },


            new Command { Id = 1, HowTo = "Cut Bread", Line = "Get a Knife", Platform = "Chopping Board" },

            new Command { Id = 2, HowTo = "Make Tea", Line = "Place teabag in cup", Platform = "Kettle & Cup" },

            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, HowTo = "Boil an Egg", Line = "Boil Water", Platform = "Kettle & Pan" };
        }
    }
}