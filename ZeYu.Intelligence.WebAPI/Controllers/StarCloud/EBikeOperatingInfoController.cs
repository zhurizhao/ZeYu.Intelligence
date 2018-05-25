using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZeYu.Intelligence.WebAPI.Models;
using Newtonsoft.Json.Linq;
using ZeYu.Intelligence.WebAPI.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace ZeYu.Intelligence.WebAPI.Controllers.StarCloud
{
    [Produces("application/json")]
    //[Route("api/EBikeOperatingData")]
    public class EBikeOperatingInfoController : Controller
    {
        private readonly DBStarCloudContext _context;
        public EBikeOperatingInfoController(DBStarCloudContext context)
        {
            _context = context;
        }
        // GET: api/EBikeOperatingData
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/EBikeOperatingData
        [HttpGet]
        [Route("api/[controller]/GetRideCountByTime")]
        public APIResponse GetRideCountByTime()
        {
            double startTime = 1511132400;
            double endTime = 1511134200;

            var parm0 = new MySqlParameter();
            parm0.DbType = System.Data.DbType.UInt32;
            parm0.ParameterName = "StartTime";
            parm0.Value = startTime;

            var parm1 = new MySqlParameter();
            parm1.DbType = System.Data.DbType.UInt32;
            parm1.ParameterName = "EndTime";
            parm1.Value = endTime;

            var count = _context.Set<QueryResult>().FromSql("CALL ProcCountRideByTime(@StartTime,@EndTime);", new MySqlParameter[] { parm0, parm1 }).ToList();

            APIResponse response = new APIResponse();
            response.Body = count;
            //body.Add("Total", total);
            //body.Add("List", JArray.FromObject(list));
            //response.Body = body;
            return response;
        }

        //// GET: api/EBikeOperatingData/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
        
        //// POST: api/EBikeOperatingData
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}
        
        //// PUT: api/EBikeOperatingData/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}
        
        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
