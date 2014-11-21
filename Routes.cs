using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rimango.Plattform
{
    using System.Web.Mvc;
    using System.Web.Routing;

    using Orchard.Mvc.Routes;

    public class Routes : IRouteProvider
    {

        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var routeDescriptor in GetRoutes()) 
                routes.Add(routeDescriptor);
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new[]
                       {
                           new RouteDescriptor
                               {
                                   Name = "PlattformRegister",
                                   Priority = 6,
                                   Route =
                                       new Route(
                                       "Plattform/Register",
                                       new RouteValueDictionary
                                           {
                                               { "area", "Rimango.Plattform" },
                                               { "controller", "User" },
                                               { "action", "Register" }
                                           },
                                       new RouteValueDictionary(),
                                       new RouteValueDictionary { { "area", "Orchard.Plattform" } },
                                       new MvcRouteHandler())
                               }
                       };
        }
    }
}