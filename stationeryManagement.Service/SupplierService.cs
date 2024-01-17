using stationeryManagement.Data;
using Microsoft.EntityFrameworkCore;

using stationeryManagement.Data.Dto;
using stationeryManagement.Data.Model;
using stationeryManagement.Service.Common;
using stationeryManagement.Service.Exceptions;
using stationeryManagement.Service.Interface;

namespace stationeryManagement.Service
{
    public class SupplierService : EntityService<Supplier>, ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SupplierService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.SupplierRepository)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Supplier> CreateSupplier(SupplierDto supplierDto)
        {
            var entity = new Supplier()
            {
                Name = supplierDto.Name,
                ContactInfo = supplierDto.ContactInfo,
            };
            var result = await _unitOfWork.SupplierRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return result;

        }
        public async Task<Supplier?> GetSupplierById(int supplierId)
        {
            return await _unitOfWork.SupplierRepository.GetByIdAsync(supplierId);
        }

        public async Task<IEnumerable<Supplier>> GetAllSupplier()
        {
            return await _unitOfWork.SupplierRepository.GetAll().ToListAsync();
        }

        public async Task<bool> UpdateSupplier(SupplierDto supplierDto, int id)
        {
            var supplier = new Supplier()
            {
                SupplierId = id,
                ContactInfo= supplierDto.ContactInfo,
                Name = supplierDto.Name,

            };
            _unitOfWork.SupplierRepository.UpdateAsync(supplier);
            return await _unitOfWork.CommitAsync() >0;
        }

        public async Task<bool> DeleteSupplier(int supplierId)
        {
            var supplier = await _unitOfWork.SupplierRepository.GetByIdAsync(supplierId);
            if (supplier is null)
            {
                throw new NotFoundException("Not Found this suppiler");
            }
            _unitOfWork.SupplierRepository.Delete(supplier);

            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}
