using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denggaopan.FreeSqlDemo
{
    public class DbHelper
    {
        public static IFreeSql Fsql = new FreeSql.FreeSqlBuilder()
          .UseConnectionString(FreeSql.DataType.SqlServer, @"Data Source=119.3.129.136;User ID=sa;Pwd=F42DFC74@King;Initial Catalog=king;Persist Security Info=True")
          .UseAutoSyncStructure(false) 
          .Build();
    }
}
