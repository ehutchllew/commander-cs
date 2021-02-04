using System.Collections.Generic;
using AutoMapper;
using commander.Data;
using commander.Dtos;
using commander.Models;
using Microsoft.AspNetCore.JsonPatch;
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

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = this._mapper.Map<Command>(commandCreateDto);

            this._repository.CreateCommand(commandModel);
            this._repository.SaveChanges();

            var commandReadDto = this._mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(this.GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = this._repository.GetAllCommands();
            return Ok(this._mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        // GET with params: api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = this._repository.GetCommandById(id);
            if (commandItem != null)
            {
                return Ok(this._mapper.Map<CommandReadDto>(commandItem));
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand([FromRoute] int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = this._repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            this._mapper.Map(commandUpdateDto, commandModelFromRepo);

            this._repository.UpdateCommand(commandModelFromRepo);
            this._repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PatchCommand([FromRoute] int id, [FromBody] JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var command = this._repository.GetCommandById(id);
            if (command == null)
            {
                return NotFound();
            }

            var commandToPatch = this._mapper.Map<CommandUpdateDto>(command);
            patchDoc.ApplyTo(commandToPatch, ModelState);
            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            this._mapper.Map(commandToPatch, command);

            this._repository.UpdateCommand(command);
            this._repository.SaveChanges();

            return NoContent();
        }
    }
}