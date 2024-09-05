using ToDoTask.Application.Interfaces;
using ToDoTask.Domain.Entities;
using ToDoTask.Infrastructure.Context;

namespace ToDoTask.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IRepository<ToDo> _toDosRepository;
        private IRepository<User> _userRepository;
        private IRepository<Role> _roleRepository;


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public UnitOfWork()
        {
        }

        public IRepository<User> UserRepository
        {
            get
            {
                return _userRepository ??= new Repository<User>(_context);
            }
        }
        public IRepository<Role> RoleRepository
        {
            get
            {
                return _roleRepository ??= new Repository<Role>(_context);
            }
        }
        public IRepository<ToDo> ToDosRepository
        {
            get
            {
                return _toDosRepository ??= new Repository<ToDo>(_context);
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}