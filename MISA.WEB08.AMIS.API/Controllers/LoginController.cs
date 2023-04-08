using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MISA.WEB08.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Administrator,Seller")]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        ////[AllowAnonymous]
        //[Route("index")]
        //[HttpPost]
        //public object Login([FromBody] userModel userLogin)
        //{
        //    if (userLogin != null)
        //    {
        //        var token = Generate(userLogin);
        //        return token;
        //    }

        //    return "F";
        //}

        //[Route("logout")]
        //[HttpPost]
        //public IActionResult Logout()
        //{
        //    return Ok("Logged out.");
        //}

        //private string Generate(userModel user)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.account),
        //        new Claim(ClaimTypes.Name, user.account),
        //        new Claim(ClaimTypes.GivenName, user.password),
        //        new Claim(ClaimTypes.Role, user.role)
        //    };

        //    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        //      _config["Jwt:Audience"],
        //      claims,
        //      expires: DateTime.Now.AddYears(10),
        //      signingCredentials: credentials);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
        //[HttpGet("get-user")]
        //public IActionResult AdminsAndSellersEndpoint()
        //{
        //    var currentUser = GetCurrentUser();

        //    return Ok(currentUser);
        //}


        //private userModel GetCurrentUser()
        //{
        //    var identity = HttpContext.User.Identity as ClaimsIdentity;

        //    if (identity != null)
        //    {
        //        var userClaims = identity.Claims;

        //        return new userModel
        //        {
        //            account = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
        //            password = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
        //            role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
        //        };
        //    }
        //    return null;
        //}
        //private userModel Authenticate(userModel userLogin)
        //{
        //    return business.GetUserbyID(userLogin.account, userLogin.password);
        //}
    }
}
