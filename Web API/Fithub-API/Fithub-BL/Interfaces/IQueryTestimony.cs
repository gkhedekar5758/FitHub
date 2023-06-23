using Fithub_Data.DTO;

namespace Fithub_BL.Interfaces
{
    public interface IQueryTestimony
    {
        public TestimonyDTO QueryTestimonyByUser(string connection, int UserID);
    }
}
