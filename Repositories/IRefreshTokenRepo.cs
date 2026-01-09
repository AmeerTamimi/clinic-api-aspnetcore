using ClinicAPI.Models;

namespace ClinicAPI.Repositories
{
    public interface IRefreshTokenRepo
    {
        Task<RefreshTokenModel> GetRefreshTokenAsync(string refreshToken);
        Task AddRefreshTokenAsync(RefreshTokenModel refreshToken);
        Task DeleteRefreshTokenAsync(int userId);
    }
}
