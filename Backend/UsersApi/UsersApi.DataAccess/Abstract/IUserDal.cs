using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Entities.Entities.Concreate;
using Users.Core.Repository.Abstract;

namespace Users.DataAccess.Abstract
{
    public interface IUserDal: IGenericRepository<User>
    {
        public Task<User> LoginAsync(string name, string password);
    }
}
