using CourseStore.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseStore.Infrastructures.DataAccess.Configuration
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(250);
            builder.HasIndex(c => c.Name).IsUnique();

            builder.HasData(
                new Course { Id = 1, Name = "Workshop Domain Driven Design", LicensingModel = LicensingModel.TowDays },
                new Course { Id = 2, Name = "Advanced ASP.Net MVC", LicensingModel = LicensingModel.LifeLong },
                new Course { Id = 3, Name = "NO SQL Course", LicensingModel = LicensingModel.LifeLong });
        }
    }
}
