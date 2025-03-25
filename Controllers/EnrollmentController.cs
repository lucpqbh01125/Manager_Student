using Manager_SIMS.Facades;
using Manager_SIMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Manager_SIMS.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentFacade _enrollmentFacade;
        private readonly ApplicationDbContext _context;

        public EnrollmentController(IEnrollmentFacade enrollmentFacade, ApplicationDbContext context)
        {
            _enrollmentFacade = enrollmentFacade;
            _context = context;
        }
        public IActionResult Details(int id)
        {
            return View("~/Views/Course/Details.cshtml");
        }

        public async Task<IActionResult> List()
        {
            var enrollments = await _enrollmentFacade.GetAllEnrollmentsAsync();
            return View(enrollments);
        }

        public IActionResult Assign()
        {
            ViewBag.Students = new SelectList(_context.Users.Where(u => u.RoleId == 3), "UserId", "FullName");
            ViewBag.Courses = new SelectList(_context.Courses, "CourseId", "CourseName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Assign(int studentId, int courseId)
        {
            if (await _enrollmentFacade.AssignStudentToCourseAsync(studentId, courseId))
            {
                return RedirectToAction("List");
            }
            ModelState.AddModelError("", "Gán sinh viên vào khóa học thất bại.");
            return View();
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _enrollmentFacade.RemoveEnrollmentAsync(id);
            return RedirectToAction("List");
        }
    }
}
