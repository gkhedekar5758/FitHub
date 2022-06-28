using Fithub_Data.Models;
using Fithub_DL.Interfaces;
using System;
using System.Data.SqlClient;

namespace Fithub_DL
{
  public class ReadUser : IReadUser
  {
    const string connectionString = "server=g708915-w101;database=Fithub;Trusted_Connection=true";

    /// <summary>
    /// read the user by email id
    /// <see cref="IReadUser.ReadUserByEmail(string)"/>
    /// </summary>
    /// <param name="emailID">email id</param>
    /// <returns>user id of the user</returns>
    public User ReadUserByEmail(string emailID)
    {
      try
      {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
          connection.Open();
          
          string sqlSP= "dbo.uspReadUser";
          SqlParameter sqlParameter = new SqlParameter("@EmailID", emailID);// { ParameterName:"EmailID"}
          SqlCommand sqlCommand = new SqlCommand(sqlSP, connection);
          sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
          sqlCommand.Parameters.Add(sqlParameter);
          var result = sqlCommand.ExecuteReader();
          User returUser = null;
          if (result!= null)
          {
           
            if(result.Read())
            {
              returUser = new User();
              var userRolea = new UserRole();
              userRolea.Name = result["Name"].ToString();
              userRolea.NormalisedName = result["NormalisedName"].ToString();

              returUser.UserID = Convert.ToInt32(result["UserID"]);
              returUser.Email = result["Email"].ToString();
              returUser.Password = result["Password"].ToString();
              returUser.FirstName = result["FirstName"].ToString();
              returUser.LastName = result["LastName"].ToString();
              returUser.DateOfBirth = Convert.ToDateTime(result["DateOfBirth"]);
              returUser.ExternalLoginProvider = result["ExternalLoginProvider"].ToString();
              returUser.ExternalLoginProviderName = result["ExternalLoginProviderName"].ToString();
              returUser.ExternalProviderKey = result["ExternalProviderKey"].ToString();
              returUser.IsExternalProvider = Convert.ToBoolean(result["IsExternalProvider"]);
              returUser.IsActive = Convert.ToBoolean(result["IsActive"]);
              returUser.Role = userRolea;
            }

          }
          return returUser;
          
        }
      }
      catch(Exception e)
      {
        throw;
      }
      
    }

    /// <summary>
    /// read users password from DB <see cref="IReadUser.ReadUsersPassword(int)"/>
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public string ReadUsersPassword(string email)
    {
      try
      {
        using(SqlConnection connection=new SqlConnection(connectionString))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("Select [Password] from [dbo].[User] where Email = " + email, connection);
          var result = sqlCommand.ExecuteScalar();
          return Convert.ToString(result);
        }
      }
      catch (Exception e)
      {

        throw;
      }
    }

    /// <summary>
    /// read the user id from DB for a email <see cref="IReadUser.ReadUserIdByEmail(string)"/>
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    int IReadUser.ReadUserIdByEmail(string email)
    {
      try
      {
        using(SqlConnection connection=new SqlConnection(connectionString))
        {
          connection.Open();
          SqlCommand sqlCommand = new SqlCommand("Select userID from [dbo].[User] where Email = '" + email+"'", connection);
          //var id
          return Convert.ToInt32(sqlCommand.ExecuteScalar());
        }
      }
      catch (Exception e)
      {

        throw;
      }
    }
  }
}
