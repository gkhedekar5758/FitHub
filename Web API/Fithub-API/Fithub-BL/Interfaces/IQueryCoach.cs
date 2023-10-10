using Fithub_Data.DTO.ResponseDTO;
using Fithub_Data.Models;
using System.Collections.Generic;

namespace Fithub_BL.Interfaces
{
    /// <summary>
    /// contract to fetch the coaches from DB
    /// </summary>
    public interface IQueryCoach
    {
        /// <summary>
        /// contract to fetch coaches by class ID
        /// </summary>
        /// <param name="classID"></param>
        /// <returns></returns>
        public IEnumerable<Coach> QueryCoachesByClassID(string connection, int classID);

        /// <summary>
        /// contract to fetch a coach from his ID
        /// </summary>
        /// <param name="coachID"></param>
        /// <returns></returns>
        public CoachClassResponseDTO QueryCoachByCoachID(string connection, int coachID);
        /// <summary>
        /// contract to fetch coach rating by user
        /// </summary>
        /// <param name="coachID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Rating QueryCoachRatingByUserID(string connection, int coachID, int userID);
        /// <summary>
        /// Contract for fetching all the rating response by user id
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IEnumerable<CoachRatingResponseDTO> QueryCoachRatingsByUser(string connection, int userID);
    }
}
