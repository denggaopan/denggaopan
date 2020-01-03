using System;
using Com.Ctrip.Framework.Apollo.OpenApi;
using Com.Ctrip.Framework.Apollo.OpenApi.Model;

namespace Denggaopan.ApolloOpenApiDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Apollo!");

            var portalUrl = "http://10.111.211.17:8070/";// "http://106.54.227.205:8070"; // portal url
            var token = "f3ab791f808063530553bd5492f52c66489dc021";// "4c6fa0d0b51458325dee10a2dd03f57a1e89606f"; // 申请的token
            var appId = "apollo-demo";
            var env = "PRO";
            var user = "apollo";

            var opt = new OpenApiOptions();
            opt.PortalUrl = new Uri(portalUrl);
            opt.Token = token;
            IOpenApiFactory oaf = new OpenApiFactory(opt);
            var nsc = oaf.CreateNamespaceClient(appId, env);

            Console.WriteLine("Apollo OpenApi Demo. Input quit to exit.");
            Console.WriteLine("1.Please input 'create key,value' to create item.");
            Console.WriteLine("2.Please input 'update key,value' to update item.");
            Console.WriteLine("3.Please input 'remove key' to remove item.");
            Console.WriteLine("4.Please input 'get key' to get item.");
            Console.WriteLine("5.Please input 'publish xxx' to publish.");
            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    continue;
                }
                input = input.Trim();
                if (input.Equals("quit", StringComparison.CurrentCultureIgnoreCase))
                {
                    Environment.Exit(0);
                }
                if(input.ToLower().StartsWith("publish "))
                {
                    var nsr = new NamespaceRelease();
                    nsr.ReleasedBy = user;
                    nsr.ReleaseTitle = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}-release";
                    nsr.ReleaseComment = input.Substring(8);
                    nsc.Publish(nsr);
                    consoleWriteLine("发布成功！");
                }else if (input.ToLower().StartsWith("create "))
                {
                    var kv = input.Substring(7);
                    var arr = kv.Split(new string[] { ",", "，" }, StringSplitOptions.None);
                    if (arr.Length == 2)
                    {
                        var item = new Item();
                        item.Key = arr[0];
                        item.Value = arr[1];
                        item.DataChangeCreatedBy = user;
                        item.DataChangeCreatedTime = DateTime.Now;
                        var r = nsc.CreateItem(item).Result;
                        var color = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Red;
                        consoleWriteLine("设置成功！");
                        Console.ForegroundColor = color;
                    }
                    else
                    {
                        consoleWriteLine("格式错误！");
                    }
                }else if (input.ToLower().StartsWith("update "))
                {
                    var kv = input.Substring(7);
                    var arr = kv.Split(new string[] { ",", "，" }, StringSplitOptions.None);
                    if (arr.Length == 2)
                    {
                        var item = new Item();
                        item.Key = arr[0];
                        item.Value = arr[1];
                        item.DataChangeLastModifiedBy = user;
                        item.DataChangeLastModifiedTime = DateTime.Now;
                        var r = nsc.UpdateItem(item);
                        consoleWriteLine("更新成功！");
                    }
                    else
                    {
                        consoleWriteLine("格式错误！");
                    }
                }else if (input.ToLower().StartsWith("remove "))
                {
                    var key = input.Substring(7);
                    var r = nsc.RemoveItem(key, user).Result;
                    if (r)
                    {
                        consoleWriteLine("删除成功！");
                    }
                    else
                    {
                        consoleWriteLine("删除失败！");
                    }
                }
                else if(input.ToLower().StartsWith("get "))
                {
                    var key = input.Substring(4);
                    var item = nsc.GetItem(key).Result;
                    if(item != null)
                    {
                        consoleWriteLine($"key:{key},value:{item.Value}");
                    }
                    else
                    {
                        consoleWriteLine("获取失败！");
                    }
                }
                else
                {
                    consoleWriteLine("无效指令！");
                }
            }
        }

        private static void consoleWriteLine(string text)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = color;
        }


    }
}
