using Manager_SIMS.Facades;
using Manager_SIMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Manager_SIMS.Controllers
{
    //[Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly IEnrollmentFacade _enrollmentFacade;
        private readonly IGradeFacade _gradeFacade;
        private readonly ApplicationDbContext _context;

        public StudentController(IEnrollmentFacade enrollmentFacade, IGradeFacade gradeFacade, ApplicationDbContext context)
        {
            _enrollmentFacade = enrollmentFacade;
            _gradeFacade = gradeFacade;
            _context = context;
        }

        public async Task<IActionResult> ViewCourse()
        {
            int studentId = GetCurrentUserId();
            var courses = await _enrollmentFacade.GetStudentCoursesAsync(studentId);
           
            return View(courses);
        }

        public async Task<IActionResult> ViewGrade()
        {
            int studentId = GetCurrentUserId();
            var grades = await _gradeFacade.GetStudentGradesAsync(studentId);
            //return View(grades);
            var viewModel = new GradeViewModel
            {
                Grades = _context.Grades
                    .Include(g => g.Enrollment) // Lấy Enrollment
                        .ThenInclude(e => e.Student) // Lấy Student từ Enrollment
                    .Include(g => g.Enrollment)
                        .ThenInclude(e => e.Course) // Lấy Course từ Enrollment
                    .Include(g => g.Faculty) // Lấy Faculty từ User
                    .ToList(),

                Enrollments = _context.Enrollments
                    .Include(e => e.Student)
                    .Include(e => e.Course)
                    .ToList()
            };

            return View(viewModel);
        }

        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }

}
