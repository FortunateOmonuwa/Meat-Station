using Meat_Station.DataAccess.Interfaces;
using Meat_Station.Domain.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Meat_Station.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO newUserModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userService.RegisterUser(newUserModel);
                    return Ok(user);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}\n {ex.Source}\n {ex.InnerException}");
            }
        }
        [HttpPost("{verifiy-registeration-token}")]
        public async Task<IActionResult> VerifyRegisterToken(UserRegisterTokenVerificationDTO verifyToken)
        {
            try
            {
                var verification = await _userService.VerifyUser(verifyToken);
                if (verification == null)
                {
                    return BadRequest("Invalid Token");
                }
                else
                {
                    return Ok("Your verification was successful ");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}\n {ex.Source}\n {ex.InnerException}");
            }
        }
    }
}
