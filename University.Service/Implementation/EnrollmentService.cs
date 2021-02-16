using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using University.Service.DataContext;
using University.Service.Entities;
using University.Service.Interfaces;

namespace University.Service.Implementation
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IConfigurationService Configuration;        

        public EnrollmentService(IConfigurationService configuration)
        {
            Configuration = configuration;
        }

        public async Task<string> EnrolStudent(int studentId, int subjectId)
        {            
            return await CheckEnrollmentProcess(studentId, subjectId);
        }

        private async Task<int> GetStudentHoursPerWeek(int studentId)
        {
            if (studentId > 0)
            {
                using var context = new UniversityContext().CreateDbContext(Configuration.ConnectionString);
                var enrollmentHours = await (from e in context.Enrollments
                                             join l in context.Lectures on e.SubjectId equals l.SubjectId
                                             join s in context.Subjects on e.SubjectId equals s.Id
                                             join st in context.Students on e.StudentId equals st.Id
                                             where e.StudentId == studentId && !e.IsDeleted
                                             group new { l, st, s } by new { st.FirstName, st.LastName, s.Name, l.StartTime, l.EndTime } into g
                                             select new
                                             {
                                                 g.Key.FirstName,
                                                 g.Key.LastName,
                                                 Subject = g.Key.Name,
                                                 g.Key.StartTime,
                                                 g.Key.EndTime
                                             }).ToListAsync();


                return enrollmentHours.Sum(x => (x.EndTime - x.StartTime).Hours);
            }

            return 0;
        }

        private async Task<string> CheckEnrollmentProcess(int studentId, int subjectId)
        {
            try
            {
                // Check if student already enrolled for this subject
                using (var context = new UniversityContext().CreateDbContext(Configuration.ConnectionString))
                {
                    var newEnrollment = context.Enrollments.FirstOrDefault(x => x.StudentId == studentId && x.SubjectId == subjectId && !x.IsDeleted);

                    if (newEnrollment == null)
                    {
                        // Check if student id is valid
                        var student = await context.Students.FindAsync(studentId);

                        if (student == null)
                            return "Invalid student Id!";

                        // Check if subject id is valid 
                        var subject = await context.Subjects.FindAsync(subjectId);

                        if (subject == null)
                            return "Invalid subject Id!";

                        // Get student enrollment
                        var enrollmentHours = await GetStudentHoursPerWeek(studentId);

                        if (enrollmentHours <= 10)
                        {
                            // We need to perform the next check and that is we need to find if theatre has reached the maximum capicity
                            // 1. We need to get the count for all students enrolled under a subject
                            var enrolledStudent = context.Enrollments.Count(x => x.SubjectId == subjectId && !x.IsDeleted);

                            // 2. We need to fetch the capacity for the lecture theatre for that subject
                            var capacity = (from l in context.Lectures
                                            join th in context.LectureTheatres on l.LectureTheatreId equals th.Id
                                            where l.SubjectId == subjectId && !l.IsDeleted
                                            select new { 
                                                th.Capacity
                                            }).FirstOrDefault().Capacity;

                            if (enrolledStudent <= capacity)
                            {
                                await context.Set<Enrollment>().AddAsync(new Enrollment() { StudentId = studentId, SubjectId = subjectId, CreatedDate = DateTime.Now });
                                await context.SaveChangesAsync();
                                return "Student enrolled successfully!";
                            }

                            return "Lecture theatre reached maximum capacity!";
                        }

                        return "Max hours reached per week";
                    }

                    return "Student already enrolled for this subject!";
                }                
            }            
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }               
    }
}
