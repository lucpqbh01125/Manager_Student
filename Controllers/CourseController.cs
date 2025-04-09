using Manager_SIMS.Facades;
using Manager_SIMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace Manager_SIMS.Controllers
{
    public class CourseController : Controller
    {
      
        private readonly ICourseFacade _courseFacade;

        public CourseController(ICourseFacade courseFacade)
        {
            _courseFacade = courseFacade;
        }

        public IActionResult List()
        {
            var courses = _courseFacade.GetAllCourses();
            return View(courses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("Model is valid. Saving course: " + course.CourseName);
                _courseFacade.AddCourse(course);
                return RedirectToAction("List");
            }
            else
            {
                Console.WriteLine("ModelState is NOT valid!");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine("Validation Error: " + error.ErrorMessage);
                }
            }
            return View(course);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _courseFacade.GetCourseById(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseFacade.UpdateCourse(course);
                return RedirectToAction("List");
            }
            return View(course);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var course = _courseFacade.GetCourseById(id);
            if (course == null) return NotFound();

            _courseFacade.DeleteCourse(id);

            // Quay về danh sách ngay sau khi xóa
            return RedirectToAction("List");
        }

    }
}

