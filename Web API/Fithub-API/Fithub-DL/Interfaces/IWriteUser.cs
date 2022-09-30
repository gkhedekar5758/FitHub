using Fithub_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_DL.Interfaces
{
  /// <summary>
  /// contract for update user
  /// </summary>
  public interface IWriteUser
  {
    public int UpdateUserPassword(string connection,int userId, string password);
        public int WriteUserInDB(string connection,User user);
        
        public int WriteUserInfoInDB(string connection,UserInfo userInfo, int UserID);
  }
}
