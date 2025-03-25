using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manager_SIMS.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(255)]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public string Address { get; set; }

        [Required, MaxLength(15)] 
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
