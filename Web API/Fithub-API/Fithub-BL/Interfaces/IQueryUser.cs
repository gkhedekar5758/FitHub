using Fithub_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_BL.Interfaces
{
  /// <summary>
  /// contract for QueryUser
  /// </summary>
  public interface IQueryUser
  {
    public User QueryUserByEmail(string connection,string emailID);
    public string CheckUserPassword(string connection,string email);

    public int QueryUserIdByEmail(string connection,string email);
  }
}
