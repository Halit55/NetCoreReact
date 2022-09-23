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

namespace Users.DataAccess.Concreate.EntityFramework
{
    public class EfDepartmentDal : EfRepository<Department,UsersContext>,IDepartmentDal
    {
      
    }
}
