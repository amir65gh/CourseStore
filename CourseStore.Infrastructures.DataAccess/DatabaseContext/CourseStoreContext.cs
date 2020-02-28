using CourseStore.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace CourseStore.Infrastructures.DataAccess.DatabaseContext
{
    public class CourseStoreContext : DbContext
    {
        public CourseStoreContext(DbContextOptions<CourseStoreContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<PurchasedCourse> PurchasedCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
