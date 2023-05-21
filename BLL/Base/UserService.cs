using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using BLL.Interface;
using BLL.Response;
using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Base
{
    public class UserService : IUserService
    {
        private readonly TodoListContext _context;
        public UserService(TodoListContext context)
        {
             _context= context; 
        }

        public GenericResponse<User> Create(User user)
        {
            try
            {
                var entity = _context.Users.FirstOrDefault(x=>x.UserName == user.UserName);
                if (entity == null)
                {
                    user.Password = CrypterDefault.Encrypt(user.Password);
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return new GenericResponse<User>(user);
                }
                return new GenericResponse<User>($"Ya existe un usuario con ese nombre");
            }
            catch (Exception e)
            {
                _context.Dispose();
                return new GenericResponse<User>($"Error de consulta: error {e.Message}");

            }
        }

        public GenericResponse<User> Login(User user)
        {
            try
            {
                var entity = _context.Users.FirstOrDefault(x => x.UserName == user.UserName);
                if (entity != null && user.Password == CrypterDefault.Decrypt(entity.Password))
                {
                    return new GenericResponse<User>(entity);
                }
                return new GenericResponse<User>($"Credenciales invalidas");
            }
            catch (Exception e)
            {
                _context.Dispose();
                return new GenericResponse<User>($"Error de consulta: error {e.Message}");

            }
        }
    }
}
