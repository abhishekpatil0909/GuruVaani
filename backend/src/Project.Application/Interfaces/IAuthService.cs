using System.Threading.Tasks;
using Project.Application.DTOs;

namespace Project.Application.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> RegisterAsync(string fullName, string email, string password);
        Task<UserDto> LoginAsync(string email, string password);
    }
}
