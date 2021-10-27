using Course_Management_System_API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Course_Management_System_API.Data
{
    public interface IAuthenticationRepo
    {
        Task RegisterStudent(RegisterModel model);
        Task RegisterProfessor(RegisterModel model);
        Task<JwtSecurityToken> Login(LoginModel model);
        Task ChangePassword(ChangePasswordModel model);
    }
}
