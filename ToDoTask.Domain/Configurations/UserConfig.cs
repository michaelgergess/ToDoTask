using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ToDoTask.Domain.Entities;
using System.Text;
using System.Security.Cryptography;

namespace ToDoTask.Domain.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).IsRequired().HasMaxLength(45);
            builder.Property(t => t.Password).IsRequired().HasMaxLength(25);
            builder.OwnsOne( u=>u.Role ).HasData(new User { Id = 1, Name = "Michael", Password = "123", RoleId = 1 });
            builder.HasOne(u => u.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(t => t.RoleId)
                    .IsRequired();
        }
       
    }


}
