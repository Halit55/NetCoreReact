using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Core.Repository.Abstract;
using Users.Entities.Entities.Concreate;

namespace Users.DataAccess.Abstract
{
    public interface IDepartmentDal:IGenericRepository<Department>
    {
    }
}
