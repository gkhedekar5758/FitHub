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
    public int UpdateUserPassword(int userId, string password);
  }
}
