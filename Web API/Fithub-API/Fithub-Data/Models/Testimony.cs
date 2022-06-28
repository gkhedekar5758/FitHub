using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_Data.Models
{
    public class Testimony
    {
        public int TestimonyID { get; set; }
        public string UserTestimony { get; set; }
        public bool Approved { get; set; }
        public int UserID { get; set; }
    }
}
