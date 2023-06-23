using Fithub_BL.Interfaces;
using Fithub_DL.Interfaces;
using System;

namespace Fithub_BL
{
    public class UpdateTestimony : IUpdateTestimony
    {
        private readonly IWriteTestimony _writeTestimony;

        public UpdateTestimony(IWriteTestimony writeTestimony)
        {
            _writeTestimony = writeTestimony ?? throw new ArgumentNullException(nameof(writeTestimony));
        }
        public int CreateTestimonyOfUser(string connection, string testimony, int userID)
        {
            return _writeTestimony.WriteTestimonyInDB(connection, testimony, userID);
        }

        public int UpdateTestimonyOfUser(string connection, string testimony, int userID, int testimonyID)
        {
            return _writeTestimony.ModifyTestimonyInDB(connection,testimony, userID, testimonyID);
        }
    }
}
