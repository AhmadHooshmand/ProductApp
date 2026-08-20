using System.ComponentModel.DataAnnotations;

namespace ProductApp.DTOs
{
    public class RegisterDto
    {
        [Required , MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;

    }
}
