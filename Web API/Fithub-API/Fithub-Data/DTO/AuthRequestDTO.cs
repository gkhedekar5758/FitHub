using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_Data.DTO
{
  public class AuthRequestDTO
  {
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public string clientURL { get; set; }
  }
}
