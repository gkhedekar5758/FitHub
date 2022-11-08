using System.ComponentModel.DataAnnotations;

namespace Fithub_Data.DTO
{
    public class ResetPasswordDTO
    {
        [Required]
        public int userId { get; set; }
        [Required]
        public string password { get; set; }
    }
}
