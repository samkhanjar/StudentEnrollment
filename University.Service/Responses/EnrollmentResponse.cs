using System;
using System.Collections.Generic;
using System.Text;
using University.Service.Entities;

namespace University.Common.Responses
{
    public class EnrollmentResponse
    {
        public Student Student { get; set; }

        public List<Enrollment> Enrollments { get; set; }
    }
}
