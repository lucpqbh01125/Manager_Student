using Manager_SIMS.Models;

namespace Manager_SIMS.Facades
{
    public interface IEnrollmentFacade
    {
        Task<bool> AssignStudentToCourseAsync(int studentId, int courseId);
        Task<bool> RemoveEnrollmentAsync(int enrollmentId);
        Task<List<Enrollment>> GetAllEnrollmentsAsync();

        Task<IEnumerable<Course>> GetStudentCoursesAsync(int studentId);

    }
}
