using Manager_SIMS.Controllers;
using Manager_SIMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Manager_SIMS.Tests
{
    public class GradeControllerTests
    {
        private GradeController GetControllerWithContext(ApplicationDbContext context, string userId = "1")
        {
            var controller = new GradeController(context);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            return controller;
        }

        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GradeTestDb")
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task List_ReturnsViewWithViewModel()
        {
            var context = GetInMemoryDbContext();
            context.Grades.Add(new Grade { Score = 90, EnrollmentId = 1, FacultyId = 1 });
            context.Enrollments.Add(new Enrollment { EnrollmentId = 1, Course = new Course { CourseName = "Math" }, Student = new User { FullName = "John" } });
            await context.SaveChangesAsync();

            var controller = GetControllerWithContext(context);
            var result = await controller.List();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.Model);
        }

        [Fact]
        public void Create_Get_ReturnsViewWithEnrollments()
        {
            var context = GetInMemoryDbContext();
            context.Enrollments.Add(new Enrollment { EnrollmentId = 1, Student = new User { FullName = "Alice" }, Course = new Course { CourseName = "History" } });
            context.SaveChanges();

            var controller = GetControllerWithContext(context);
            var result = controller.Create();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.True(viewResult.ViewData.ContainsKey("Enrollments"));
        }

        [Fact]
        public async Task Create_Post_ValidModel_RedirectsToList()
        {
            var context = GetInMemoryDbContext();
            var controller = GetControllerWithContext(context);

            var grade = new Grade { Score = 88, EnrollmentId = 1 };
            var result = await controller.Create(grade);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("List", redirect.ActionName);
        }

        [Fact]
        public async Task Create_Post_InvalidModel_ReturnsView()
        {
            var context = GetInMemoryDbContext();
            var controller = GetControllerWithContext(context);
            controller.ModelState.AddModelError("Score", "Required");

            var grade = new Grade();
            var result = await controller.Create(grade);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Edit_Get_ReturnsViewWithGrade()
        {
            var context = GetInMemoryDbContext();
            context.Grades.Add(new Grade { GradeId = 10, Score = 75, EnrollmentId = 1 });
            await context.SaveChangesAsync();

            var controller = GetControllerWithContext(context);
            var result = await controller.Edit(10);

            var view = Assert.IsType<ViewResult>(result);
            Assert.IsType<Grade>(view.Model);
        }

        [Fact]
        public async Task Edit_Get_InvalidId_ReturnsNotFound()
        {
            var controller = GetControllerWithContext(GetInMemoryDbContext());
            var result = await controller.Edit(99);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_Post_ValidModel_Redirects()
        {
            var context = GetInMemoryDbContext();
            context.Grades.Add(new Grade { GradeId = 1, Score = 65, EnrollmentId = 1 });
            await context.SaveChangesAsync();

            var controller = GetControllerWithContext(context);
            var updated = new Grade { GradeId = 1, Score = 95, EnrollmentId = 1 };

            var result = await controller.Edit(updated);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("List", redirect.ActionName);
        }

        [Fact]
        public async Task Edit_Post_InvalidModel_ReturnsView()
        {
            var controller = GetControllerWithContext(GetInMemoryDbContext());
            controller.ModelState.AddModelError("Score", "Required");

            var grade = new Grade();
            var result = await controller.Edit(grade);

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Delete_ValidId_DeletesAndRedirects()
        {
            var context = GetInMemoryDbContext();
            context.Grades.Add(new Grade { GradeId = 1, Score = 45, EnrollmentId = 1 });
            await context.SaveChangesAsync();

            var controller = GetControllerWithContext(context);
            var result = await controller.Delete(1);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("List", redirect.ActionName);
            Assert.Empty(context.Grades);
        }

        [Fact]
        public async Task Delete_InvalidId_ReturnsNotFound()
        {
            var controller = GetControllerWithContext(GetInMemoryDbContext());
            var result = await controller.Delete(123);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}