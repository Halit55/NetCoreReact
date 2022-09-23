using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.DataAccess.Context;
using Users.DataAccess.Abstract;
using Users.Entities.Entities.Concreate;
using System.Linq.Expressions;
using Users.Core.Repository.Concreate;
using Microsoft.EntityFrameworkCore;

namespace Users.DataAccess.Concreate.EntityFramework
{
    public class EfUserDal : EfRepository<User, UsersContext>, IUserDal
    {
        public async Task<User> LoginAsync(string name, string password)
        {            
            using (var context = new UsersContext())
            {
                return await context.Users.FirstOrDefaultAsync(x => x.UserName == name && x.Password == password);
            }
        }
    }
}
