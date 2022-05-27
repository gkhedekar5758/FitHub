using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fithub_Data.DTO.ResponseDTO;
using Fithub_Data.Models;

namespace Fithub_BL.Interfaces
{
  /// <summary>
  /// contract to fetch the coaches from DB
  /// </summary>
  public interface IQueryCoach
  {
    /// <summary>
    /// contract to fetch coaches by class ID
    /// </summary>
    /// <param name="classID"></param>
    /// <returns></returns>
    public IEnumerable<Coach> QueryCoachesByClassID(int classID);

    /// <summary>
    /// contract to fetch a coach from his ID
    /// </summary>
    /// <param name="coachID"></param>
    /// <returns></returns>
    public CoachClassResponseDTO QueryCoachByCoachID(int coachID);
  }
}
