using BLL.Interface;
using Entity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Site.Config;
using Site.Models;
using Site.Service;

namespace Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwt;
        public UserController(IUserService userService, IJwtService jwt)
        {
            _userService = userService;
            _jwt = jwt;
        }

        [HttpPost("Login")]
        public ActionResult<UserModel> Login(UserInputModel userInput)
        {
            
            var request = _userService.Login(mappingUser(userInput));
            if(request.Error)
            {
                return BadRequest(request.Message);
            }

            var clainUser = _jwt.GetJwtToken(request.Entity);

            return Ok(clainUser);
        }

        [HttpPost("Register")]
        public ActionResult<UserModel> Register(UserInputModel userInput)
        {
            var request = _userService.Create(mappingUser(userInput));
            if (request.Error)
            {
                return BadRequest(request.Message);
            }

            var clainUser = _jwt.GetJwtToken(request.Entity);

            return Ok(clainUser);
        }

        private User mappingUser(UserInputModel userInput)
        {
            var user = new User()
            {
                UserName = userInput.UserName,
                Password = userInput.Password,
            };

            return user;
        }
      
    }
}
