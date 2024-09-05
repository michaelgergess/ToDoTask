using Microsoft.EntityFrameworkCore;
using System;
using ToDoTask.Domain.Entities;

namespace ToDoTask.Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Role> Roles { get; set; }

        public ApplicationDbContext()
        {
            
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(new Role
            {
                Id = 1,
                Name = "User",
            });
            modelBuilder.Entity<User>().HasData(GenerateUsersSeedData());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
        private List<User> GenerateUsersSeedData()
        {
            return new List<User>
           {
        new User { Id = 1, Name = "Alice Johnson", Password =HashPassword( "P@ssw0rd1"), RoleId = 1 },
        new User { Id = 2, Name = "Bob Smith", Password =HashPassword( "P@ssw0rd1"), RoleId = 1 },
        new User { Id = 3, Name = "Carol Davis", Password =HashPassword( "P@ssw0rd1"), RoleId = 1 },
        new User { Id = 4, Name = "David Wilson", Password =HashPassword( "P@ssw0rd1"), RoleId = 1 },
        new User { Id = 5, Name = "Emily Brown", Password =HashPassword( "P@ssw0rd1"), RoleId = 1 },
        new User { Id = 6, Name = "Frank Harris", Password =HashPassword( "P@ssw0rd1"), RoleId = 1 },
        new User { Id = 7, Name = "Grace Martin",Password =HashPassword( "P@ssw0rd1"), RoleId = 1 },
        new User { Id = 8, Name = "Henry Thompson", Password =HashPassword( "P@ssw0rd1"), RoleId = 1 },
        new User { Id = 9, Name = "Ivy Garcia", Password =HashPassword( "P@ssw0rd1"), RoleId = 1 },
        new User { Id = 10, Name = "Jack Lee", Password =HashPassword( "P@ssw0rd1"), RoleId = 1 }
         };
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
           
        }
       
    }
}
