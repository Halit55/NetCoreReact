using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Entities.Entities.Concreate;

namespace Users.Businness.Abstract
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<User> DeleteAsync(int id);
        Task<User> GetByIdAsync(int id);
        Task<User> LoginAsync(string name, string password);
    }
}
