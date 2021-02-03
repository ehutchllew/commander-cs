using System;
using System.Collections.Generic;
using System.Linq;
using commander.Models;

namespace commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly CommanderContext _context;

        public SqlCommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            this._context.Commands.Add(command);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return this._context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return this._context.Commands.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (this._context.SaveChanges() >= 0);
        }
    }
}