using Fithub_Data.Models;
using Fithub_DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_DL
{
    public class WriteCoach : IWriteCoach
    {
        private readonly string connectionString = "server=g708915-w101;database=Fithub;Trusted_Connection=true";
        public int AddCoachRatingByUser(Rating rating)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    var sqlSP = "dbo.uspAddCoachRating";

                    SqlCommand sqlCommand = new(sqlSP, sqlConnection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@CoachID", rating.CoachID);
                    sqlCommand.Parameters.AddWithValue("@UserID", rating.UserID);
                    sqlCommand.Parameters.AddWithValue("@Rating", rating.RatingValue);
                    return sqlCommand.ExecuteNonQuery();


                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public int UpdateCoachRatingByUser(int CoachID, int UserID, Rating rating)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    var sqlSP = "dbo.uspUpdateCoachRating";

                    SqlCommand sqlCommand = new(sqlSP, sqlConnection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@CoachID", rating.CoachID);
                    sqlCommand.Parameters.AddWithValue("@UserID", rating.UserID);
                    sqlCommand.Parameters.AddWithValue("@Rating", rating.RatingValue);
                    return sqlCommand.ExecuteNonQuery();


                }
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
