using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ZeYu.Intelligence.WebAPI.Data;
using ZeYu.Intelligence.WebAPI.Models;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ZeYu.Intelligence.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/SIMCard")]
    public class SIMCardController: ZYBaseController
    {

        private readonly DefaultContext _context;
        public SIMCardController(DefaultContext context)
        {
            _context = context;
        }

        [HttpGet]
        public APIResponse Get()
        {
            ZYBaseController.SetPager(HttpContext, ref base.pageIndex, ref base.pageSize, ref base.skip);

            string keywords = HttpContext.Request.Query["keywords"];
            Expression<Func<ViewSIMCardDetail, bool>> expression = null;
            if (!string.IsNullOrEmpty(keywords))
            {
                expression = model => model.ICCID.Contains(keywords)
                    || model.NO.Contains(keywords)
                    || model.ISMI.Contains(keywords)
                    || model.SupplierName.Contains(keywords);

            }

            int total = 0;
            List<ViewSIMCardDetail> list = null;
            if (expression == null)
            {
                total = _context.ViewSIMCardDetail.ToList().Count();
                list = _context.ViewSIMCardDetail.OrderByDescending(t => t.AddTime).Skip(base.skip).Take(base.pageSize).Select(s => s).ToList();
            }
            else
            {
                total = _context.ViewSIMCardDetail.Where(expression).ToList().Count();
                list = _context.ViewSIMCardDetail.Where(expression).OrderByDescending(t => t.AddTime).OrderByDescending(t => t.ID).Skip(base.skip).Take(base.pageSize).Select(s => s).ToList();
            }

            JObject body = new JObject();

            APIResponse response = new APIResponse();
            body.Add("Total", total);
            body.Add("List", JArray.FromObject(list));
            response.Body = body;
            return response;
        }
    }
}