using Manager_SIMS.Models;

namespace Manager_SIMS.Repositories
{
    public interface IGradeRepository
    {
        Task<IEnumerable<Grade>> GetAllGradesAsync();
        Task<Grade?> GetGradeByIdAsync(int id);
        Task<IEnumerable<Grade>> GetGradesByFacultyAsync(int facultyId);
        Task<IEnumerable<Grade>> GetGradesByEnrollmentAsync(int enrollmentId);
        Task AddGradeAsync(Grade grade);
        Task UpdateGradeAsync(Grade grade);
        Task DeleteGradeAsync(int id);



        Task<IEnumerable<object>> GetStudentGradesAsync(int studentId);
    }
}
