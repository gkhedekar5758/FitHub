using Fithub_Data.DTO.ResponseDTO;
using Fithub_Data.Models;
using System.Collections.Generic;

namespace Fithub_DL.Interfaces
{
    public interface IReadCoach
    {
        public IEnumerable<Coach> GetCoachesByClassID(string connection, int classID);
        public CoachClassResponseDTO GetCoachByCoachID(string connection, int coachID);
        public Rating GetCoachRatingByUser(string connection, int coachID, int userID);
    }
}
