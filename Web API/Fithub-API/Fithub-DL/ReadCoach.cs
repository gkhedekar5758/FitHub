using Fithub_Data.DTO.ResponseDTO;
using Fithub_Data.Models;
using Fithub_DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Fithub_DL
{
    public class ReadCoach : IReadCoach
    {
        private readonly string connectionString = "server=g708915-w101;database=Fithub;Trusted_Connection=true";

        public CoachClassResponseDTO GetCoachByCoachID(string connection, int coachID)
        {
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    sqlConnection.Open();
                    var sqlSP = "dbo.uspReadCoachByCoachID";
                    SqlCommand sqlCommand = new SqlCommand(sqlSP, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(new SqlParameter("@CoachID", coachID));
                    var result = sqlCommand.ExecuteReader();
                    CoachClassResponseDTO response = null;

                    if (result != null)
                    {
                        response = new CoachClassResponseDTO();
                        while (result.Read())
                        {
                           
                            Class cl = new Class();

                            response.CoachID = Convert.ToInt32(result["CoachID"]);
                            response.CoachName = result["CoachName"].ToString();
                            response.Degree = result["Degree"].ToString();
                            response.PhotoURL = result["PhotoURL"].ToString();
                            cl.ClassName = result["ClassName"].ToString();
                            response.Classes.Add(cl);
                        }
                    }
                    return response;
                }

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public IEnumerable<Coach> GetCoachesByClassID(string connection, int classID)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    sqlConnection.Open();
                    var sqlSP = "dbo.uspReadCoachesByClassID";
                    SqlParameter sqlParameter = new SqlParameter("@ClassID", classID);
                    SqlCommand sqlCommand = new SqlCommand(sqlSP, sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add(sqlParameter);
                    var result = sqlCommand.ExecuteReader();
                    List<Coach> coaches = null;
                    Coach coach = null;
                    if (result != null)
                    {
                        coaches = new List<Coach>();
                        while (result.Read())
                        {

                            coach = new Coach();
                            coach.CoachID = Convert.ToInt32(result["CoachID"]);
                            coach.CoachName = result["CoachName"].ToString();
                            coach.PhotoURL = result["PhotoURL"].ToString();
                            coaches.Add(coach);
                        }
                    }
                    return coaches;

                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public Rating GetCoachRatingByUser(string connection, int coachID, int userID)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    sqlConnection.Open();
                    var sqlSP = "dbo.uspReadCoachRatingByCoachIDUserID";

                    SqlCommand sqlCommand = new(sqlSP, sqlConnection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@CoachID", coachID);
                    sqlCommand.Parameters.AddWithValue("@UserID", userID);
                    var result = sqlCommand.ExecuteReader();
                    Rating rating = null;
                    if (result != null)
                    {
                        rating = new Rating();
                        while (result.Read())
                        {
                            rating.RatingID = Convert.ToInt32(result["RatingID"]);
                            rating.CoachID = Convert.ToInt32(result["CoachID"]);
                            rating.UserID = Convert.ToInt32(result["UserID"]);
                            rating.RatingValue = Convert.ToString(result["RatingValue"]);

                        }

                    }
                    return rating;


                }
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
