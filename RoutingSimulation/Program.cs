using System;
using System.Collections.Generic;
using RoutingSimulation.Controllers;
using RoutingSimulation.Routing;

namespace RoutingSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            request("homecontroller"); // index
            request("homecontroller/"); // index
            request("homecontroller/index"); // index
            request("homecontroller/index/"); // index
            request("homecontroller/notindex/5"); // notindex
            request("homecontroller/notind"); // 404 not found
        }

        public static void request(string path)
        {
            string result = Router.Route(path);
            Console.WriteLine(result);
        }
    }
}