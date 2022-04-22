using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineAdvising.Core.Interfaces;
using OnlineAdvising.Core.Models;
using OnlineAdvising.Core.Results;

namespace OnlineAdvising.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserFileService _userFileService;

        public AuthenticationController(
            IUserService userService, 
            IUserFileService userFileService)
        {
            _userService = userService;
            _userFileService = userFileService;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var result = await _userService.LoginAsync(model);
            if (!result.IsSucceeded) return Unauthorized();
            return Ok(result.Value);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm]UserModel model)
        {
            var files = Request.Form.Files;
            var result = await _userService.AddAsync(model);
            if (result.IsSucceeded && model.RoleId == 1 && files.Count > 0)
            {
                await _userFileService.UploadFiles(files, result.Value.Id);
            }
            if (!result.IsSucceeded) return BadRequest(result.Error);
            return Ok(result.Value); 
        }
    }
}