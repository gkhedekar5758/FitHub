using Fithub_Data.DTO;
using Fithub_DL.Interfaces;
using System;
using System.Data.SqlClient;

namespace Fithub_DL
{
    public class ReadTestimony : IReadTestimony
    {
        const string connectionString = "server=g708915-w101;database=Fithub;Trusted_Connection=true";
        public TestimonyDTO ReadTestimonyByUser(string connection, int UserID)
        {
            try
            {

                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    sqlConnection.Open();
                    var sqlSP = "dbo.uspReadUserTestimony";
                    SqlCommand sqlCommand = new SqlCommand(sqlSP, sqlConnection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.Add(new SqlParameter("@UserID", UserID));
                    var result = sqlCommand.ExecuteReader();
                    TestimonyDTO testimonyDTO = null;
                    if (result != null)
                    {
                        if (result.Read())
                        {
                            testimonyDTO = new TestimonyDTO()
                            {
                                Testimony = result["Testimony"].ToString(),
                                UserID = Convert.ToInt32(result["UserID"]),
                                TestimonyID = Convert.ToInt32(result["TestimonyID"])

                            };
                        }
                    }
                    return testimonyDTO;

                }
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
