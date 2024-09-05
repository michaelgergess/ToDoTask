using System.ComponentModel.DataAnnotations;

namespace ToDoTask.Application.DTOs
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
