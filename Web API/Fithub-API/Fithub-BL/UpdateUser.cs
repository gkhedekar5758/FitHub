using Fithub_BL.Interfaces;
using Fithub_Data.Models;
using Fithub_DL.Interfaces;
using System;

namespace Fithub_BL
{
    public class UpdateUser : IUpdateUser
    {
        private readonly IWriteUser _writeUser;
        public UpdateUser(IWriteUser writeUser)
        {
            _writeUser = writeUser ?? throw new ArgumentNullException(nameof(_writeUser));
        }

        public int RegisterUser(string connection, User user)
        {
            int userInfoResult = -1;
            int userResult = _writeUser.WriteUserInDB(connection, user);

            if (userResult >= 0)
                userInfoResult = _writeUser.WriteUserInfoInDB(connection, user.UserInfo, user.UserID);


            return userInfoResult;
        }

        /// <summary>
        /// reset user password in DB
        /// </summary>
        /// <param name="userId">user id of the user</param>
        /// <param name="password">password of the user</param>
        /// <returns></returns>
        public int ResetUserPassword(string connection, int userId, string password)
        {
            return _writeUser.UpdateUserPassword(connection, userId, password);
        }
    }
}
