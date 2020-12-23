using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Denggaopan.DapperDemo.Entities;
using Newtonsoft.Json;

namespace Denggaopan.DapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var connstr = "Data Source=.;User ID=sa;Pwd=F42DFC74@King;Initial Catalog=king;Persist Security Info=True";
            var db = new SqlConnection(connstr);

            var data = db.Query<JK_ThreeclssBusinessConfig>("select *,100 as MyProperty from jk_threeclssbusinessconfig");
            Console.WriteLine($"data:{JsonConvert.SerializeObject(data)}");
            Console.WriteLine();

            var BusinessGuid = "a6bfc910e9d04629b449654bc94d7256";
            var tbc = db.QueryFirstOrDefault<JK_ThreeclssBusinessConfig>("select *,100 as MyProperty from jk_threeclssbusinessconfig where businessguid=@businessguid", new { BusinessGuid });
            Console.WriteLine($"tbc:{JsonConvert.SerializeObject(tbc)}");
            Console.WriteLine();

            /*insert*/
            var now = DateTime.Now;
            var obj = new { BusinessGuid = Guid.NewGuid().ToString(), IsActive = true, CreateTime = now, UpdateTime = now };
            var res1 = db.Execute("insert into jk_threeclssbusinessconfig (businessguid,IsActive,CreateTime,UpdateTime) values (@businessguid,@IsActive,@CreateTime,@UpdateTime)", obj);
            Console.WriteLine($"res1:{res1}");
            Console.WriteLine();

            var model = new JK_ThreeclssBusinessConfig { BusinessGuid = Guid.NewGuid().ToString(), IsActive = true, CreateTime = now, UpdateTime = now };
            var res2 = db.Execute("insert into jk_threeclssbusinessconfig (businessguid,IsActive,CreateTime,UpdateTime) values (@businessguid,@IsActive,@CreateTime,@UpdateTime)", model);
            Console.WriteLine($"res2:{res2}");
            Console.WriteLine();

            var obj1 = new { BusinessGuid = Guid.NewGuid().ToString(), IsActive = true, CreateTime = now, UpdateTime = now };
            var obj2 = new { BusinessGuid = Guid.NewGuid().ToString(), IsActive = true, CreateTime = now, UpdateTime = now };
            var objs = new[] { obj1, obj2 };
            var res3 = db.Execute("insert into jk_threeclssbusinessconfig (businessguid,IsActive,CreateTime,UpdateTime) values (@businessguid,@IsActive,@CreateTime,@UpdateTime)", objs);
            Console.WriteLine($"res3:{res3}");
            Console.WriteLine();

            var model1 = new JK_ThreeclssBusinessConfig { BusinessGuid = Guid.NewGuid().ToString(), IsActive = true, CreateTime = now, UpdateTime = now };
            var model2 = new JK_ThreeclssBusinessConfig { BusinessGuid = Guid.NewGuid().ToString(), IsActive = true, CreateTime = now, UpdateTime = now };
            var models = new[] { model1, model2 };
            var res4 = db.Execute("insert into jk_threeclssbusinessconfig (businessguid,IsActive,CreateTime,UpdateTime) values (@businessguid,@IsActive,@CreateTime,@UpdateTime)", models);
            Console.WriteLine($"res4:{res4}");
            Console.WriteLine();

            /*update*/
            var now1 = DateTime.Now;
            var param = new { BusinessGuid = "284dcbaf-b75b-4fdf-a739-9d0d0e364665", IsActive = false, UpdateTime = now1 };
            var res5 = db.Execute("update jk_threeclssbusinessconfig set IsActive=@IsActive,UpdateTime=@UpdateTime where businessguid=@businessguid", param);
            Console.WriteLine($"res5:{res5}");
            Console.WriteLine();



            Console.ReadLine();
        }
    }
}
