using ClinicAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicAPI.Presistence.DbConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.ToTable("Users");

            builder.Property(p => p.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.Email)
                   .HasMaxLength(120);

            builder.Property(p => p.Phone)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(p => p.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(p => p.Age)
                   .IsRequired();

            builder.Property(p => p.CreatedAt)
                  .IsRequired();

            builder.Property(p => p.IsDeleted)
                   .HasDefaultValue(false);

        }
    }
}
