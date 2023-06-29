using System.Collections.Generic;

namespace DAL.DTO
{
    public class UpdatePermissions
    {
        public List<DAL.DTO.PermissionData> permissions { get; set; }
        public int roleId { get; set; }
        public int userId { get; set; }
    }
}
