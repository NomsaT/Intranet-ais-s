using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public static class Permissions
    {
        public static List<DAL.DTO.PermissionData> getRolePermissions(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Permissions
               .Select(p => new DAL.DTO.PermissionData
               {
                   PermissionId = p.Id,
                   Page = p.Page,
                   Component = p.Component,
                   Description = p.Description

               }).OrderBy(p => p.Page).ToList(); 

            var source2 = db.RolePermissions.Where(d => d.RoleId == id)
                .Select(s => s.PermissionId).ToList();

            for (int i = 0; i < source.Count(); i++)
            {
                if (source2.Contains(source[i].PermissionId))
                {
                    source[i].PermissionChecked = true;
                }
                else
                {
                    source[i].PermissionChecked = false;
                }

            }
            return source;
        }

        public static void AssignRolesPermissions(int roleId, List<DAL.DTO.PermissionData> rolePermissions)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.RolePermissions
                .Where(r => r.RoleId == roleId);

            db.RolePermissions.RemoveRange(source);

            List<Models.RolePermission> roleModel = new List<Models.RolePermission>();
            foreach (var rp in rolePermissions)
            {
                if (rp.PermissionChecked)
                {
                    roleModel.Add(new Models.RolePermission
                    {
                        RoleId = roleId,
                        PermissionId = rp.PermissionId
                    });
                }
            }

            db.RolePermissions.AddRange(roleModel);
            db.SaveChanges();

        }
        public static List<DAL.DTO.PermissionData> getUserPermissions(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Permissions
               .Select(p => new DAL.DTO.PermissionData
               {
                   PermissionId = p.Id,
                   Page = p.Page,
                   Component = p.Component,
                   Description = p.Description

               }).OrderBy(p => p.Page).ToList();

            var source2 = db.UserPermissions.Where(d => d.UserId == id)
                .Select(s => s.PermissionId).ToList();

            for (int i = 0; i < source.Count(); i++)
            {
                if (source2.Contains(source[i].PermissionId))
                {
                    source[i].PermissionChecked = true;
                }
                else
                {
                    source[i].PermissionChecked = false;
                }

            }
            return source;
        }

        public static void AssignUserPermissions(int userId, List<DAL.DTO.PermissionData> permissions)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.UserPermissions
                .Where(r => r.UserId == userId);

            db.UserPermissions.RemoveRange(source);

            List<Models.UserPermission> model = new List<Models.UserPermission>();
            foreach (var p in permissions)
            {
                if (p.PermissionChecked)
                {
                    model.Add(new Models.UserPermission
                    {
                        UserId = userId,
                        PermissionId = p.PermissionId
                    });
                }
            }

            db.UserPermissions.AddRange(model);
            db.SaveChanges();

        }
    }
}
