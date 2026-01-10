using ClinicAPI.Models;
using ClinicAPI.Presistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicAPI.Repositories
{
    public class UserRepo(ClinicDbContext context)
    {
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.UserId == userId);
        }
    }
}
