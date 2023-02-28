using CafeForGames.Models;
using CafeForGames.Models.DTO;

namespace CafeForGames.Services.IRepository
{
    public interface IUserService
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LocalUser> Register(RegisterRequestDTO registerRequestDTO);
    }
}
