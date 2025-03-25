using Manager_SIMS.Models;
using Manager_SIMS.Repositories;
namespace Manager_SIMS.Facades
{
    public class GradeFacade : IGradeFacade
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IGradeRepository _gradeRepo;
        public GradeFacade(IGradeRepository gradeRepository, IEnrollmentRepository enrollmentRepository, IGradeRepository gradeRepo)
        {
            _gradeRepository = gradeRepository;
            _enrollmentRepository = enrollmentRepository;
            _gradeRepo = gradeRepo;
        }

        public async Task<IEnumerable<Grade>> GetAllGradesAsync()
        {
            return await _gradeRepository.GetAllGradesAsync();
        }

        public async Task<Grade?> GetGradeByIdAsync(int id)
        {
            return await _gradeRepository.GetGradeByIdAsync(id);
        }

        public async Task<IEnumerable<Grade>> GetGradesByFacultyAsync(int facultyId)
        {
            return await _gradeRepository.GetGradesByFacultyAsync(facultyId);
        }

        public async Task AddGradeAsync(int facultyId, int enrollmentId, double score, string feedback)
        {
            var grade = new Grade
            {
                FacultyId = facultyId,
                EnrollmentId = enrollmentId,
                Score = score,
                Feedback = feedback,
                GradedAt = DateTime.UtcNow
            };
            await _gradeRepository.AddGradeAsync(grade);
        }

        public async Task UpdateGradeAsync(int gradeId, double score, string feedback)
        {
            var grade = await _gradeRepository.GetGradeByIdAsync(gradeId);
            if (grade != null)
            {
                grade.Score = score;
                grade.Feedback = feedback;
                await _gradeRepository.UpdateGradeAsync(grade);
            }
        }

        public async Task DeleteGradeAsync(int id)
        {
            await _gradeRepository.DeleteGradeAsync(id);
        }


        public async Task<IEnumerable<object>> GetStudentGradesAsync(int studentId)
        {
            return await _gradeRepo.GetStudentGradesAsync(studentId);
        }
    }
}

