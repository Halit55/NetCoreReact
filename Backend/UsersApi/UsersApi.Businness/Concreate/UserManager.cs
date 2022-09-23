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
    public class UserManager : IUserService
    {
        public readonly IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        #region Crud      
        public async Task<User> AddAsync(User user)
        {
            if (AddValidate(user) != null)
            {
                return await _userDal.AddAsync(user);
            }
            else
            {
                return null;
            }
        }

        public async Task<User> UpdateAsync(User user)
        {
            if (UpdateValidate(user) != null)
            {
                return await _userDal.UpdateAsync(user);
            }
            else
            {
                return null;
            }
        }

        public async Task<User> DeleteAsync(int id)
        {
            if (DeleteValidate(id))
            {
                return await _userDal.DeleteAsync(new User { UserId = id });
            }
            else
            {
                return null;
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userDal.GetByIdAsync(x => x.UserId == id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userDal.GetAllAsync();
        }

        public async Task<User> LoginAsync(string name, string password)
        {
            return await _userDal.LoginAsync(name,password);
        }

        #endregion

        #region Validete Control
        User AddValidate(User user)
        {
            return user;
        }

        User UpdateValidate(User user)
        {
            return user;
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
