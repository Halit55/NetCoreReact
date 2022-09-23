using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Repository.Concreate;
using Users.DataAccess.Abstract;
using Users.DataAccess.Context;
using Users.Entities.Entities.Concreate;

namespace Users.DataAccess.Concreate.Adonet
{
    public class AdoUserDal : AdoRepository<User, UsersContext>, IUserDal
    {
        //Burası adonet e göre ayarlanacak
        public async Task<User> LoginAsync(string name, string password)
        {
            using (var context = new UsersContext())
            {
                return await context.Users.FirstOrDefaultAsync(x => x.UserName == name && x.Password == password);
            }
        }
    }
}
