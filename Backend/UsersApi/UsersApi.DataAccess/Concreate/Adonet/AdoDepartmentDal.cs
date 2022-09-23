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
    public class AdoDepartmentDal : AdoRepository<Department, UsersContext>, IDepartmentDal
    {
    }
}
