using Course_Management_System_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Course_Management_System_API.Data;

namespace Course_Management_System_API.Controllers
{
    [Route("/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationRepo _repo;

        public AuthenticationController(IAuthenticationRepo repo)
        {
            _repo = repo;
        }

        [Authorize(Roles = UserRoles.Professor)]
        [HttpPost]
        [Route("RegisterStudent")]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterModel model)
        {
            await _repo.RegisterStudent(model);
            return Ok(new Response { Status = "Success", Message = "Student created successfully" });
        }

        [Authorize(Roles = UserRoles.Professor)]
        [HttpPost]
        [Route("RegisterProfessor")]
        public async Task<IActionResult> RegisterProfessor([FromBody] RegisterModel model)
        {
            await _repo.RegisterProfessor(model);
            return Ok(new Response { Status = "Success", Message = "Professor created successfully" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var token = await _repo.Login(model);
            if (token != null)
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token)});
            else
                return Unauthorized();
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            await _repo.ChangePassword(model);
            return Ok(new Response { Status = "Success", Message = "Paasword changed successfully" });
        }
    }
}
