//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ZeYu.Intelligence.WebAPI.Data
//{
//    public class QueryHelper
//    {
//        public T List<T> GetList(int pageIndex, int pageSize,Dictionary<string,string[]> condition)
//        {
//            using (OASysEntities entity = new OASysEntities())
//            {
//                List<T> list = new List<T>();
//                if (string.IsNullOrEmpty(DepId))
//                {
//                    list = entity.Staff.Where(m => m.StaffName.Contains(staffname) && m.JoinDate >= date).OrderByDescending(m => m.StaffId).Skip(((pageIndex - 1) * pageSize)).Take(pageSize).Select(s => s).ToList();
//                }
//                else
//                {
//                    list = entity.Staff.Where(m => m.StaffName.Contains(staffname) && m.JoinDate >= date && m.DepId.ToString() == DepId).OrderByDescending(m => m.StaffId).Skip(((pageIndex - 1) * pageSize)).Take(pageSize).Select(s => s).ToList();
//                }
//                List<T> list1 = new List<T>();
//                foreach (var s in list)
//                {
//                    s.DepName = entity.Department.SingleOrDefault(m => m.DepId == s.DepId).DePName;
//                    list1.Add(s);
//                }
//                return list1;

//            }
//        }
//    }
//}
