using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RoutingSimulation.Routing
{
    public class Loader
    {
        public static List<Controller> LoadControllers()
        {
            List<Controller> controllers = new();
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.Namespace == "RoutingSimulation.Controllers");
            
            foreach (var type in types)
                controllers.Add(Activator.CreateInstance(type) as Controller);
            
            return controllers;
        }
    }
}