using Fithub_DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_DL
{
    public class ReadTestimony : IReadTestimony
    {
        const string connectionString = "server=g708915-w101;database=Fithub;Trusted_Connection=true";
        public string ReadTestimonyByUser(string connection, int UserID)
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
                    var result = sqlCommand.ExecuteScalar();
                    return result == null ? "" : result.ToString();

                }
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
