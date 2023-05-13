using Microsoft.AspNetCore.Mvc.Routing;
using System;

namespace BNS.Api.Route
{
    public class AppRouteControllerAttribute : Attribute, IRouteTemplateProvider
    {
        public string Template => "{organization}/api/[controller]";
        public int? Order => null;
        public string Name { get; set; } = string.Empty;
    }
}
