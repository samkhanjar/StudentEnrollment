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
    public class LectureTheatreController : BaseApiController
    {
        private readonly ILectureTheatreService LectureTheatreService;

        private readonly IConfiguration Configuration;

        public LectureTheatreController(ILectureTheatreService lectureTheatreService, IConfiguration configuration)
        {
            LectureTheatreService = lectureTheatreService;
            Configuration = configuration;
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<ApiResponse> Get([FromQuery]int id)
        {
            return await ExecuteWithErrorHandlingAsync(async () =>
            {
                return Success(await LectureTheatreService.GetLectureTheatreById(id));
            });
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ApiResponse> Get()
        {
            return await ExecuteWithErrorHandlingAsync(() =>
            {
                return Success(LectureTheatreService.GetAllLectureTheatres());
            });
        }

        [HttpPut]
        [Route("Update")]
        public async Task<ApiResponse> UpdateLectureTheatre([FromBody]LectureTheatre theatre)
        {
            if (ModelState.IsValid)
            {
                return await ExecuteWithErrorHandlingAsync(() =>
                {
                    var response = LectureTheatreService.UpdateLectureTheatre(theatre).Result;

                    if (response)
                    {
                        return Success("Theatre updated successfully.");
                    }

                    return Failed("Update failed!");
                });
            }

            return Failed("Invalid Request!");
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<ApiResponse> InsertLectureTheatre([FromBody]LectureTheatre theatre)
        {
            if (ModelState.IsValid)
            {
                return await ExecuteWithErrorHandlingAsync(() =>
                {
                    var response = LectureTheatreService.InsertLectureTheatre(theatre).Result;

                    if (response)
                    {
                        return Success("Lecture theatre added successfully.");
                    }

                    return Failed("Insert failed!");
                });
            }

            return Failed("Invalid Request!");
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ApiResponse> RemoveLectureTheatre(int id)
        {
            return await ExecuteWithErrorHandlingAsync(() =>
            {
                var response = LectureTheatreService.DeleteLectureTheatre(id).Result;

                if (response)
                {
                    return Success("Lecture theatre deleted successfully!");
                }

                return Failed("Lecture theatre is not found!");
            });
        }
    }
}
