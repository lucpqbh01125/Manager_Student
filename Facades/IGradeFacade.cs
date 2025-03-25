using Manager_SIMS.Models;

namespace Manager_SIMS.Facades
{
    public interface IGradeFacade
    {
        Task<IEnumerable<Grade>> GetAllGradesAsync();
        Task<Grade?> GetGradeByIdAsync(int id);
        Task<IEnumerable<Grade>> GetGradesByFacultyAsync(int facultyId);
        Task AddGradeAsync(int facultyId, int enrollmentId, double score, string feedback);
        Task UpdateGradeAsync(int gradeId, double score, string feedback);
        Task DeleteGradeAsync(int id);


        Task<IEnumerable<object>> GetStudentGradesAsync(int studentId);

    }
}
