using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletApi
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = "http://localhost:5000/";

            //use of owin host for a lightweight web api that does not rely on IIS.
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"WebApp started at {baseAddress}");
                Console.ReadKey();
            }
        }
    }
}
