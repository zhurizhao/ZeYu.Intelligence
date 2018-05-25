using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeYu.Intelligence.WebAPI.Models;
using ZeYu.Intelligence.WebAPI.Security;
using ZeYu.Intelligence.WebAPI.Data;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ZeYu.Intelligence.WebAPI.Controllers
{
    [Produces("application/json")]
    //[Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly DB0Context _context;
        public IConfiguration Configuration { get; }

        public AuthController(DB0Context context, IConfiguration configuration)
        {
            this._context = context;
            Configuration = configuration;
        }
        

        [HttpPost]
        [Route("api/[controller]/Login")]
        public IActionResult Login([FromBody]LoginModel2 loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.email) || string.IsNullOrEmpty(loginModel.password))
                return Unauthorized();

            var account = _context.Account.FirstOrDefault(a => a.EMail == loginModel.email);

            if (account == null || account == default(Account))
            {
                return Unauthorized();
            }
            else
            {
                string preHashText = loginModel.password.Trim() + account.Salt;
                string hashString = COCOPASS.Helper.NetCoreHelper.Encrypt.ToHMACSHA256HashString(preHashText);
                if (!hashString.Equals(account.Password))
                {
                    return Unauthorized();
                }
            }

            var token = new JwtTokenBuilder()
                              .AddSubject("ZeYu")
                              .AddSecurityKey(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthenticationToken:SigningKey"])))
                              .AddIssuer(Configuration["AuthenticationToken:Issuer"])
                              .AddAudience(Configuration["AuthenticationToken:Audience"])
                              .AddClaim("MembershipId", account.ID)
                              .AddExpiry(2)
                              .Build();


            return Ok(token);
        }



    }
}