using Manager_SIMS.Models;

namespace Manager_SIMS.Facades
{
    public interface ICourseFacade
    {
        IEnumerable<Course> GetAllCourses();
        Course GetCourseById(int id);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int id);
    }
}
