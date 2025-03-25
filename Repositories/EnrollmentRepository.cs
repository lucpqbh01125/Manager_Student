using Manager_SIMS.Models;
using Microsoft.EntityFrameworkCore;

namespace Manager_SIMS.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Enrollment>> GetAllEnrollmentsAsync()
        {
            return await _context.Enrollments.Include(e => e.Student).Include(e => e.Course).ToListAsync();
        }

        public async Task<Enrollment> GetEnrollmentByIdAsync(int enrollmentId)
        {
            return await _context.Enrollments.FindAsync(enrollmentId);
        }

        public async Task<List<Enrollment>> GetEnrollmentsByStudentIdAsync(int studentId)
        {
            return await _context.Enrollments.Where(e => e.StudentId == studentId).ToListAsync();
        }

        public async Task<bool> AddEnrollmentAsync(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveEnrollmentAsync(int enrollmentId)
        {
            var enrollment = await _context.Enrollments.FindAsync(enrollmentId);
            if (enrollment == null) return false;

            _context.Enrollments.Remove(enrollment);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<List<Course>> GetCoursesByStudentId(int studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Select(e => e.Course)
                .ToListAsync();
        }




        public async Task<IEnumerable<Course>> GetStudentCoursesAsync(int studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Course)
                .Select(e => e.Course)
                .ToListAsync();
        }
    }
}
