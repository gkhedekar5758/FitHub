using Fithub_Data.Models;

namespace Fithub_BL.Interfaces
{
    public interface IUpdateCoach
    {
        public int AddCoachRating(string connection, Rating rating);
        public int UpdateCoachRating(string connection, int CoachID, int UserID, Rating rating);
    }
}
