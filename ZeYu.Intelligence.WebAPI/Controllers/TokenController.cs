using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ZeYu.Intelligence.WebAPI.Models;
using ZeYu.Intelligence.WebAPI.Security;
using Microsoft.AspNetCore.Cors;

namespace ZeYu.Intelligence.WebAPI.Controllers
{
    [Produces("application/json")]
    //[Route("api/[controller]")] //设置了类路由属性后，方法路由属性会失效
    [AllowAnonymous]
    //[EnableCors(origin: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
    //[AllowCrossSiteJsonAttribute] //采用微软自带类实现
    [EnableCors("AllowSpecificOrigin")]
    public class TokenController : Controller
    {
       
        [HttpPost]
        [Route("api/[controller]/Create")]
        public IActionResult Create([FromBody]LoginModel inputModel)
        {
            if (inputModel.Account != "admin" && inputModel.Password != "admin")
                return Unauthorized();

            var token = new JwtTokenBuilder()
                                .AddSecurityKey(JwtSecurityKey.Create("fiver-secret-key"))
                                .AddSubject("james bond")
                                .AddIssuer("Fiver.Security.Bearer")
                                .AddAudience("Fiver.Security.Bearer")
                                .AddClaim("MembershipId", "111")
                                .AddExpiry(1)
                                .Build();

            //return Ok(token);
            return Ok(token);
        }
    }
}