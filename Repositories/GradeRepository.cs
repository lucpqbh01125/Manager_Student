using Manager_SIMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager_SIMS.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly ApplicationDbContext _context;

        public GradeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Grade>> GetAllGradesAsync()
        {
            return await _context.Grades.Include(g => g.Faculty)
                                        .Include(g => g.Enrollment)
                                        .ToListAsync();
        }

        public async Task<Grade?> GetGradeByIdAsync(int id)
        {
            return await _context.Grades.FindAsync(id);
        }

        public async Task<IEnumerable<Grade>> GetGradesByFacultyAsync(int facultyId)
        {
            return await _context.Grades.Where(g => g.FacultyId == facultyId).ToListAsync();
        }

        public async Task<IEnumerable<Grade>> GetGradesByEnrollmentAsync(int enrollmentId)
        {
            return await _context.Grades.Where(g => g.EnrollmentId == enrollmentId).ToListAsync();
        }

        public async Task AddGradeAsync(Grade grade)
        {
            await _context.Grades.AddAsync(grade);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGradeAsync(Grade grade)
        {
            _context.Grades.Update(grade);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGradeAsync(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Grade>> GetGradesByStudentId(int studentId)
        {
            return await _context.Grades
                .Include(g => g.Enrollment)
                .ThenInclude(e => e.Course)
                .Where(g => g.Enrollment.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<IEnumerable<object>> GetStudentGradesAsync(int studentId)
        {
            return await _context.Grades
                .Where(g => _context.Enrollments
                    .Where(e => e.StudentId == studentId)
                    .Select(e => e.EnrollmentId)
                    .Contains(g.EnrollmentId))
                .Include(g => g.Enrollment)
                    .ThenInclude(e => e.Course)
                .Select(g => new
                {
                    CourseName = g.Enrollment.Course.CourseName,
                    Score = g.Score,
                    Feedback = g.Feedback
                })
                .ToListAsync();
        }
    }
}

