using System.ComponentModel.DataAnnotations;

namespace ToDoTask.Application.DTOs
{
    public class CreateToDoDto
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(300, ErrorMessage = "Username cannot exceed 300 characters")]
        public required string Title { get; set; }

    }
}
