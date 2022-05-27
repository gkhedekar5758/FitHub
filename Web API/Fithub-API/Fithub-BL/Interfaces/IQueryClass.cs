using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fithub_Data.Models;

namespace Fithub_BL.Interfaces
{
  public interface IQueryClass
  {
    public IEnumerable<Class> QueryClasses();
  }
}
