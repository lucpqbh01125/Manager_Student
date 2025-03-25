using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manager_SIMS.Models
{
    public class Grade
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GradeId { get; set; }

        [Required]
        [Range(0, 100)]
        public double Score { get; set; }

        [StringLength(255)]
        public string? Feedback { get; set; }

        [Required] // ✅ Bắt buộc nhập giá trị, tránh NULL
        public DateTime GradedAt { get; set; } = DateTime.UtcNow;

        // Khóa ngoại
        public int EnrollmentId { get; set; }
        public int FacultyId { get; set; }

        // Navigation Properties
        [ForeignKey("EnrollmentId")]
        public virtual Enrollment? Enrollment { get; set; }

        [ForeignKey("FacultyId")]
        public virtual User? Faculty { get; set; }

        // Khi gán Enrollment, tự động cập nhật EnrollmentId
        public void SetEnrollment(Enrollment enrollment)
        {
            Enrollment = enrollment;
            EnrollmentId = enrollment.EnrollmentId;
        }

        // Khi gán Faculty, tự động cập nhật FacultyId
        public void SetFaculty(User faculty)
        {
            Faculty = faculty;
            FacultyId = faculty.UserId;
        }
    }
}
