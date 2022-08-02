using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_Data.Models
{
    public class UserInfo
    {
        public int UserInfoID { get; set; }

        public int Height { get; set; }
        public int Weight { get; set; }
        public string MobileNo { get; set; }
        public string EmergencyMobileNo { get; set; }
        public int BMI { get; set; }
    }
}
