using Manager_SIMS.Models;
using Manager_SIMS.Repositories;

//namespace Manager_SIMS.Facades
//{
//    public class CourseFacade : ICourseFacade
//    {
//        private readonly ICourseRepository _courseRepository;

//        public CourseFacade(ICourseRepository courseRepository)
//        {
//            _courseRepository = courseRepository;
//        }

//        public IEnumerable<Course> GetAllCourses()
//        {
//            return _courseRepository.GetAllCourses();
//        }

//        public Course GetCourseById(int id)
//        {
//            return _courseRepository.GetCourseById(id);
//        }

//        public void AddCourse(Course course)
//        {
//            _courseRepository.AddCourse(course);
//            Console.WriteLine(course.CourseName);
//        }

//        public void UpdateCourse(Course course)
//        {
//            _courseRepository.UpdateCourse(course);
//        }

//        public void DeleteCourse(int id)
//        {
//            _courseRepository.DeleteCourse(id);
//        }
//    }
//}



namespace Manager_SIMS.Facades
{
    public class CourseFacade : ICourseFacade
    {
        private readonly ApplicationDbContext _context;

        public CourseFacade(ApplicationDbContext context)
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
                Console.WriteLine($"[CourseFacade] Adding Course: {course.CourseName}");
                _context.Courses.Add(course);
                _context.SaveChanges();
                Console.WriteLine("[CourseFacade] Course saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[CourseFacade] Error: " + ex.Message);
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
