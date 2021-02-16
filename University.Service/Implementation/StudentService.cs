using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Common.Requests;
using University.Service.DataContext;
using University.Service.Entities;
using University.Service.Interfaces;

namespace University.Service
{
    public class StudentService : IStudentService
    {
        private readonly IConfigurationService Configuration;

        public StudentService(IConfigurationService configuration)
        {
            Configuration = configuration;
        }

        public async Task<bool> InsertStudent(StudentRequest studentRequest)
        {
            if (studentRequest.Id == 0)
            {
                using var context = new UniversityContext().CreateDbContext(Configuration.ConnectionString);
                try
                {
                    var student = new Student()
                    {                        
                        FirstName = studentRequest.FirstName,
                        LastName = studentRequest.LastName,
                        CreatedDate = DateTime.Now
                    };

                    context.Set<Student>().Add(student);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return false;
        }

        public async Task<bool> UpdateStudent(StudentRequest studentRequest)
        {
            if (studentRequest.Id > 0)
            {
                using var context = new UniversityContext().CreateDbContext(Configuration.ConnectionString);
                try
                {
                    var student = new Student()
                    {
                        Id = studentRequest.Id,
                        FirstName = studentRequest.FirstName,
                        LastName = studentRequest.LastName,
                        UpdatedDate = DateTime.Now
                    };

                    var registeredStudent = await context.Set<Student>().AsNoTracking().SingleOrDefaultAsync(x => x.Id == studentRequest.Id && !x.IsDeleted);

                    if (registeredStudent != null)
                    {
                        context.Set<Student>().Update(student);
                        await context.SaveChangesAsync();
                        return true;
                    }                                       
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return false;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            using var context = new UniversityContext().CreateDbContext(Configuration.ConnectionString);
            try
            {
                var registeredStudent = await context.Set<Student>().SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (registeredStudent != null)
                {
                    registeredStudent.IsDeleted = true;
                    context.Set<Student>().Update(registeredStudent);
                    return true;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return false;
        }

        public async Task<List<Student>> GetAllStudents()
        {
            using var context = new UniversityContext().CreateDbContext(Configuration.ConnectionString);
            return await context.Set<Student>().Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            if (id > 0)
            {
                using var context = new UniversityContext().CreateDbContext(Configuration.ConnectionString);
                return await context.Set<Student>().SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            }

            return null;
        }     
    }
}
