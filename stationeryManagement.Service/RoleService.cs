using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stationeryManagement.Data;
using stationeryManagement.Data.Dto;
using stationeryManagement.Service.Common;
using stationeryManagement.Service.Interface;

namespace stationeryManagement.Service
{
    public class RoleService : EntityService<Role>, IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.RoleRepository)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Role> CreateRole(RoleDto roleDto)
        {
           var entity= new Role()
            {
               RoleName=roleDto.RoleName,
               ThresholdAmountPerMonth=roleDto.ThresholdAmountPerMonth,
            };
            var result = await _unitOfWork.RoleRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return result;

        }
        public async Task<Role?> GetRoleById(int roleId)
        {
            return await _unitOfWork.RoleRepository.GetByIdAsync(roleId);
        }
        public async Task<IEnumerable<Role>> GetAllRole()
        {
            return await _unitOfWork.RoleRepository.GetAll().ToListAsync();
        }
        public async Task<bool> UpdateRole(RoleDto roleDto, int id)
        {
            var role = new Role()
            {
               RoleId=id,
               RoleName=roleDto.RoleName,
               ThresholdAmountPerMonth=roleDto.ThresholdAmountPerMonth

            };
            _unitOfWork.RoleRepository.UpdateAsync(role);
           

            return await _unitOfWork.CommitAsync() > 0;
        }
        public async Task<bool> DeleteRole(int roleId)
        {
            var role = await _unitOfWork.RoleRepository.GetByIdAsync(roleId);
            if (role != null)
            {
                _unitOfWork.RoleRepository.Delete(role);
            }

            return await _unitOfWork.CommitAsync() > 0;
        }

     
    }
}
