using Com.Ctrip.Framework.Apollo.ConfigAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denggaopan.ApolloNet461Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Apollo!");

            YamlConfigAdapter.Register();

            var demo = new ConfigurationDemo();

            Console.WriteLine("Apollo Config Demo. Please input key to get the value. Input quit to exit.");
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
                demo.GetConfig(input);
            }
        }
    }
}
