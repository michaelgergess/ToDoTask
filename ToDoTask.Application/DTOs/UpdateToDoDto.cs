using System.ComponentModel.DataAnnotations;

namespace ToDoTask.Application.DTOs
{
    public class UpdateToDoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(300, ErrorMessage = "Username cannot exceed 300 characters")]
        public required string Title { get; set; }

        public required  bool completed { get; set; }

    }
}
