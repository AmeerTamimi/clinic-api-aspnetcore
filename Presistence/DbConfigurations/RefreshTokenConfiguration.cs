using ClinicAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicAPI.Presistence.DbConfigurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenModel>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenModel> builder)
        {
            builder.ToTable("RefreshToken");

            builder.HasKey(r => r.RefreshTokenId);

            builder.HasOne(r => r.User)
                   .WithOne(u => u.RefreshTokenModel)
                   .HasForeignKey<RefreshTokenModel>(r => r.UserId);
        }
    }
}
