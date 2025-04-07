using Manager_SIMS.Facades;
using Manager_SIMS.Models;
using Manager_SIMS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Manager_SIMS.Controllers
{
    //[Authorize(Roles = "Faculty")]
    public class GradeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GradeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> List()
        {
            //var grades = await _context.Grades.Include(g => g.Faculty).ToListAsync();
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

        public IActionResult Create()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); // Lấy ID giáo viên đăng nhập

            var enrollments = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToList()
                .Select(e => new SelectListItem
                {
                    Value = e.EnrollmentId.ToString(),
                    Text = $"{e.Student.FullName} - {e.Course.CourseName}"
                }).ToList();

            ViewBag.Enrollments = enrollments; // Truyền danh sách Enrollment ra View
            ViewBag.FacultyId = userId; // Gán mặc định FacultyId

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Grade grade)
        {
            if (ModelState.IsValid)
            {
                grade.FacultyId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); // Tự động lấy ID giáo viên đăng nhập
                grade.GradedAt = DateTime.UtcNow;

                _context.Grades.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }

            return View(grade);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null) return NotFound();
            return View(grade);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Grade grade)
        {
            if (ModelState.IsValid)
            {
                _context.Update(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(grade);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();

            return RedirectToAction("List");
        }
    }



}

