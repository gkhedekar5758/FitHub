using Fithub_Data.DTO.ResponseDTO;
using Fithub_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_DL.Interfaces
{
  public interface IReadCoach
  {
    public IEnumerable<Coach> GetCoachesByClassID(int classID);
    public CoachClassResponseDTO GetCoachByCoachID(int coachID);
    public Rating GetCoachRatingByUser(int coachID, int userID);
  }
}
