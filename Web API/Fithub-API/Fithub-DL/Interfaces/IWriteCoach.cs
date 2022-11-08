using Fithub_Data.Models;

namespace Fithub_DL.Interfaces
{
    public interface IWriteCoach
    {
        public int AddCoachRatingByUser(string connection, Rating rating);
        public int UpdateCoachRatingByUser(string connection, int CoachID, int UserID, Rating rating);
    }
}
