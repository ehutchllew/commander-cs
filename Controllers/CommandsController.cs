using System.Collections.Generic;
using AutoMapper;
using commander.Data;
using commander.Dtos;
using commander.Models;
using Microsoft.AspNetCore.Mvc;

namespace commander.Controllers
{
    // Startup is the entry point to this app, but this file is the avenue to all things `commands`.
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommanderRepo _repository;
        // This class is portable -- we can vastly change this behavior by passing in another implementation of `repository` that still adheres to the interface. We can override each method for any need.
        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            this._mapper = mapper;
            this._repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = this._repository.GetAllCommands();
            return Ok(this._mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        // GET with params: api/commands/{id}
        [HttpGet("{id}")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = this._repository.GetCommandById(id);
            if (commandItem != null)
            {
                return Ok(this._mapper.Map<CommandReadDto>(commandItem));
            }

            return NotFound();
        }
    }
}