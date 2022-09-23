using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Entities.Entities.Concreate;

namespace Users.Businness.Abstract
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<Department> AddAsync(Department department);
        Task<Department> UpdateAsync(Department department);
        Task<Department> DeleteAsync(int id);
        Task<Department> GetByIdAsync(int id);
    }
}
