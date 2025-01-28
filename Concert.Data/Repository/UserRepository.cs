using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concert.Data.Entity;
using Concert.Data;
using Microsoft.EntityFrameworkCore;

namespace Concert.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository

    {
        public ApplicationDbContext DbContext => Context as ApplicationDbContext;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await DbContext.Users.FindAsync(id);
        }
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await DbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task AddUserAsync(User user)
        {
            await DbContext.Users.AddAsync(user);
        }
        public async Task UpdateUserAsync(User user)
        {
            DbContext.Users.Update(user);
        }
        public async Task DeleteUserAsync(User user)
        {
            DbContext.Users.Remove(user);
        }

    }
}
