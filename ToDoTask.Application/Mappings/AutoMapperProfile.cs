using AutoMapper;
using ToDoTask.Application.DTOs;
using ToDoTask.Domain.Entities;

namespace ToDoTask.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ToDo, ToDoDto>().ReverseMap();
            CreateMap<ToDo, CreateToDoDto>().ReverseMap();
            CreateMap<ToDo, UpdateToDoDto>().ReverseMap();

            CreateMap<User, RegisterUserDto>().ReverseMap();

        }
    }
}
