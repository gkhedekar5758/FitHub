using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
