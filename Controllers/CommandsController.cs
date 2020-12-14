using System.Collections.Generic;
using commander.Data;
using commander.Models;
using Microsoft.AspNetCore.Mvc;

namespace commander.Controllers
{
    // Startup is the entry point to this app, but this file is the avenue to all things `commands`.
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        // This class is portable -- we can vastly change this behavior by passing in another implementation of `repository` that still adheres to the interface. We can override each method for any need.
        public CommandsController(ICommanderRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(commandItems);
        }

        // GET with params: api/commands/{id}
        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            return Ok(commandItem);
        }
    }
}