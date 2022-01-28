using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SalesRepDAL.Entities
{
    public class DbRole : IdentityRole<long>
    {
        public virtual ICollection<DbUserRole> UserRoles { get; set; }

    }
}
