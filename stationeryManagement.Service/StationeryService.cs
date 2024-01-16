using Microsoft.EntityFrameworkCore;
using stationeryManagement.Data;
using stationeryManagement.Data.Dto;
using stationeryManagement.Data.Model;
using stationeryManagement.Service.Common;
using stationeryManagement.Service.Interface;

namespace stationeryManagement.Service
{
    public class StationeryService : EntityService<Stationery>, IStationeryService


    {
        private readonly IUnitOfWork _unitOfWork;

        public StationeryService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.StationeryRepository)
        {
            _unitOfWork = unitOfWork;
        }

        
        public async Task<Stationery?> GetStationeryById(int itemId)
        {
            return await _unitOfWork.StationeryRepository.GetByIdAsync(itemId);
        }
        public async Task<IEnumerable<Stationery>> GetAllStationery()
        {
            return await _unitOfWork.StationeryRepository.GetAll().ToListAsync();
        }
        public async Task<bool> UpdateStationery(StationeryDto stationeryDto, int itemid)
        {
            var stationery = new Stationery()
            {
                StationeryId = itemid,
                Name= stationeryDto.Name,
                Description = stationeryDto.Description,
                UnitPrice = stationeryDto.UnitPrice,
                Image = stationeryDto.Image,
                Price = stationeryDto.Price,
                Inventory = stationeryDto.Inventory,
                SupplierId = stationeryDto.SupplierId,
                
              

            };
            _unitOfWork.StationeryRepository.UpdateAsync(stationery);
            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> DeleteStationery(int itemid)
        {
            var stationery = await _unitOfWork.StationeryRepository.GetByIdAsync(itemid);
            if (stationery != null)
            {
                _unitOfWork.StationeryRepository.Delete(stationery);
            }

            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<Stationery> CreateStationery(StationeryDto stationeryDto)
        {

            var entity = new Stationery()
            {
              
                Name = stationeryDto.Name,
                Description = stationeryDto.Description,
                UnitPrice = stationeryDto.UnitPrice,
                Image = stationeryDto.Image,
                Price = stationeryDto.Price,
                Inventory = stationeryDto.Inventory,
                SupplierId = stationeryDto.SupplierId,

            };
            var result = await _unitOfWork.StationeryRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return result;
        }
    }


}
