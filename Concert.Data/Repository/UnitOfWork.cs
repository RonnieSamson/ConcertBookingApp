using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concert.Data.Repository;
using Concert.Data.Entity;

namespace Concert.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public IUserRepository Users { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Users = new UserRepository(context);
        }
        public async Task<int> CompleteAsync()
        {
            return await context.SaveChangesAsync();
        }
        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await Users.GetUserByIdAsync(id);
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await Users.GetUserByEmailAsync(email);
        }

        public async Task AddUserAsync(User user)
        {
            await Users.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await Users.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(User user)
        {
            await Users.DeleteUserAsync(user);
        }



    }

}
