using Fithub_DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_DL
{
  /// <summary>
  /// update user information in DB
  /// </summary>
  public class WriteUser : IWriteUser
  {
    const string connectionString = "server=g708915-w101;database=Fithub;Trusted_Connection=true";
    public int UpdateUserPassword(int userId, string password)
    {
      try
      {
        using(SqlConnection sqlConnection=new SqlConnection(connectionString) )
        {
          sqlConnection.Open();
          string query = "Update [dbo].[User] set Password = '" + password +"'"+ " where userId =" + Convert.ToString(userId);
          SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
          int result=sqlCommand.ExecuteNonQuery();
          return result;
        }
      }
      catch (Exception e)
      {

        throw;
      }
    }
  }
}
