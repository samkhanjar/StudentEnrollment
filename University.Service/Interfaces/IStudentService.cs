using System.Collections.Generic;
using System.Threading.Tasks;
using University.Common.Requests;
using University.Service.Entities;

namespace University.Service.Interfaces
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudents();

        Task<Student> GetStudentById(int id);

        Task<bool> InsertStudent(StudentRequest student);

        Task<bool> UpdateStudent(StudentRequest student);        

        Task<bool> DeleteStudent(int id);
    }
}
