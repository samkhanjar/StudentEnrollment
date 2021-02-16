using System.Collections.Generic;
using System.Threading.Tasks;
using University.Service.Entities;

namespace University.Service.Interfaces
{
    public interface ISubjectService
    {
        Task<List<Subject>> GetAllSubjects();

        Task<Subject> GetSubjectById(int id);

        Task<bool> InsertSubject(Subject subject);

        Task<bool> UpdateSubject(Subject subject);        

        Task<bool> DeleteSubject(int id);
    }
}
