using Fithub_BL.Interfaces;
using Fithub_Data.Models;
using Fithub_DL.Interfaces;
using System;
using System.Collections.Generic;

namespace Fithub_BL
{
    public class QueryClasss : IQueryClass
    {
        private readonly IReadClass _readClass;
        public QueryClasss(IReadClass readClass)
        {
            _readClass = readClass ?? throw new ArgumentNullException(nameof(readClass));
        }
        public IEnumerable<Class> QueryClasses(string connection)
        {
            return _readClass.ReadClasses(connection);
        }
    }
}
