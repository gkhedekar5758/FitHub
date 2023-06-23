using Fithub_Data.DTO;

namespace Fithub_DL.Interfaces
{
    public interface IReadTestimony
    {
        public TestimonyDTO ReadTestimonyByUser(string connection, int UserID);
    }
}
