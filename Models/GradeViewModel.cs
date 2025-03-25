namespace Manager_SIMS.Models
{
    public class GradeViewModel
    {
        public IEnumerable<Grade> Grades { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; } // Thêm bảng Enrollment
    }
}
