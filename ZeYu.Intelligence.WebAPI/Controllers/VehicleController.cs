using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeYu.Intelligence.WebAPI.Data;
using ZeYu.Intelligence.WebAPI.Models;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace ZeYu.Intelligence.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Vehicle")]
    public class VehicleController : ZYBaseController
    {
        private readonly DefaultContext _context;

        public VehicleController(DefaultContext context)
        {
            _context = context;
        }
        // GET: api/UserInfo
        [HttpGet]
        public APIResponse Get()
        {
            ZYBaseController.SetPager(HttpContext, ref base.pageIndex, ref base.pageSize, ref base.skip);

            bool bdLocationHasValue = false;
            bool.TryParse(HttpContext.Request.Query["bdLocationHasValue"], out bdLocationHasValue);

            string keywords = HttpContext.Request.Query["keywords"];
            Expression<Func<ViewVehicle, bool>> expression = null;
            if (!string.IsNullOrEmpty(keywords))
            {
                    expression = model => model.PolicyNo.Contains(keywords)
                        ||model.RealName.Contains(keywords)
                        ||model.VIN.Contains(keywords)
                        ||model.TerminalID.Contains(keywords);
                
            }

            int total = 0;
            List<ViewVehicle> list = null;
            if (expression == null)
            {
                total = _context.ViewVehicle.ToList().Count();
                list = _context.ViewVehicle.OrderByDescending(t => t.CreateTime).OrderByDescending(t => t.ID).Skip(base.skip).Take(base.pageSize).Select(s => s).ToList();
            }
            else
            {
                total = _context.ViewVehicle.Where(expression).ToList().Count();
                list = _context.ViewVehicle.Where(expression).OrderByDescending(t => t.CreateTime).OrderByDescending(t => t.ID).Skip(base.skip).Take(base.pageSize).Select(s => s).ToList();
            }


            JObject body = new JObject();

            APIResponse response = new APIResponse();
            body.Add("Total", total);
            body.Add("List", JArray.FromObject(list));
            response.Body = body;
            return response;
        }

        // GET: api/UserInfo/5
        [HttpGet("{id}")]
        public APIResponse Get(string id)
        {
            APIResponse response = new APIResponse();

            var item = _context.Vehicle.FirstOrDefault(model => model.ID == id);
            if (item == null)
            {
                response.Error = 1;
                return response;
            }

            response.Body = item;
            return response;
        }

        // POST: api/UserInfo
        [HttpPost]
        public APIResponse Post([FromBody]Vehicle model)
        {
            APIResponse response = new APIResponse();
            if (model == null)
            {
                response.Error = 1;
                return response;
            }
            model.CreateTime = (long)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
            model.ID = model.CreateTime.GetHashCode().ToString().Replace("-", "N");
            _context.Vehicle.Add(model);
            _context.SaveChanges();
            return response;
        }

        // PUT: api/UserInfo/5
        [HttpPut("{id}")]
        public APIResponse Put(string id, [FromBody]Vehicle model)
        {
            APIResponse response = new APIResponse();
            if (model == null || model.ID != id)
            {
                response.Error = 1;
                return response;
            }

            var item = _context.Vehicle.FirstOrDefault(m => m.ID == id);
            if (item == null)
            {
                response.Error = 2;
                return response;
            }

            item.Brand = model.Brand;
            item.Insured = model.Insured;
            item.ModelNumber = model.ModelNumber;
            item.PlateNumber = model.PlateNumber;
            item.PolicyNo = model.PolicyNo;
            item.RegisterTime = model.RegisterTime;
            item.Remark = model.Remark;
            item.TerminalID = model.TerminalID;
            item.VIN = model.VIN;
            item.UserID = model.UserID;

            _context.Vehicle.Update(item);
            _context.SaveChanges();
            return response;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public APIResponse Delete(string id)
        {
            APIResponse response = new APIResponse();

            var item = _context.Vehicle.FirstOrDefault(t => t.ID == id);
            if (item == null)
            {
                response.Error = 1;
                return response;
            }
            _context.Vehicle.Remove(item);
            _context.SaveChanges();

            return response;
        }
    }
}