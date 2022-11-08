using Fithub_BL.Interfaces;
using Fithub_Data.Models;
using Fithub_DL.Interfaces;
using System;

namespace Fithub_BL
{
    public class UpdateCoach : IUpdateCoach
    {
        private readonly IWriteCoach _writeCoach;

        public UpdateCoach(IWriteCoach writeCoach)
        {
            _writeCoach = writeCoach ?? throw new ArgumentNullException(nameof(writeCoach));
        }
        public int AddCoachRating(string connection, Rating rating)
        {
            return _writeCoach.AddCoachRatingByUser(connection, rating);
        }

        public int UpdateCoachRating(string connection, int CoachID, int UserID, Rating rating)
        {
            return _writeCoach.UpdateCoachRatingByUser(connection, CoachID, UserID, rating);
        }
    }
}
