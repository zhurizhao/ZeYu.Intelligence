using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZeYu.Intelligence.WebAPI.Controllers
{
    public class ZYBaseController : Controller
    {

        //未完成
        public int pageIndex = 1;
        public int pageSize = 1000;
        public int skip = 0;
        public ZYBaseController()
        {
            //int.TryParse(HttpContext.Request.Query["pageIndex"], out this.pageIndex);
            //this.pageIndex = this.pageIndex == 0 ? 1 : this.pageIndex;
            //int.TryParse(HttpContext.Request.Query["pageSize"], out this.pageSize);
            //this.pageSize = this.pageSize == 0 ? 10 : this.pageSize;
            //this.skip = (this.pageIndex - 1) * this.pageSize;

        }


        public static void SetPager(HttpContext context,ref int pageIndex, ref int pageSize, ref int skip)
        {
            int.TryParse(context.Request.Query["pageIndex"], out pageIndex);
            pageIndex = pageIndex == 0 ? 1 :pageIndex;
            int.TryParse(context.Request.Query["pageSize"], out pageSize);
            pageSize = pageSize == 0 ? 1000 :pageSize;
            skip = (pageIndex - 1) * pageSize;
        }

       
    }
}
