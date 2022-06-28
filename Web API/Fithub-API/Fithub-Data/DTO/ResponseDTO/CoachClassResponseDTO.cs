using Fithub_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_Data.DTO.ResponseDTO
{
  public class CoachClassResponseDTO
  {
    public int CoachID { get; set; }
    public string CoachName { get; set; }
    public string Degree { get; set; }
    public string PhotoURL { get; set; }

    /// <summary>
    /// classes run by the coach
    /// </summary>
    public ICollection<Class> Classes { get; set; }

    public CoachClassResponseDTO()
    {
      Classes = new List<Class>();
    }
  }
}
