using System.Collections.Generic;
using System.Threading.Tasks;
using University.Service.Entities;

namespace University.Service.Interfaces
{
    public interface ILectureTheatreService
    {
        Task<List<LectureTheatre>> GetAllLectureTheatres();

        Task<LectureTheatre> GetLectureTheatreById(int id);

        Task<bool> InsertLectureTheatre(LectureTheatre subject);

        Task<bool> UpdateLectureTheatre(LectureTheatre subject);        

        Task<bool> DeleteLectureTheatre(int id);
    }
}
