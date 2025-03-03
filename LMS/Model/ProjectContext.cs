using Microsoft.EntityFrameworkCore;

namespace LMS.Model
{
    public class ProjectContext: DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options):base(options)
        {
            
        }
        public DbSet<categories> Categories { get; set; }
        public DbSet<courses> Courses { get; set; }
        public DbSet<enrollments> Enrollments { get; set; }
        public DbSet<lecture_materials> LectureMaterials { get; set; }
        public DbSet<lectures> Lectures { get; set; }
        public DbSet<students> Students { get; set; }

    }
}
