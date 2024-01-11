using stationeryManagement.Data.Dto;
using stationeryManagement.Data;

namespace stationeryManagement.Service.Interface
{
    public interface IRoleService
    {

        // Create
        Task<Role> CreateRole(RoleDto role);
        // Read
        Task<Role?> GetRoleById(int roleId);
        Task<IEnumerable<Role>> GetAllRole();

        // Update
        Task<bool> UpdateRole(RoleDto role, int id);

        // Delete
        Task<bool> DeleteRole(int roleId);
    }
}
