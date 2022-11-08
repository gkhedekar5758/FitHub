using Fithub_Data.Models;

namespace Fithub_BL.Interfaces
{
    /// <summary>
    /// contract for QueryUser
    /// </summary>
    public interface IQueryUser
    {
        public User QueryUserByEmail(string connection, string emailID);
        public string CheckUserPassword(string connection, string email);

        public int QueryUserIdByEmail(string connection, string email);
    }
}
