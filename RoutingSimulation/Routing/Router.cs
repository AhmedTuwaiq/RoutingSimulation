using System;
using System.Collections.Generic;
using System.Reflection;

namespace RoutingSimulation.Routing
{
    public class Router
    {
        private static List<Controller> controllers;

        static Router()
        {
            controllers = Loader.LoadControllers();
        }

        public static string Route(string path)
        {
            if (controllers.Count == 0)
                return NotFound();
            
            var data = path.ToLower().Split("/");
            LastProvided lastProvided = data.Length == 1 ? LastProvided.Controller : 
                                        data.Length == 2 ? LastProvided.Action :
                                                           LastProvided.Id;
            
            var controllerName = data[0];
            var action =
                (lastProvided == LastProvided.Action || lastProvided == LastProvided.Id) && data[1].Length > 0 ? data[1] : "index";

            foreach (var controller in controllers)
            {
                if (controller.GetType().Name.ToLower() == controllerName)
                {
                    MethodInfo methodInfo = getAction(controller, action);
                    
                    if (methodInfo == null)
                        return NotFound();

                    /*
                     * I have to make the router parse the parameters properly :D
                     * for now you have to pretend you didn't see this please </3
                     */
                    object result = lastProvided == LastProvided.Id && data[2].Length > 0 ?
                        methodInfo.Invoke(controller, new object[] {int.Parse(data[2])}) :
                        methodInfo.Invoke(controller, new object[] {});

                    return result == null ? "action found but returned null" : result.ToString();
                }
            }
            
            return NotFound();
        }

        public static MethodInfo getAction(Controller controller, string actionName)
        {
            foreach (var methodInfo in controller.GetType().GetMethods())
            {
                if (methodInfo.Name.ToLower() == actionName.ToLower())
                    return methodInfo;
            }
            return null;
        }

        public static string NotFound()
        {
            return "404 not found";
        }
    }
}