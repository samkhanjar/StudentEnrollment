using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using University.Service.DataContext;
using University.Service.Entities;
using University.Service.Interfaces;

namespace University.Service.Implementation
{
    public class SubjectService : ISubjectService
    {
        private readonly IConfigurationService Configuration;

        public SubjectService(IConfigurationService configuration)
        {
            Configuration = configuration;
        }

        public async Task<bool> InsertSubject(Subject subject)
        {
            if (subject.Id == 0)
            {
                using var context = new UniversityContext().CreateDbContext(Configuration.ConnectionString);
                try
                {
                    context.Set<Subject>().Add(subject);
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

        public async Task<bool> UpdateSubject(Subject subject)
        {
            if (subject.Id > 0)
            {
                using var context = new UniversityContext().CreateDbContext(Configuration.ConnectionString);
                try
                {
                    var registeredSubject = await context.Set<Subject>().AsNoTracking().SingleOrDefaultAsync(x => x.Id == subject.Id && !x.IsDeleted);

                    if (registeredSubject != null)
                    {
                        context.Set<Subject>().Update(subject);
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

        public async Task<bool> DeleteSubject(int id)
        {
            using var context = new UniversityContext().CreateDbContext(Configuration.ConnectionString);
            try
            {
                var registeredSubject = await context.Set<Subject>().SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (registeredSubject != null)
                {
                    registeredSubject.IsDeleted = true;
                    context.Set<Subject>().Update(registeredSubject);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return false;
        }

        public async Task<List<Subject>> GetAllSubjects()
        {
            using var context = new UniversityContext().CreateDbContext(Configuration.ConnectionString);
            return await context.Set<Subject>().Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<Subject> GetSubjectById(int id)
        {
            if (id > 0)
            {
                using var context = new UniversityContext().CreateDbContext(Configuration.ConnectionString);
                return await context.Set<Subject>().SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            }

            return null;
        }
    }
}
