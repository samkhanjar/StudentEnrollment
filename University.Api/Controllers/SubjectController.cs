using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using University.Api.ErrorHandling;
using University.Service.Entities;
using University.Service.Interfaces;

namespace University.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : BaseApiController
    {
        private readonly ISubjectService SubjectService;

        private readonly IConfiguration Configuration;

        public SubjectController(ISubjectService subjectService, IConfiguration configuration)
        {
            SubjectService = subjectService;
            Configuration = configuration;
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<ApiResponse> Get([FromQuery]int id)
        {
            return await ExecuteWithErrorHandlingAsync(async () =>
            {
                return Success(await SubjectService.GetSubjectById(id));
            });
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ApiResponse> Get()
        {
            return await ExecuteWithErrorHandlingAsync(() =>
            {
                return Success(SubjectService.GetAllSubjects());
            });
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ApiResponse> UpdateSubject([FromBody]Subject subject)
        {
            if (ModelState.IsValid)
            {
                return await ExecuteWithErrorHandlingAsync(() =>
                {
                    var response = SubjectService.UpdateSubject(subject).Result;

                    if (response)
                    {
                        return Success("Subject updated successfully.");
                    }

                    return Failed("Update failed!");
                });
            }

            return Failed("Invalid Request!");
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<ApiResponse> InsertSubject([FromBody]Subject subject)
        {
            if (ModelState.IsValid)
            {
                return await ExecuteWithErrorHandlingAsync(() =>
                {
                    var response = SubjectService.InsertSubject(subject).Result;

                    if (response)
                    {
                        return Success("Subject added successfully.");
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
            return await ExecuteWithErrorHandlingAsync(() =>
            {
                var response = SubjectService.DeleteSubject(id).Result;

                if (response)
                {
                    return Success("Subject deleted successfully!");
                }

                return Failed("Subject is not found!");
            });
        }
    }
}
