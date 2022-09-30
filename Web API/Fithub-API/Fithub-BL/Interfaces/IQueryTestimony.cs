using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_BL.Interfaces
{
    public interface IQueryTestimony
    {
        public string QueryTestimonyByUser(string connection,int UserID);
    }
}
