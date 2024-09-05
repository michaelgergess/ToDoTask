using ToDoTask.Domain.Entities;

namespace ToDoTask.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
         IRepository<ToDo> ToDosRepository { get; }
        IRepository<Role> RoleRepository { get; }

        IRepository<User> UserRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
