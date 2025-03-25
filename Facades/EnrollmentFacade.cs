using Manager_SIMS.Models;
using Manager_SIMS.Repositories;

namespace Manager_SIMS.Facades
{
    public class EnrollmentFacade : IEnrollmentFacade
    {
        private readonly IEnrollmentRepository _enrollmentRepo;
        private readonly IEnrollmentRepository _enrollmentRepository;


        public EnrollmentFacade(IEnrollmentRepository enrollmentRepo, IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepo = enrollmentRepo;
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<bool> AssignStudentToCourseAsync(int studentId, int courseId)
        {
            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                Status = EnrollmentStatus.Enrolled,
                EnrolledAt = DateTime.UtcNow
            };

            return await _enrollmentRepo.AddEnrollmentAsync(enrollment);
        }

        public async Task<bool> RemoveEnrollmentAsync(int enrollmentId)
        {
            return await _enrollmentRepo.RemoveEnrollmentAsync(enrollmentId);
        }

        public async Task<List<Enrollment>> GetAllEnrollmentsAsync()
        {
            return await _enrollmentRepo.GetAllEnrollmentsAsync();
        }




        public async Task<IEnumerable<Course>> GetStudentCoursesAsync(int studentId)
        {
            return await _enrollmentRepo.GetStudentCoursesAsync(studentId);
        }
    }
}
