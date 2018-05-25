using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeYu.Intelligence.WebAPI.Data;
using ZeYu.Intelligence.WebAPI.Models;
using Newtonsoft.Json.Linq;

namespace ZeYu.Intelligence.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Order")]
    public class OrderController : ZYBaseController
    {
        private readonly DefaultContext _context;

        public OrderController(DefaultContext context)
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

            int total = _context.Order.Where(model => model.ID.Length > 0).ToList().Count();
            List<Order> list = _context.Order.Where(model => model.ID.Length > 0).OrderByDescending(t => t.CreateTime).OrderByDescending(t => t.ID).Skip(base.skip).Take(base.pageSize).Select(s => s).ToList();

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

            var item = _context.Order.FirstOrDefault(model => model.ID == id);
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
        public APIResponse Post([FromBody]Order model)
        {
            APIResponse response = new APIResponse();
            if (model == null)
            {
                response.Error = 1;
                return response;
            }
            model.CreateTime = (long)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
            model.ID = model.CreateTime.GetHashCode().ToString().Replace("-", "N");
            _context.Order.Add(model);
            _context.SaveChanges();
            return response;
        }

        // PUT: api/UserInfo/5
        [HttpPut("{id}")]
        public APIResponse Put(string id, [FromBody]Order model)
        {
            APIResponse response = new APIResponse();
            if (model == null || model.ID != id)
            {
                response.Error = 1;
                return response;
            }

            var item = _context.Order.FirstOrDefault(m => m.ID == id);
            if (item == null)
            {
                response.Error = 2;
                return response;
            }

            item.Amount = model.Amount;
            item.DeliverStatus = model.DeliverStatus;
            item.Deposit = model.Deposit;
            item.Discount = model.Discount;
            item.PayTypeID = model.PayTypeID;
            item.Remark = model.Remark;
            item.Quantity = model.Quantity;
            item.Status = model.Status;
            item.UnionID = model.UnionID;


            _context.Order.Update(item);
            _context.SaveChanges();
            return response;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public APIResponse Delete(string id)
        {
            APIResponse response = new APIResponse();

            var item = _context.Order.FirstOrDefault(t => t.ID == id);
            if (item == null)
            {
                response.Error = 1;
                return response;
            }
            _context.Order.Remove(item);
            _context.SaveChanges();

            return response;
        }
    }
}