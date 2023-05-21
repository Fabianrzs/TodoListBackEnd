using Entity;
using Microsoft.AspNetCore.Mvc;

namespace Site.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncriptController : ControllerBase
    {
        [HttpPost("Encript")]
        public IActionResult Encript([FromBody] string data)
        {
            return Ok(CrypterDefault.Encrypt(data));
        }

        [HttpPost("Decript")]
        public IActionResult Decript([FromBody] string data)
        {
            return Ok(CrypterDefault.Decrypt(data));
        }
    }
}
