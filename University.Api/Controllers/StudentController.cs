using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using University.Api.ErrorHandling;
using University.Common.Requests;
using University.Service.Interfaces;

namespace University.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : BaseApiController
    {
        private readonly IStudentService StudentService;

        private readonly IConfiguration Configuration;

        public StudentController(IStudentService studentService, IConfiguration configuration)
        {
            StudentService = studentService;
            Configuration = configuration;
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<ApiResponse> Get([FromQuery]int id)
        {
            return await ExecuteWithErrorHandlingAsync(async () =>
            {
                return Success(await StudentService.GetStudentById(id));
            });            
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ApiResponse> Get()
        {
            return await ExecuteWithErrorHandlingAsync(async () =>
            {
                return Success(await StudentService.GetAllStudents());
            });            
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ApiResponse> UpdateStudent([FromBody]StudentRequest student)
        {
            if (ModelState.IsValid)
            {
                return await ExecuteWithErrorHandlingAsync(async () =>
                {
                    var response = await StudentService.UpdateStudent(student);

                    if (response)
                    {
                        return Success("Student updated successfully.");
                    }
                    
                    return Failed("Update failed!");                    
                });
            }
            
            return Failed("Invalid Request!");
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<ApiResponse> InsertStudent([FromBody]StudentRequest student)
        {
            if (ModelState.IsValid)
            {
                return await ExecuteWithErrorHandlingAsync(async () =>
                {
                    var response = await StudentService.InsertStudent(student);

                    if (response)
                    {
                        return Success("Studnet added successfully.");
                    }

                    return Failed("Insert failed!");
                });
            }

            return Failed("Invalid Request!");
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ApiResponse> RemoveStudent(int id)
        {            
            return await ExecuteWithErrorHandlingAsync(async () =>
            {
                var response = await StudentService.DeleteStudent(id);

                if (response)
                {
                    return Success("Student deleted successfully!");
                }

                return Failed("Student ID is invalid!");
            });
        }
    }
}
