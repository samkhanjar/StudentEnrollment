using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Language.Flow;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using University.Api.Controllers;
using University.Api.ErrorHandling;
using University.Common.Requests;
using University.Service;
using University.Service.Entities;
using University.Service.Interfaces;

namespace University.Api.Test
{
    [TestClass]
    public class StudentControllerUnitTests : WebApiUnitTestBase
    {
        private Mock<IStudentService> StudentServiceMock { get; set; }

        private Mock<IConfiguration> ConfigurationMock { get; set; }

        public StudentControllerUnitTests()
        {
            StudentServiceMock = new Mock<IStudentService>();
            ConfigurationMock = new Mock<IConfiguration>();
        }

        [TestMethod]
        public void ShouldInsertStudentDetails()
        {            
            ExecutStudentCreation<Task<ApiResponse>>(
                setup =>
                {
                    setup.ReturnsAsync(true);
                },
                response =>
                {
                    var output = JsonConvert.DeserializeObject<ApiResponse<string>>(JsonConvert.SerializeObject(response.Result));
                    Assert.AreEqual((int)ApiResponseCode.Success, (int)response.Result.ErrorCode);
                    Assert.AreNotEqual((int)ApiResponseCode.Error, (int)response.Result.ErrorCode);
                    Assert.IsNotNull(output);
                    Assert.AreEqual(output.Result, "Studnet added successfully.");
                });
        }

        [TestMethod]
        public void ShouldDeleteStudent()
        {
            ExecutStudentDeletion<Task<ApiResponse>>(
                setup =>
                {
                    setup.ReturnsAsync(true);
                },
                response =>
                {
                    var output = JsonConvert.DeserializeObject<ApiResponse<string>>(JsonConvert.SerializeObject(response.Result));
                    Assert.AreEqual((int)ApiResponseCode.Success, (int)response.Result.ErrorCode);
                    Assert.AreNotEqual((int)ApiResponseCode.Error, (int)response.Result.ErrorCode);
                    Assert.IsNotNull(output);
                    Assert.AreEqual(output.Result, "Student deleted successfully!");
                });
        }

        [TestMethod]
        public void ShouldUpdateStudent()
        {            
            ExecutUpdateStudent<Task<ApiResponse>>(
                setup =>
                {
                    setup.ReturnsAsync(true);
                },
                response =>
                {
                    var output = JsonConvert.DeserializeObject<ApiResponse<string>>(JsonConvert.SerializeObject(response.Result));
                    Assert.AreEqual((int)ApiResponseCode.Success, (int)response.Result.ErrorCode);
                    Assert.AreNotEqual((int)ApiResponseCode.Error, (int)response.Result.ErrorCode);
                    Assert.IsNotNull(output);
                    Assert.AreEqual(output.Result, "Student updated successfully.");
                });
        }

        [TestMethod]
        public void ShouldGetListOfStudents()
        {
            var students = new List<Student>()
            {
                new Student()
                {
                    Id = 1,
                    FirstName = "James",
                    LastName = "Bond",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                },
                new Student()
                {
                    Id = 2,
                    FirstName = "David",
                    LastName = "Jones",
                    CreatedDate = DateTime.Now,
                    IsDeleted = false
                }
            };

            ExecutStudentList<Task<ApiResponse>>(
                setup =>
                {
                    setup.ReturnsAsync(students);
                },
                response =>
                {
                    var output = JsonConvert.DeserializeObject<ApiResponse<List<Student>>>(JsonConvert.SerializeObject(response.Result));
                    Assert.AreEqual((int)ApiResponseCode.Success, (int)response.Result.ErrorCode);
                    Assert.AreNotEqual((int)ApiResponseCode.Error, (int)response.Result.ErrorCode);
                    Assert.IsNotNull(output);
                    Assert.AreEqual(output.Result[0].FirstName, students[0].FirstName);
                    Assert.AreEqual(output.Result[0].LastName, students[0].LastName);
                    Assert.AreEqual(output.Result[0].Id, students[0].Id);
                    Assert.AreEqual(output.Result[1].FirstName, students[1].FirstName);
                    Assert.AreEqual(output.Result[1].LastName, students[1].LastName);
                    Assert.AreEqual(output.Result[1].Id, students[1].Id);
                });
        }

        private void ExecutStudentList<TResponse>(Action<ISetup<IStudentService, Task<List<Student>>>> setup, Action<TResponse> callback) where TResponse : class
        {
            var setupStudentList = StudentServiceMock.Setup(x => x.GetAllStudents());

            if (setup != null)
            {
                setup(setupStudentList);
            }

            var controller = new StudentController(StudentServiceMock.Object, ConfigurationMock.Object);

            var response = controller.Get();

            callback(response as TResponse);
        }

        private void ExecutUpdateStudent<TResponse>(Action<ISetup<IStudentService, Task<bool>>> setup, Action<TResponse> callback) where TResponse : class
        {
            var dataQuery = new StudentRequest()
            {
                Id = It.IsAny<int>(),
                FirstName = It.IsAny<string>(),
                LastName = It.IsAny<string>()
            };

            var setupUpdateStudent = StudentServiceMock.Setup(x => x.UpdateStudent(dataQuery));

            if (setup != null)
            {
                setup(setupUpdateStudent);
            }

            var controller = new StudentController(StudentServiceMock.Object, ConfigurationMock.Object);

            var response = controller.UpdateStudent(dataQuery);

            callback(response as TResponse);
        }

        private void ExecutStudentCreation<TResponse>(Action<ISetup<IStudentService, Task<bool>>> setup, Action<TResponse> callback) where TResponse : class
        {            
            var dataQuery = new StudentRequest()
            {
                FirstName = It.IsAny<string>(),
                LastName = It.IsAny<string>(),
                Id = 0
            };

            var setupInsertStudent = StudentServiceMock.Setup(x => x.InsertStudent(dataQuery));

            if (setup != null)
            {
                setup(setupInsertStudent);
            }

            var controller = new StudentController(StudentServiceMock.Object, ConfigurationMock.Object);

            var response = controller.InsertStudent(dataQuery);

            callback(response as TResponse);
        }        

        private void ExecutStudentDeletion<TResponse>(Action<ISetup<IStudentService, Task<bool>>> setup, Action<TResponse> callback) where TResponse : class
        {
            const int studentId = 5;

            var setupDeleteStudent = StudentServiceMock.Setup(x => x.DeleteStudent(It.IsAny<int>()));

            if (setup != null)
            {
                setup(setupDeleteStudent);
            }

            var controller = new StudentController(StudentServiceMock.Object, ConfigurationMock.Object);

            var response = controller.RemoveStudent(studentId);

            callback(response as TResponse);
        }
    }
}
