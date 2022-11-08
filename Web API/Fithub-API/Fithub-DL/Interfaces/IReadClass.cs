using Fithub_Data.Models;
using System.Collections.Generic;

namespace Fithub_DL.Interfaces
{
    public interface IReadClass
    {
        public IEnumerable<Class> ReadClasses(string connection);
    }
}
