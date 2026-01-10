using ClinicAPI.Models;
using ClinicAPI.Presistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicAPI.Repositories
{
    public class RefreshTokenRepo(ClinicDbContext context) : IRefreshTokenRepo
    {

        public async Task<RefreshTokenModel?> GetRefreshTokenAsync(string refreshTokenHash)
        {
            return await context.RefreshTokens.SingleOrDefaultAsync(r => r.RefreshTokenHash == refreshTokenHash);
        }

        public async Task AddRefreshTokenAsync(RefreshTokenModel refreshToken)
        {
            // 
            await context.RefreshTokens.AddAsync(refreshToken);

            await context.SaveChangesAsync();
        }

        public async Task DeleteRefreshTokenAsync(int userId)
        {
            var refreshToken = await context.RefreshTokens.SingleOrDefaultAsync(r => r.UserId == userId);

            if (refreshToken is not null) 
            {
                context.RefreshTokens.Remove(refreshToken);
                Console.WriteLine("WOHOOOOOOOOOOOOOOOOOOOOO");
            }

                

            await context.SaveChangesAsync();
        }
    }
}
