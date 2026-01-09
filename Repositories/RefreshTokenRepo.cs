using ClinicAPI.Models;
using ClinicAPI.Presistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicAPI.Repositories
{
    public class RefreshTokenRepo(ClinicDbContext context) : IRefreshTokenRepo
    {

        public async Task<RefreshTokenModel> GetRefreshTokenAsync(string refreshToken)
        {
            return await context.RefreshTokens.FirstOrDefaultAsync(r => r.RefreshTokenHash == refreshToken);
        }

        public async Task AddRefreshTokenAsync(RefreshTokenModel refreshToken)
        {
            await context.RefreshTokens.AddAsync(refreshToken);

            await context.SaveChangesAsync();
        }

        public async Task DeleteRefreshTokenAsync(int userId)
        {
            var refreshToken = await context.RefreshTokens.FirstOrDefaultAsync(r => r.UserId == userId);

            context.RefreshTokens.Remove(refreshToken!);

            await context.SaveChangesAsync();
        }
    }
}
