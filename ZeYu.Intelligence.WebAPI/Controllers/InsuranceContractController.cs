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
    [Route("api/InsuranceContract")]
    public class InsuranceContractController : ZYBaseController
    {
        private readonly DefaultContext _context;

        public InsuranceContractController(DefaultContext context)
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

            int total = _context.InsuranceContract.Where(model => model.ID.Length > 0).ToList().Count();
            List<InsuranceContract> list = _context.InsuranceContract.Where(model => model.ID.Length > 0).OrderByDescending(t => t.CreateTime).OrderByDescending(t => t.ID).Skip(base.skip).Take(base.pageSize).Select(s => s).ToList();

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

            var item = _context.InsuranceContract.FirstOrDefault(model => model.ID == id);
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
        public APIResponse Post([FromBody]InsuranceContract model)
        {
            APIResponse response = new APIResponse();
            if (model == null)
            {
                response.Error = 1;
                return response;
            }
            model.CreateTime = (long)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
            model.ID = model.CreateTime.GetHashCode().ToString().Replace("-", "N");
            _context.InsuranceContract.Add(model);
            _context.SaveChanges();
            return response;
        }

        // PUT: api/UserInfo/5
        [HttpPut("{id}")]
        public APIResponse Put(string id, [FromBody]InsuranceContract model)
        {
            APIResponse response = new APIResponse();
            if (model == null || model.ID != id)
            {
                response.Error = 1;
                return response;
            }

            var item = _context.InsuranceContract.FirstOrDefault(m => m.ID == id);
            if (item == null)
            {
                response.Error = 2;
                return response;
            }

            item.Applicant = model.Applicant;
            item.Beneficiary = model.Beneficiary;
            item.EndTime = model.EndTime;
            item.InsuranceCompany = model.InsuranceCompany;
            item.InsuranceCompanyID = model.InsuranceCompanyID;
            item.Remark = model.Remark;
            item.InsuredObject = model.InsuredObject;
            item.OrderID = model.OrderID;
            item.StartTime = model.StartTime;
            item.Status = model.Status;
            item.UserID = model.UserID;

            _context.InsuranceContract.Update(item);
            _context.SaveChanges();
            return response;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public APIResponse Delete(string id)
        {
            APIResponse response = new APIResponse();

            var item = _context.InsuranceContract.FirstOrDefault(t => t.ID == id);
            if (item == null)
            {
                response.Error = 1;
                return response;
            }
            _context.InsuranceContract.Remove(item);
            _context.SaveChanges();

            return response;
        }
    }
}