using Microsoft.EntityFrameworkCore;
using stationeryManagement.Data;
using stationeryManagement.Data.Dto;
using stationeryManagement.Data.Dto.StationeryDto;
using stationeryManagement.Data.Model;
using stationeryManagement.Service.Common;
using stationeryManagement.Service.Interface;
using stationeryManagement.Service.Utils;

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
            return await _unitOfWork.StationeryRepository.GetWithCategoryAndSupplier().ToListAsync();
        }
        public async Task<bool> UpdateStationery(StationeryDto stationeryDto, int stationeryId)
        {
            var findStationery = await _unitOfWork.StationeryRepository.GetByIdAsync(stationeryId);
            if (findStationery == null) return false;
            if (stationeryDto.FileUpload != null && stationeryDto.FileUpload.Length > 0)
            {
                var path = await FileUtils.AddFile("stationery", stationeryDto.FileUpload);
                if (!string.IsNullOrWhiteSpace(findStationery.Image))
                {
                    FileUtils.RemoveFile(findStationery.Image);
                }

                findStationery.Image = path;
            }    
            else
            {
                stationeryDto.Image = "/images/stationery/default_stationery.jpg";
            }

            findStationery.Name = stationeryDto.Name;
            findStationery.Description = stationeryDto.Description;
            findStationery.UnitPrice = stationeryDto.UnitPrice;
            findStationery.Price = stationeryDto.Price;
            findStationery.Inventory = stationeryDto.Inventory;
            findStationery.SupplierId = stationeryDto.SupplierId;
            findStationery.CategoryId = stationeryDto.CategoryId;
            _unitOfWork.StationeryRepository.UpdateAsync(findStationery);

            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> DeleteStationery(int stationeryId)
        {
            var stationery = await _unitOfWork.StationeryRepository.GetByIdAsync(stationeryId);
            if (stationery != null)
            {
                _unitOfWork.StationeryRepository.Delete(stationery);
                var result = await _unitOfWork.CommitAsync() > 0;
                if (!result) return result;
                if (!string.IsNullOrWhiteSpace(stationery.Image))
                {
                    FileUtils.RemoveFile(stationery.Image);
                }
                return result;
            }
            else
            {
                return false;
            }
        }

        public async Task<Stationery> CreateStationery(StationeryDto stationeryDto)
        {
            if (stationeryDto.FileUpload != null && stationeryDto.FileUpload.Length > 0)
            {
                var path = await FileUtils.AddFile("stationery", stationeryDto.FileUpload);
                stationeryDto.Image = path;
            }
            else
            {
                stationeryDto.Image = "/images/stationery/default_stationery.jpg";
            }
            var entity = new Stationery()
            {
                Name = stationeryDto.Name,
                Description = stationeryDto.Description,
                UnitPrice = stationeryDto.UnitPrice,
                Image = stationeryDto.Image,
                Price = stationeryDto.Price,
                Inventory = stationeryDto.Inventory,
                SupplierId = stationeryDto.SupplierId,
                CategoryId = stationeryDto.CategoryId
            };
            var result = await _unitOfWork.StationeryRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return result;
        }
    }


}
