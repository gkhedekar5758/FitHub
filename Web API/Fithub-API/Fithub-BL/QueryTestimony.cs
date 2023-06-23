using Fithub_BL.Interfaces;
using Fithub_Data.DTO;
using Fithub_DL.Interfaces;
using System;

namespace Fithub_BL
{
    public class QueryTestimony : IQueryTestimony
    {
        private readonly IReadTestimony _readTestimony;

        public QueryTestimony(IReadTestimony readTestimony)
        {
            _readTestimony = readTestimony ?? throw new ArgumentNullException(nameof(readTestimony));
        }
        public TestimonyDTO QueryTestimonyByUser(string connection, int UserID)
        {
            return _readTestimony.ReadTestimonyByUser(connection, UserID);
        }
    }
}
