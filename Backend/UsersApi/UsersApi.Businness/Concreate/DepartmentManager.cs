using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Businness.Abstract;
using Users.Entities.Entities.Concreate;
using Users.DataAccess.Abstract;

namespace Users.Businness.Concreate
{
    public class DepartmentManager : IDepartmentService
    {
        public readonly IDepartmentDal _departmentDal;
        public DepartmentManager(IDepartmentDal departmentDal)
        {
            _departmentDal = departmentDal;
        }

        #region Crud      
        public async Task<Department> AddAsync(Department department)
        {
            if (AddValidate(department) != null)
            {
                return await _departmentDal.AddAsync(department);
            }
            else
            {
                return null;
            }
        }

        public async Task<Department> UpdateAsync(Department department)
        {
            if (UpdateValidate(department) != null)
            {
                return await _departmentDal.UpdateAsync(department);
            }
            else
            {
                return null;
            }
        }

        public async Task<Department> DeleteAsync(int id)
        {
            if (DeleteValidate(id))
            {
                return await _departmentDal.DeleteAsync(new Department { DepartmentId = id });
            }
            else
            {
                return null;
            }
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _departmentDal.GetByIdAsync(x => x.DepartmentId == id);
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _departmentDal.GetAllAsync();
        }
        #endregion

        #region Validete Control
        Department AddValidate(Department department)
        {
            return department;
        }

        Department UpdateValidate(Department department)
        {
            return department;
        }

        Boolean DeleteValidate(int id)
        {
            if (id > 0)
                return true;
            else
                return false;
        }        
        #endregion

    }
}
