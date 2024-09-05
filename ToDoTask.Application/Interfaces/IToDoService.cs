using ToDoTask.Application.DTOs;
using ToDoTask.Domain.Entities;

namespace ToDoTask.Application.Interfaces
{
    public interface IToDoService
    {
        Task<ToDoDto> GetToDoByIdAsync(int id);
        Task<IEnumerable<ToDoDto>> GetAllToDosAsync(int pageNumber, int pageSize, int userId);
        Task AddToDoAsync(CreateToDoDto toDoDto,int userId);
        Task<UpdateToDoDto> UpdateToDoAsync( UpdateToDoDto updateToDoDto, int userId);
        Task<ToDoDto> DeleteToDoAsync(int id);
    }
}
