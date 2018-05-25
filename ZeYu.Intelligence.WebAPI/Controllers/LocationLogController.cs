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
    [Route("api/LocationLog")]
    public class LocationLogController : ZYBaseController
    {
        private readonly DefaultContext _context;

        public LocationLogController(DefaultContext context)
        {
            _context = context;
        }
        // GET: api/UserInfo
        [HttpGet]
        public APIResponse Get()
        {
            ZYBaseController.SetPager(HttpContext, ref base.pageIndex, ref base.pageSize, ref base.skip);

            string terminalID = HttpContext.Request.Query["terminalID"];
            Expression<Func<LocationLog, bool>> expression = null;
            if (!string.IsNullOrEmpty(terminalID))
            {
                expression = model => model.TerminalID == terminalID;
            }
            else
            {
                expression = model => model.TerminalID!=null;
            }

            int total = _context.LocationLog.Where(expression).ToList().Count();
            List<LocationLog> list = _context.LocationLog.Where(expression).OrderByDescending(t => t.SamplingTime).OrderByDescending(t => t.ID).Skip(base.skip).Take(base.pageSize).Select(s => s).ToList();

            JObject body = new JObject();

            APIResponse response = new APIResponse();
            body.Add("Total", total);
            body.Add("List", JArray.FromObject(list));
            response.Body = body;
            return response;
        }

        // GET: api/UserInfo/5
        [HttpGet("{id}")]
        public APIResponse Get(long id)
        {
            APIResponse response = new APIResponse();

            var item = _context.LocationLog.FirstOrDefault(model => model.ID == id);
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
        public APIResponse Post([FromBody]LocationLog model)
        {
            APIResponse response = new APIResponse();
            if (model == null)
            {
                response.Error = 1;
                return response;
            }
            //model.SamplingTime = (long)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
             
            _context.LocationLog.Add(model);
            _context.SaveChanges();
            return response;
        }

        // PUT: api/UserInfo/5
        [HttpPut("{id}")]
        public APIResponse Put(int id, [FromBody]LocationLog model)
        {
            APIResponse response = new APIResponse();
            if (model == null || model.ID != id)
            {
                response.Error = 1;
                return response;
            }

            var item = _context.LocationLog.FirstOrDefault(m => m.ID == id);
            if (item == null)
            {
                response.Error = 2;
                return response;
            }

            item.TerminalID = model.TerminalID;
            item.GDLocation = model.GDLocation;
            item.BDLocation = model.BDLocation;
            item.Latitude = model.Latitude;
            item.Longitude = model.Longitude;
            item.StationID = model.StationID;



            _context.LocationLog.Update(item);
            _context.SaveChanges();
            return response;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public APIResponse Delete(int id)
        {
            APIResponse response = new APIResponse();

            var item = _context.LocationLog.FirstOrDefault(t => t.ID == id);
            if (item == null)
            {
                response.Error = 1;
                return response;
            }
            _context.LocationLog.Remove(item);
            _context.SaveChanges();

            return response;
        }
    }
}