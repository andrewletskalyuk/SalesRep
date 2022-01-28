using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesRepDAL.Entities
{
    public class DbUser : IdentityUser<long>
    {
        [Range(18, 99, ErrorMessage = "Incorrect age of user!!!")]
        public int Age { get; set; }
        public virtual ICollection<DbUserRole> UserRoles { get; set; }
    }
}