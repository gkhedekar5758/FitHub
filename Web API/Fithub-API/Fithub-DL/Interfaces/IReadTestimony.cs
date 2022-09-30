using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_DL.Interfaces
{
    public interface IReadTestimony
    {
        public string ReadTestimonyByUser(string connection,int UserID);
    }
}
