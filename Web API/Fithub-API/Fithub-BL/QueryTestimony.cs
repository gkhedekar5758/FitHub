using Fithub_BL.Interfaces;
using Fithub_DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_BL
{
    public class QueryTestimony : IQueryTestimony
    {
        private readonly IReadTestimony _readTestimony;

        public QueryTestimony(IReadTestimony readTestimony)
        {
            _readTestimony = readTestimony?? throw new ArgumentNullException(nameof(readTestimony));
        }
        public string QueryTestimonyByUser(string connection,int UserID)
        {
            return _readTestimony.ReadTestimonyByUser(connection,UserID);
        }
    }
}
