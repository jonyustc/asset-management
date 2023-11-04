using Core.Model;
using Microsoft.AspNetCore.Identity;

namespace Core.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isActive { get; set; }
        public int CopmanyId { get; set; }
        public string EmployeeCode { get; set; }
        public int AccessPermissionId { get; set; }
        public string ProfileUrl { get; set; }

        public Company Company { get; set; }
    }
}