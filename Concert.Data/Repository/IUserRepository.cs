using Concert.Data.Entity;

namespace Concert.Data.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByIdAsync(string id);
        Task<User?> GetUserByEmailAsync(string email);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
