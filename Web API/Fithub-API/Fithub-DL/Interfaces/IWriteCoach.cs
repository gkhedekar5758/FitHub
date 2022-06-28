using Fithub_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_DL.Interfaces
{
    public interface IWriteCoach
    {
        public int AddCoachRatingByUser(Rating rating);
        public int UpdateCoachRatingByUser(int CoachID, int UserID, Rating rating);
    }
}
