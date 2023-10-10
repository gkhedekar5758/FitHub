using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fithub_Data.DTO.ResponseDTO
{
    public class CoachRatingResponseDTO
    {
        public string CoachName { get; set; }
        public string Rating { get; set; }
        public string PhotoURL { get; set; }
        public int UserID { get; set; }
        public int CoachID { get; set; }
    }
}
