using AutoMapper;
using commander.Dtos;
using commander.Models;

namespace commander.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<Command, CommandReadDto>();
        }
    }
}