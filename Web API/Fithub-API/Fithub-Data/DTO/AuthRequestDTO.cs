using System.ComponentModel.DataAnnotations;

namespace Fithub_Data.DTO
{
    public class AuthRequestDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string clientURL { get; set; }
        public string clientRegURL { get; set; }
    }
}
