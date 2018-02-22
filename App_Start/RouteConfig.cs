// Decompiled with JetBrains decompiler
// Type: RemainsTest.RouteConfig
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

using System.Web.Mvc;
using System.Web.Routing;

namespace RemainsTest
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
      routes.MapRoute("Default", "{controller}/{action}/{id}", new
      {
        controller = "Account",
        action = "Login",
        id = UrlParameter.Optional
      });
      routes.MapRoute("login", "login", (object) new
      {
        controller = "Account",
        action = "Login",
        id = UrlParameter.Optional
      });
      routes.MapRoute("details", "products/details", new
      {
        controller = "Product",
        action = "Details",
        id = UrlParameter.Optional
      });
    }
  }
}
