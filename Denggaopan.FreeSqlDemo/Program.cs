using Denggaopan.FreeSqlDemo.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denggaopan.FreeSqlDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            var list = DbHelper.Fsql.Select<ReserveOrder>().Where(a=>a.CreateTime > DateTime.Now.AddDays(-1)).ToList();
            var json = JsonConvert.SerializeObject(list);
            Console.WriteLine(json);

            var last = DbHelper.Fsql.Select<ReserveOrder>().OrderByDescending(a => a.CreateTime).First();
            var repo = DbHelper.Fsql.GetRepository<ReserveOrder>();
            DbHelper.Fsql.Update<ReserveOrder>().Set(a => a.TsOrderId, 666)
                .Where(a => a.ReserveOrderID == last.ReserveOrderID)
                .ExecuteAffrows();
            Console.ReadLine();
        }
    }
}
