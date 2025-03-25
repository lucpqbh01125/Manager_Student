using System;
using System.ComponentModel.DataAnnotations;

namespace Manager_SIMS.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required, MaxLength(255)]
        public string CourseName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required, Range(1, 10)]
        public int Credits { get; set; }

        [Required]
        public string Semester { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
