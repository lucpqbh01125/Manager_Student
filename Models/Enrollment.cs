using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manager_SIMS.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public EnrollmentStatus Status { get; set; }

        [Required]
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("StudentId")]
        public User Student { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }

    public enum EnrollmentStatus
    {
        Enrolled,
        Completed,
        Dropped
    }
}
