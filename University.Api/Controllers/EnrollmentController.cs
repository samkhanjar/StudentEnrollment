using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Api.ErrorHandling;
using University.Service.Interfaces;

namespace University.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : BaseApiController
    {
        private readonly IEnrollmentService EnrollmentService;

        private readonly IConfiguration Configuration;

        public EnrollmentController(IEnrollmentService enrollmentService, IConfiguration configuration)
        {
            EnrollmentService = enrollmentService;
            Configuration = configuration;
        }

        [HttpPost]
        [Route("Enrol")]
        public async Task<ApiResponse> InsertStudent([FromQuery]int studentId, [FromQuery]int subjectId)
        {
            return await ExecuteWithErrorHandlingAsync(async () =>
            {
                return Success(await EnrollmentService.EnrolStudent(studentId, subjectId));
            });
        }
    }
}
