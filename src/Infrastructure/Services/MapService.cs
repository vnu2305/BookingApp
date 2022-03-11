﻿using Application.DTOs.MapDTO;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Pagination;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class MapService : IMapService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MapService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(AddMapDTO mapDTO)
        {
            Map map = _mapper.Map<Map>(mapDTO);
            await _unitOfWork.Maps.AddAsync(map);
            await _unitOfWork.CompleteAsync();
            return map.Id;
        }

        public async Task<PagedList<GetMapDTO>> GetPagedAsync(PagedQueryBase query)
        {
            var maps = await _unitOfWork.Maps.GetPagedAsync(query);
            var mapsDTO = new PagedList<GetMapDTO>(_mapper.Map<List<GetMapDTO>>(maps), maps.TotalCount, maps.CurrentPage, maps.PageSize);
            return mapsDTO;
        }

        public async Task<GetMapDTO> GetByIdAsync(Guid Id)
        {
            var map = await _unitOfWork.Maps.GetByIdAsync(Id);
            return _mapper.Map<GetMapDTO>(map);
        }

        public async Task<bool> RemoveAsync(Guid Id)
        {
            var map = await _unitOfWork.Maps.GetByIdAsync(Id);
            if (map == null)
                return false;

            _unitOfWork.Maps.Remove(map);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<GetMapDTO>> SearchByOfficeIdAsync(Guid OfficeId)
        {
            var maps = _unitOfWork.Maps.Search(c => c.OfficeId.Equals(OfficeId), false);
            if (maps == null)
                return null;

            return _mapper.Map<IEnumerable<GetMapDTO>>(maps);
        }

        public async Task<GetMapDTO> UpdateAsync(UpdateMapDTO mapDTO)
        {
            var map = await _unitOfWork.Maps.GetByIdAsync(mapDTO.Id);
            if (map == null)
                return null;

            _mapper.Map(mapDTO, map);
            _unitOfWork.Maps.Update(map);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<GetMapDTO>(map);
        }
    }
}
