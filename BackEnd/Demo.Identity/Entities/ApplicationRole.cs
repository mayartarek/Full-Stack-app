using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Identity.Entities
{
    public class ApplicationRole:IdentityRole
    {
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

    }
}
