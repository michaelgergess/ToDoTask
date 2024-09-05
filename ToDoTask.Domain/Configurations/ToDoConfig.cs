using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ToDoTask.Domain.Entities;

namespace ToDoTask.Domain.Configurations
{
    public class ToDoConfig : IEntityTypeConfiguration<ToDo>
    {
         public void Configure(EntityTypeBuilder<ToDo> builder)
         {
             builder.HasKey(t => t.Id);
             builder.Property(t => t.Title).IsRequired().HasMaxLength(300);
             builder.Property(t => t.User).IsRequired();
             builder.Property(t => t.UserId).IsRequired();
             
             builder.HasOne(t => t.User)
                    .WithMany(u => u.ToDos)
                    .HasForeignKey(t => t.UserId)
                    .IsRequired(); 
        }
    }

}
