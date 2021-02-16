using Microsoft.EntityFrameworkCore;
using University.Service.Entities;

namespace University.Service.DataContext
{
    public class UniversityContext : DbContext
    {        
        public UniversityContext()
        {
        }

        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public UniversityContext CreateDbContext(string conn)
        {            
            var optionBuilder = new DbContextOptionsBuilder<UniversityContext>();
            optionBuilder.UseSqlServer(conn);

            return new UniversityContext(optionBuilder.Options);
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>().HasKey(enrollment => new { enrollment.StudentId, enrollment.SubjectId });            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<LectureTheatre> LectureTheatres { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Subject> Subjects { get; set; }
    }
}
