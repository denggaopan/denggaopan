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
          .UseConnectionString(FreeSql.DataType.SqlServer, @"Data Source=.;User ID=sa;Pwd=F42DFC74@King;Initial Catalog=King;Pooling=true;Min Pool Size=1")
          .UseAutoSyncStructure(false) 
          .Build();
    }
}
