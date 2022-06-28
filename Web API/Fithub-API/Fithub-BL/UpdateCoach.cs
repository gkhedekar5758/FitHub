using Fithub_BL.Interfaces;
using Fithub_Data.Models;
using Fithub_DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_BL
{
    public class UpdateCoach : IUpdateCoach
    {
        private readonly IWriteCoach _writeCoach;

        public UpdateCoach(IWriteCoach writeCoach)
        {
            _writeCoach = writeCoach?? throw new ArgumentNullException(nameof(writeCoach));
        }
        public int AddCoachRating(Rating rating)
        {
            return _writeCoach.AddCoachRatingByUser(rating);
        }

        public int UpdateCoachRating(int CoachID, int UserID, Rating rating)
        {
            return _writeCoach.UpdateCoachRatingByUser(CoachID, UserID, rating);
        }
    }
}
