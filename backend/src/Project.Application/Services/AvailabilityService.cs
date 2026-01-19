using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Project.Application.DTOs;
using Project.Application.Interfaces;
using Project.Domain.Entities;

namespace Project.Application.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IRepository<AvailabilitySlot> _repo;
        private readonly IMapper _mapper;

        public AvailabilityService(IRepository<AvailabilitySlot> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<AvailabilitySlotDto> CreateAsync(AvailabilitySlotDto dto)
        {
            var entity = _mapper.Map<AvailabilitySlot>(dto);
            await _repo.AddAsync(entity);
            return _mapper.Map<AvailabilitySlotDto>(entity);
        }

        public async Task<IEnumerable<AvailabilitySlotDto>> ListByGuruAsync(Guid guruId)
        {
            var all = await _repo.ListAsync();
            var items = all.Where(x => x.GuruId == guruId);
            return items.Select(x => _mapper.Map<AvailabilitySlotDto>(x));
        }
    }
}
