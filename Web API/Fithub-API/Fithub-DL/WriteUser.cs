using Fithub_Data.Models;
using Fithub_DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Fithub_DL
{
    /// <summary>
    /// update user information in DB
    /// </summary>
    public class WriteUser : IWriteUser
    {
        const string connectionString = "server=g708915-w101;database=Fithub;Trusted_Connection=true";
        public int UpdateUserPassword(string connection, int userId, string password)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    sqlConnection.Open();
                    string query = "Update [dbo].[User] set Password = '" + password + "'" + " where userId =" + Convert.ToString(userId);
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                    int result = sqlCommand.ExecuteNonQuery();
                    return result;
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public int WriteUserInfoInDB(string connection, UserInfo userInfo, int UserID)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    sqlConnection.Open();
                    List<SqlParameter> sqlParameters = new();

                    string sp = "dbo.uspUserInfoInsert";

                    SqlCommand sqlCommand = new SqlCommand(sp, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure };
                    sqlCommand.Parameters.Add(new SqlParameter("@weight", userInfo?.Weight != null ? userInfo.Weight : DBNull.Value));
                    sqlCommand.Parameters.Add(new SqlParameter("@height", userInfo?.Height != null ? userInfo.Height : DBNull.Value));
                    sqlCommand.Parameters.Add(new SqlParameter("@BMI", userInfo?.BMI != null ? userInfo.BMI : DBNull.Value));
                    sqlCommand.Parameters.Add(new SqlParameter("@mobileNo", userInfo?.MobileNo != null ? userInfo.MobileNo : DBNull.Value));
                    sqlCommand.Parameters.Add(new SqlParameter("@emergencyNo", userInfo?.EmergencyMobileNo != null ? userInfo.EmergencyMobileNo : DBNull.Value));
                    sqlCommand.Parameters.Add(new SqlParameter("@userID", UserID));


                    return sqlCommand.ExecuteNonQuery();



                }
            }
            catch (Exception e)
            {

                throw;
            }
        }



        public int WriteUserInDB(string connection, User user)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connection))
                {
                    sqlConnection.Open();
                    List<SqlParameter> sqlParameters = new List<SqlParameter>();
                    SqlParameter idOutParam = new SqlParameter("@ID_OUT", DBNull.Value) { Direction = System.Data.ParameterDirection.Output, SqlDbType = System.Data.SqlDbType.Int };
                    string sp = "dbo.uspUserInsert";

                    SqlCommand sqlCommand = new SqlCommand(sp, sqlConnection) { CommandType = System.Data.CommandType.StoredProcedure };
                    sqlCommand.Parameters.Add(new SqlParameter("@email", user.Email));
                    sqlCommand.Parameters.Add(new SqlParameter("@password", user.Password));
                    sqlCommand.Parameters.Add(new SqlParameter("@firstname", user.FirstName));
                    sqlCommand.Parameters.Add(new SqlParameter("@lastname", user.LastName));
                    sqlCommand.Parameters.Add(new SqlParameter("@externalloginprovider", user.ExternalLoginProvider));
                    sqlCommand.Parameters.Add(new SqlParameter("@externalloginprovidername", user.ExternalLoginProviderName));
                    sqlCommand.Parameters.Add(new SqlParameter("@externalproviderkey", user.ExternalProviderKey));
                    sqlCommand.Parameters.Add(new SqlParameter("@IsExternalProvider", user.IsExternalProvider));
                    sqlCommand.Parameters.Add(new SqlParameter("@isactive", 1));
                    sqlCommand.Parameters.Add(idOutParam);

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result >= 0)
                        user.UserID = Convert.ToInt32(idOutParam.Value);

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
