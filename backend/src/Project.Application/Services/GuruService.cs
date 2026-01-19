using System;
using System.Threading.Tasks;
using AutoMapper;
using Project.Application.DTOs;
using Project.Application.Interfaces;
using Project.Domain.Entities;

namespace Project.Application.Services
{
    public class GuruService : IGuruService
    {
        private readonly IRepository<GuruProfile> _repo;
        private readonly IMapper _mapper;

        public GuruService(IRepository<GuruProfile> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<GuruProfileDto> GetByUserIdAsync(Guid userId)
        {
            var all = await _repo.ListAsync();
            var g = System.Linq.Enumerable.FirstOrDefault(all, x => x.UserId == userId);
            if (g == null) throw new InvalidOperationException("Guru profile not found");
            return _mapper.Map<GuruProfileDto>(g);
        }

        public async Task<GuruProfileDto> CreateOrUpdateAsync(GuruProfileDto dto)
        {
            var entity = _mapper.Map<GuruProfile>(dto);
            var existing = await _repo.GetByIdAsync(entity.Id);
            if (existing == null)
            {
                await _repo.AddAsync(entity);
            }
            else
            {
                existing.Expertise = entity.Expertise;
                existing.ExperienceYears = entity.ExperienceYears;
                existing.IsVerified = entity.IsVerified;
                await _repo.UpdateAsync(existing);
            }

            return _mapper.Map<GuruProfileDto>(entity);
        }

        public async Task VerifyAsync(Guid id, bool isVerified)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) throw new InvalidOperationException("Guru profile not found");
            existing.IsVerified = isVerified;
            await _repo.UpdateAsync(existing);
        }
    }
}
