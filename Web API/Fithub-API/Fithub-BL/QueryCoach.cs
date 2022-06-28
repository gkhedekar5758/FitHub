using Fithub_BL.Interfaces;
using Fithub_Data.DTO.ResponseDTO;
using Fithub_Data.Models;
using Fithub_DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_BL
{
  /// <summary>
  /// class to query the coaches <see cref="IQueryCoach"/>
  /// </summary>
  public class QueryCoach : IQueryCoach
  {
    private readonly IReadCoach _readCoach;

    public QueryCoach(IReadCoach readCoach)
    {
      _readCoach = readCoach??throw new ArgumentNullException(nameof(readCoach)) ;
    }

    public CoachClassResponseDTO QueryCoachByCoachID(int coachID)
    {
      return _readCoach.GetCoachByCoachID(coachID);
    }

    public IEnumerable<Coach> QueryCoachesByClassID(int classID)
    {
      return _readCoach.GetCoachesByClassID(classID);
    }

    public Rating QueryCoachRatingByUserID(int coachID, int userID)
    {
      return _readCoach.GetCoachRatingByUser(coachID, userID);
    }
  }
}
