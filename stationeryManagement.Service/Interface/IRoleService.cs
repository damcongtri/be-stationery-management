using stationeryManagement.Data.Dto;
using stationeryManagement.Data;
using stationeryManagement.Data.Dto.RoleDto;

namespace stationeryManagement.Service.Interface
{
    public interface IRoleService
    {

        // Create
        Task<Role> CreateRole(RoleDto role);
        // Read
        Task<Role?> GetRoleById(Guid roleId);
        Task<IEnumerable<Role>> GetAllRole();

        // Update
        Task<bool> UpdateRole(RoleDto role, Guid id);

        // Delete
        Task<bool> DeleteRole(Guid roleId);
    }
}
