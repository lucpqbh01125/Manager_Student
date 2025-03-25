using Manager_SIMS.Models;

namespace Manager_SIMS.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _context.Courses.ToList();
        }

        public Course GetCourseById(int id)
        {
            return _context.Courses.Find(id);
        }

        public void AddCourse(Course course)
        {
            try
            {
                Console.WriteLine($"[CourseRepository] Adding Course: {course.CourseName}");
                _context.Courses.Add(course);
                _context.SaveChanges();
                Console.WriteLine("[CourseRepository] Course saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[CourseRepository] Error: " + ex.Message);
            }
        }

        public void UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public void DeleteCourse(int id)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
        }

        
    }
}
