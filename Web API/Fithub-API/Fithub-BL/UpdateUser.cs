using Fithub_BL.Interfaces;
using Fithub_DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_BL
{
  public class UpdateUser : IUpdateUser
  {
    private readonly IWriteUser _writeUser;
    public UpdateUser(IWriteUser writeUser)
    {
      _writeUser = writeUser ?? throw new ArgumentNullException(nameof(_writeUser));
    }
    /// <summary>
    /// reset user password in DB
    /// </summary>
    /// <param name="userId">user id of the user</param>
    /// <param name="password">password of the user</param>
    /// <returns></returns>
    public int ResetUserPassword(int userId, string password)
    {
      return _writeUser.UpdateUserPassword(userId, password);
    }
  }
}
