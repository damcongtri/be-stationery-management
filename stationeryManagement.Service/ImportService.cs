using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using stationeryManagement.Data;
using stationeryManagement.Data.Common.BaseRepository;
using stationeryManagement.Data.Dto;
using stationeryManagement.Data.Model;
using stationeryManagement.Service.Common;
using stationeryManagement.Service.Interface;

namespace stationeryManagement.Service
{
    [Authorize]
    public class ImportService : EntityService<Import>, IImportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImportService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.ImportRepository)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Import?> CreateImport(ImportDto import, Guid userId)
        {
            var newImport = new Import()
            {
                UserCreateId = userId,
            };
            var importR = await _unitOfWork.ImportRepository.AddAsync(newImport);

            await _unitOfWork.CommitAsync();
            foreach (var importD in import.ImportDetails.Select(item => new ImportDetail()
                     {
                         ImportId = importR.ImportId,
                         StationeryId = item.StationeryId,
                         Quantity = item.Quantity,
                     }))
            {
                var rrd = await _unitOfWork.ImportDetailRepository.AddAsync(importD);
                importR.ImportDetails.Add(rrd);
                var findStationery = await _unitOfWork.StationeryRepository.GetByIdAsync(importD.StationeryId);
                findStationery.Inventory += importD.Quantity;
                 _unitOfWork.StationeryRepository.UpdateAsync(findStationery);
            }

            return await _unitOfWork.CommitAsync() > 0 ? importR : null;
        }

        public async Task<bool> DeleteImport(int importId)
        {
            var delete = await _unitOfWork.ImportRepository.GetByIdAsync(importId);
            if (delete != null)
            {
                _unitOfWork.ImportRepository.Delete(delete);
            }

            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<IEnumerable<Import>> GetAllImport()
        {
            return await _unitOfWork.ImportRepository.GetAll().ToListAsync();
        }

        public async Task<Import?> GetImportId(int importId)
        {
            return await _unitOfWork.ImportRepository.GetByIdAsync(importId);
        }

        public async Task<bool> UpdateImport(ImportDto import, Guid id, int importId)
        {
            var update = new Import()
            {
                ImportId = importId,
                UserCreateId = id
            };
            _unitOfWork.ImportRepository.UpdateAsync(update);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}