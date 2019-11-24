using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Micro.Common.Commands;
using Micro.Services.Identity.Services;

namespace Micro.Services.Identity.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthenticateUser command)
            => Ok(await _userService.LogginAsync(command.Email, command.Password));
    }
}