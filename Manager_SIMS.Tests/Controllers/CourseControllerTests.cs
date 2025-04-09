using Manager_SIMS.Controllers;
using Manager_SIMS.Facades;
using Manager_SIMS.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Manager_SIMS.Manager_SIMS.Tests.Controllers
{
    public class CourseControllerTests
    {
        private readonly Mock<ICourseFacade> _mockFacade;
        private readonly CourseController _controller;

        public CourseControllerTests()
        {
            _mockFacade = new Mock<ICourseFacade>();
            _controller = new CourseController(_mockFacade.Object);
        }

        [Fact]
        public void List_ShouldReturnViewWithCourses()
        {
            // Arrange
            var courses = new List<Course> {
                new Course { CourseId = 1, CourseName = "Math" },
                new Course { CourseId = 2, CourseName = "Physics" }
            };
            _mockFacade.Setup(f => f.GetAllCourses()).Returns(courses);

            // Act
            var result = _controller.List() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Course>>(result.Model);
            Assert.Equal(2, ((List<Course>)result.Model).Count);
        }

        [Fact]
        public void Create_Get_ShouldReturnView()
        {
            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_Post_ShouldRedirectToList_WhenModelIsValid()
        {
            // Arrange
            var course = new Course { CourseId = 1, CourseName = "New Course" };

            // Act
            var result = _controller.Create(course) as RedirectToActionResult;

            // Assert
            _mockFacade.Verify(f => f.AddCourse(course), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("List", result.ActionName);
        }

        [Fact]
        public void Create_Post_ShouldReturnView_WhenModelIsInvalid()
        {
            // Arrange
            var course = new Course();
            _controller.ModelState.AddModelError("CourseName", "Required");

            // Act
            var result = _controller.Create(course) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(course, result.Model);
        }

        [Fact]
        public void Edit_Get_ShouldReturnView_WhenCourseExists()
        {
            // Arrange
            var course = new Course { CourseId = 1, CourseName = "Math" };
            _mockFacade.Setup(f => f.GetCourseById(1)).Returns(course);

            // Act
            var result = _controller.Edit(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(course, result.Model);
        }

        [Fact]
        public void Edit_Get_ShouldReturnNotFound_WhenCourseNotFound()
        {
            // Arrange
            _mockFacade.Setup(f => f.GetCourseById(999)).Returns((Course)null);

            // Act
            var result = _controller.Edit(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Post_ShouldRedirectToList_WhenModelIsValid()
        {
            // Arrange
            var course = new Course { CourseId = 1, CourseName = "Updated Course" };

            // Act
            var result = _controller.Edit(course) as RedirectToActionResult;

            // Assert
            _mockFacade.Verify(f => f.UpdateCourse(course), Times.Once);
            Assert.Equal("List", result.ActionName);
        }

        [Fact]
        public void Edit_Post_ShouldReturnView_WhenModelIsInvalid()
        {
            // Arrange
            var course = new Course { CourseId = 1 };
            _controller.ModelState.AddModelError("CourseName", "Required");

            // Act
            var result = _controller.Edit(course) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(course, result.Model);
        }

        [Fact]
        public void DeleteConfirmed_ShouldRedirectToList_WhenCourseExists()
        {
            // Arrange
            var course = new Course { CourseId = 1, CourseName = "Math" };
            _mockFacade.Setup(f => f.GetCourseById(1)).Returns(course);

            // Act
            var result = _controller.DeleteConfirmed(1) as RedirectToActionResult;

            // Assert
            _mockFacade.Verify(f => f.DeleteCourse(1), Times.Once);
            Assert.Equal("List", result.ActionName);
        }

        [Fact]
        public void DeleteConfirmed_ShouldReturnNotFound_WhenCourseNotFound()
        {
            // Arrange
            _mockFacade.Setup(f => f.GetCourseById(99)).Returns((Course)null);

            // Act
            var result = _controller.DeleteConfirmed(99);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
