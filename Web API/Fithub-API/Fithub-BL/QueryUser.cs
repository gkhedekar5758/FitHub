using Fithub_BL.Interfaces;
using Fithub_Data.Models;
using Fithub_DL.Interfaces;
using System;

namespace Fithub_BL
{
  public class QueryUser : IQueryUser
  {
    private readonly IReadUser _readUser;

    /// <summary>
    /// constructor to initialize the depedency
    /// </summary>
    /// <param name="readUser"></param>
    public QueryUser(IReadUser readUser)
    {
      _readUser = readUser ?? throw new ArgumentNullException(nameof(readUser));
    }

    /// <summary>
    /// query users password from the userid. <see cref="IQueryUser.CheckUserPassword(int)"/>
    /// </summary>
    /// <param name="email"></param>
    /// <returns>password of the user</returns>
    public string CheckUserPassword(string connection, string email)
    {
      return _readUser.ReadUsersPassword(connection, email);
    }

    /// <summary>
    /// Query the user usin email id <see cref="IQueryUser.QueryUserByEmail(string)"/>
    /// </summary>
    /// <param name="emailID"></param>
    /// <returns>UserID</returns>
    public User QueryUserByEmail(string connection, string emailID)
    {
      User user=_readUser.ReadUserByEmail(connection, emailID);

      return user;
    }

    /// <summary>
    /// Query user id by email <see cref="IQueryUser.QueryUserIdByEmail(string)"/>
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    int IQueryUser.QueryUserIdByEmail(string connection, string email)
    {
      return _readUser.ReadUserIdByEmail(connection, email);

    }
  }
}
