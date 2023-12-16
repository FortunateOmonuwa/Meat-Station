
using Meat_Station.DataAccess.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Meat_Station.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IUserRoleService roleService;
        public RolesController(IUserRoleService roleService)
        {
            this.roleService = roleService;       
        }


        [HttpGet ("get-all-roles")]
        public async Task<IActionResult> GetAllRoles() => Ok(await roleService.GetAllRolesAsync());


        [HttpPatch("updated-user-role")]
        public async Task<IActionResult> UpdateUserRolePatchAsync([FromQuery] string userId, [FromBody]JsonPatchDocument roleId)
        {
            var role = await roleService.UpdateUserRolePatchAsync(userId, roleId);
            if (role == null)
            {
                return NotFound();
            }
            try
            {
                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}\n {ex.Source}\n {ex.InnerException}");
            }
        }
     
    }
}
