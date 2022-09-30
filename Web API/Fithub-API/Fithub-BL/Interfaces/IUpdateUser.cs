using Fithub_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_BL.Interfaces
{
  /// <summary>
  /// contract for update user
  /// </summary>
  public interface IUpdateUser
  {
    public int ResetUserPassword(string connection,int userId, string password);

        public int RegisterUser(string connection,User user);
  }
}
