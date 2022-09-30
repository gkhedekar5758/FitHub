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
        public int AddCoachRatingByUser(string connection,Rating rating);
        public int UpdateCoachRatingByUser(string connection,int CoachID, int UserID, Rating rating);
    }
}
