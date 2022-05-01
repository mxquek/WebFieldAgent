using FieldAgent.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FieldAgent.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost, Route("login")]
        public IActionResult Login(LoginModel user)
        {
            if (user == null)
            {
                return BadRequest("Invalid request");
            }

            if(user.UserName == "assessment" && user.Password == "abc@123")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"));
                var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:2000",
                    audience: "http://localhost:2000",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signInCredentials
                    );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }

            return BadRequest();
        }

    }
}
