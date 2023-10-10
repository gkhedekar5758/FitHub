using Fithub_Data.Models;
using System.Threading.Tasks;

namespace Fithub_DL.Interfaces
{
    /// <summary>
    /// contract for ReadUser
    /// </summary>
    public interface IReadUser
    {
        public Task<User> ReadUserByEmail(string connection, string emailID);

        public string ReadUsersPassword(string connection, string email);
        public int ReadUserIdByEmail(string connection, string email);
    }
}
