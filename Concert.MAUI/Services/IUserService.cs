using Concert.MAUI.Models;

namespace Concert.MAUI.Services
{
    public interface IUserService
    {
        Task<List<User>?> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(string id);
        Task<User?> GetUserByEmailAsync(string email);
        Task SaveUserAsync(User user, bool isNewUser);
        Task DeleteUserAsync(string id);
    }
}
