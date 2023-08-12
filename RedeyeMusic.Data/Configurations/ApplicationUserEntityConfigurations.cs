using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedeyeMusic.Data.Models;

namespace RedeyeMusic.Data.Configurations
{
    public class ApplicationUserEntityConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
               .Property(i => i.IsDeleted)
               .HasDefaultValue(false);
            builder
                .Property(u => u.FirstName)
                .HasDefaultValue("Test");
            builder
                .Property(u => u.LastName)
                .HasDefaultValue("Testov");
        }
    }
}
