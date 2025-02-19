using Concert.Data.Repository;
using Concert.Data;
using Concert.Data.Entity;

public class UserRepository : Repository<User>, IUserRepository
{
    public ApplicationDbContext DbContext => Context as ApplicationDbContext;

    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        return (await Find(u => u.Id == id)).FirstOrDefault();
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return (await Find(u => u.Email == email)).FirstOrDefault();
    }

    public void AddUser(User user) 
    {
        Insert(user);
    }

    public void UpdateUser(User user)
    {
        Update(user);
    }

    public void DeleteUser(User user)
    {
        Delete(user);
    }
}
