namespace Fithub_Data.Models
{
    public class Rating
    {
        public int RatingID { get; set; }
        public int CoachID { get; set; }
        public int UserID { get; set; }
        public string RatingValue { get; set; }
    }
}
