using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Manager_SIMS.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required, MaxLength(50)]
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }

    }

}
