using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_BL.Interfaces
{
    public interface IUpdateTestimony
    {
        public int CreateTestimonyOfUser(string connection, string testimony, int userID);
        public int UpdateTestimonyOfUser(string connection,string testimony, int userID,int testimonyID);
    }
}
