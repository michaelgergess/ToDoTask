
using Microsoft.EntityFrameworkCore;
using ToDoTask.Application.Interfaces;
using ToDoTask.Domain.Entities;
using ToDoTask.Infrastructure.Context;

namespace ToDoTask.Infrastructure.Repositories
    {
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }


        public async Task<IEnumerable<T>> GetAllAsyncPagination<T>(int pageNumber, int pageSize, int userId) 
        {
            if (typeof(T) == typeof(ToDo))
            {
                // Cast to DbSet<ToDo> and apply filtering
                var query = await _context.Set<ToDo>()
                    .Where(todo => todo.UserId == userId)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .AsNoTracking()
                    .ToListAsync();

                return (IEnumerable<T>)query;  
            }
            else
            {
                var query = await _dbSet
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .AsNoTracking()
                    .ToListAsync();

                return (IEnumerable<T>)query;
            }
        }



        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
            {
                return await _dbSet.FindAsync(id);
            }

            public async Task AddAsync(T entity)
            {
                await _dbSet.AddAsync(entity);
            }

            public async Task UpdateAsync(T entity)
            {
                _dbSet.Update(entity);
            }   

            public async Task DeleteAsync(int id)
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                }
            }
        }
    }
