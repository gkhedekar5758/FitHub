using Fithub_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_BL.Interfaces
{
    public interface IUpdateCoach
    {
        public int AddCoachRating(string connection,Rating rating);
        public int UpdateCoachRating(string connection,int CoachID, int UserID, Rating rating);
    }
}
