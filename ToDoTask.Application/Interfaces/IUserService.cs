using ToDoTask.Application.DTOs;

namespace ToDoTask.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterUserDto registerUserDto);
        Task<string> LoginUserAsync(LoginUserDto loginUserDto);
    }
}
