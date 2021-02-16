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
    public class LectureTheatreService : ILectureTheatreService
    {
        private readonly IConfigurationService Configuration;

        public LectureTheatreService(IConfigurationService configuration)
        {
            Configuration = configuration;
        }

        public async Task<bool> InsertLectureTheatre(LectureTheatre theatre)
        {
            if (theatre.Id == 0)
            {
                using var context = new UniversityContext().CreateDbContext(Configuration.ConnectionString);
                try
                {
                    context.LectureTheatres.Add(theatre);
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

        public async Task<bool> UpdateLectureTheatre(LectureTheatre theatre)
        {
            if (theatre.Id > 0)
            {
                using var context = new UniversityContext().CreateDbContext(Configuration.ConnectionString);
                try
                {
                    var registeredTheatre = await context.LectureTheatres.SingleOrDefaultAsync(x => x.Id == theatre.Id && !x.IsDeleted);

                    if (registeredTheatre != null)
                    {
                        context.LectureTheatres.Update(theatre);
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

        public async Task<bool> DeleteLectureTheatre(int id)
        {
            using var context = new UniversityContext().CreateDbContext(Configuration.ConnectionString);
            try
            {
                var registeredTheatre = await context.LectureTheatres.SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

                if (registeredTheatre != null)
                {
                    registeredTheatre.IsDeleted = true;
                    context.LectureTheatres.Update(registeredTheatre);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return false;
        }

        public async Task<List<LectureTheatre>> GetAllLectureTheatres()
        {
            using var context = new UniversityContext().CreateDbContext(Configuration.ConnectionString);
            return await context.LectureTheatres.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<LectureTheatre> GetLectureTheatreById(int id)
        {
            if (id > 0)
            {
                using var context = new UniversityContext().CreateDbContext(Configuration.ConnectionString);
                return await context.LectureTheatres.SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            }

            return null;
        }
    }
}
