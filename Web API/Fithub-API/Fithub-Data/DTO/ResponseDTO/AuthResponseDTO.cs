using Fithub_Data.Models;
using System.Threading.Tasks;

namespace Fithub_Data.DTO.ResponseDTO
{
    public class AuthResponseDTO
    {
        public User User { get; set; }

        public Task<string> Token { get; set; }
    }
}
