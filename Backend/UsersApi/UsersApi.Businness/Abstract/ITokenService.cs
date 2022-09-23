using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Businness.Abstract
{
    public interface ITokenService
    {
        string CreateToken(string username,string password);
    }
}
