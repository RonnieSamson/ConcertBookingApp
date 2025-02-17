using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Concert.Data.Entity;  

namespace Concert.Data.Repository
{
    public interface IUserRepository : IRepository<User>
    {
       Task<User?> GetUserByIdAsync(string id);
       Task<User?> GetUserByEmailAsync(string email);
       Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
    }
}
