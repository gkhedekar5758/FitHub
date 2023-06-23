using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_DL.Interfaces
{
    public interface IWriteTestimony
    {
        public int WriteTestimonyInDB(string connection, string testimony,int userID);
        public int ModifyTestimonyInDB(string connection,string testimony,int userID,int testimonyID);
    }
}
