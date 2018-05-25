using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ZeYu.Intelligence.WebAPI.Models
{
    public class QueryResult
    {
        [Key]
        public Int64 Val { get; set; }
    }
}
