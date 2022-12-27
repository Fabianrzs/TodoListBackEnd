using BLL.Response;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IUserService
    {
        GenericResponse<User> Create(User user);
        GenericResponse<User> Login(User user);
    }
}
 