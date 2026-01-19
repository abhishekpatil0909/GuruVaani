using System;
using System.Threading.Tasks;
using Project.Application.DTOs;

namespace Project.Application.Interfaces
{
    public interface IGuruService
    {
        Task<GuruProfileDto> GetByUserIdAsync(Guid userId);
        Task<GuruProfileDto> CreateOrUpdateAsync(GuruProfileDto dto);
        Task VerifyAsync(Guid id, bool isVerified);
    }
}
