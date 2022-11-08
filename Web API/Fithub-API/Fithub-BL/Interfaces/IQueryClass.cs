using Fithub_Data.Models;
using System.Collections.Generic;

namespace Fithub_BL.Interfaces
{
    public interface IQueryClass
    {
        public IEnumerable<Class> QueryClasses(string connection);
    }
}
