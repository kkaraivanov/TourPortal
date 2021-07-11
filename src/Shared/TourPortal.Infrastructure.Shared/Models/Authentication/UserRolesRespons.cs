namespace TourPortal.Infrastructure.Shared.Models.Authentication
{
    using System.Collections.Generic;

    public class UserRolesRespons
    {
        public IEnumerable<UserRoleModel> UserRoles { get; set; }
    }

    public class UserRoleModel
    {
        public string RoleId { get; set; }

        public string RoleName { get; set; }
    }
}