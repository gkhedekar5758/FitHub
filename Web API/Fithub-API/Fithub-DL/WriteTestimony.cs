using Fithub_Data.Models;
using Fithub_DL.Interfaces;
using System.Data.SqlClient;
using System;

namespace Fithub_DL
{
    public class WriteTestimony : IWriteTestimony
    {
        public int ModifyTestimonyInDB(string connection, string testimony, int userID, int testimonyID)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    sqlConnection.Open();
                    var sqlSP = "dbo.uspModifyTestimony";

                    SqlCommand sqlCommand = new(sqlSP, sqlConnection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@Testimony", testimony);
                    sqlCommand.Parameters.AddWithValue("@UserID", userID);
                    sqlCommand.Parameters.AddWithValue("@TestimonyID", testimonyID);

                    return sqlCommand.ExecuteNonQuery();


                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public int WriteTestimonyInDB(string connection, string testimony, int userID)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    sqlConnection.Open();
                    var sqlSP = "dbo.uspInsertTestimony";
                    SqlParameter sqlParameterIDOut = new SqlParameter("@TestimonyID", DBNull.Value) { Direction = System.Data.ParameterDirection.Output, SqlDbType = System.Data.SqlDbType.Int };
                    SqlCommand sqlCommand = new(sqlSP, sqlConnection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@Testimony", testimony);
                    sqlCommand.Parameters.AddWithValue("@UserID", userID);
                    sqlCommand.Parameters.Add(sqlParameterIDOut);
                    int result= sqlCommand.ExecuteNonQuery();
                    if (result >= 0)
                    {
                        return Convert.ToInt32(sqlParameterIDOut.Value);
                    }
                    else
                    {
                        return result;
                    }


                }
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
