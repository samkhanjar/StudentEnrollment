using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace University.Service.Interfaces
{
    public interface IEnrollmentService
    {
        Task<string> EnrolStudent(int studentId, int subjectId);        
    }
}
