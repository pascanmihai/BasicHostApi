using HostDb.Models;
using HostDb.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostApib.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {
        private UserRepos _repos;
        public AuthController(UserRepos repos)
        {
            _repos = repos;
        }

        [HttpPost("Register")]
        [ProducesResponseType(typeof(List<User>),200)]
        public async Task<IActionResult>Register(User user)
        {
           var created = await _repos.Register(user);
            if (created == null)
                return BadRequest("User with this username already exists");
            return Ok("user has been created");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(User user)
        {
            
            
                var acces = await _repos.Login(user.UserName);
                if (acces == null)
                    return BadRequest(new { message = "Username or is incorrect" });

                var result = await _repos.PasswordSignInAsync(user.Password);
            if (result == null)
            {
                return Unauthorized(new { message = "password incorrcet" });
            }
                return Ok(acces);
        }

        [HttpPut("id")]
        public async Task<IActionResult>Update(long id, User user)
        {
            if (id != user.Id)
                return BadRequest();
            var entity = await _repos.Update(user);
            if (entity == null)
                return BadRequest();
            return Ok(entity);
        }
        [HttpDelete("id")]
        public async Task<IActionResult>Delete(int id)
        {
            try
            {
                await _repos.DeleteUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }   
}
