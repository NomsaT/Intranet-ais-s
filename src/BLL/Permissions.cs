using System.Collections.Generic;

namespace BLL
{
    public static class Permissions
    {
        public static List<DAL.DTO.PermissionData> getRolePermissions(int id)
        {
            return DAL.Permissions.getRolePermissions(id);
        }

        public static void AssignRolesPermissions(int roleId, List<DAL.DTO.PermissionData> rolePermissions)
        {
            DAL.Permissions.AssignRolesPermissions(roleId, rolePermissions);
        }

        public static List<DAL.DTO.PermissionData> getUserPermissions(int id)
        {
            return DAL.Permissions.getUserPermissions(id);
        }

        public static void AssignUserPermissions(int userId, List<DAL.DTO.PermissionData> permissions)
        {
            DAL.Permissions.AssignUserPermissions(userId, permissions);
        }
    }
}
