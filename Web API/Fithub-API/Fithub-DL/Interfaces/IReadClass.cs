using Fithub_Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_DL.Interfaces
{
  public interface IReadClass
  {
    public IEnumerable<Class> ReadClasses();
  }
}
