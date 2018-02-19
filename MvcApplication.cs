// Decompiled with JetBrains decompiler
// Type: RemainsTest.MvcApplication
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

using NLog;
using RemainsTest.Models.EntityModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RemainsTest
{
  public class MvcApplication : HttpApplication
  {
    private static Logger logger = LogManager.GetCurrentClassLogger();

    protected void Application_Start()
    {
      MvcApplication.logger.Info("App start");
      AreaRegistration.RegisterAllAreas();
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      ModelBinders.Binders.Add(typeof (Cart), (IModelBinder) new CartModelBinder());
    }

    protected void Session_Start()
    {
    }
  }
}
