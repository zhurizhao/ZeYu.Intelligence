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
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ZeYu.Intelligence.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/Terminal")]
    public class TerminalController : ZYBaseController
    {

        private readonly DefaultContext _context;
        public TerminalController(DefaultContext context)
        {
            _context = context;

            //if (_context.Terminal.Count() == 0)
            //{
            //    _context.Terminal.Add(new TodoItem { Name = "Item1" });
            //    _context.SaveChanges();
            //}
        }

        // GET: api/Terminal
        [HttpGet]
        public APIResponse Get()
        {
            ZYBaseController.SetPager(HttpContext, ref base.pageIndex,ref base.pageSize,ref base.skip);

            bool bdLocationHasValue = false;
            bool.TryParse(HttpContext.Request.Query["bdLocationHasValue"],out bdLocationHasValue);

            int total = _context.ViewTerminalDetail.Where(t=>t.BDLocation.Length>0).ToList().Count();
            List<ViewTerminalDetail> list = _context.ViewTerminalDetail.Where(t => t.BDLocation.Length > 0).OrderByDescending(t => t.AddDate).OrderByDescending(t => t.ID).Skip(base.skip).Take(base.pageSize).Select(s => s).ToList();

            JObject body = new JObject();

            APIResponse response = new APIResponse();
            body.Add("Total", total);
            body.Add("List", JArray.FromObject(list));
            response.Body = body;
            return response;
        }

        // GET: api/Terminal/5
        [HttpGet("{id}", Name = "Get")]
        public APIResponse Get(int id)
        {
            APIResponse response = new APIResponse();

            var item = _context.ViewTerminalDetail.FirstOrDefault(t => t.ID == id);
            if (item == null)
            {
                response.Error = 1;
                return response;
            }

            response.Body = item;
            return response;
        }
        
        // POST: api/Terminal
        [HttpPost]
        public APIResponse Post([FromBody]ViewTerminalDetail model)
        {
            APIResponse response = new APIResponse();
            if (model == null)
            {
                response.Error = 1;
                return response;
            }
            model.AddDate=(long)(DateTime.Now- new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
            _context.ViewTerminalDetail.Add(model);
            _context.SaveChanges();
            return response;
        }
        
        // PUT: api/Terminal/5
        [HttpPut("{id}")]
        public APIResponse Put(int id, [FromBody]ViewTerminalDetail model) //[FromBody]JObject model
        {
            APIResponse response = new APIResponse();
            if (model == null || model.ID != id)
            {
                response.Error = 1;
                return response;
            }

            var item = _context.ViewTerminalDetail.FirstOrDefault(m => m.ID == id);
            if (item == null)
            {
                response.Error = 2;
                return response;
            }

            item.IMEI = model.IMEI;
            item.MAC = model.MAC;
            item.FirmwareVersion = model.FirmwareVersion;
            //item.GroupID = model.GroupID;
            item.BDLocation = model.BDLocation;
            item.ModelNumber  = model.ModelNumber;
            item.ReceiveHost = model.ReceiveHost;
            item.ReceiveTCPPort = model.ReceiveTCPPort;
            item.ReceiveUDPPort = model.ReceiveUDPPort;
            item.Status = model.Status;
            item.TypeID = model.TypeID;
            item.Address= model.Address;

            _context.ViewTerminalDetail.Update(item);
            _context.SaveChanges();
            return response;
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public APIResponse Delete(int id)
        {
            APIResponse response = new APIResponse();

            var item = _context.ViewTerminalDetail.FirstOrDefault(t => t.ID == id);
            if (item == null)
            {
                response.Error = 1;
                return response;
            }

            _context.ViewTerminalDetail.Remove(item);
            _context.SaveChanges();

            

            return response;

        }
    }
}
