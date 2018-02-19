// Decompiled with JetBrains decompiler
// Type: RemainsTest.Controllers.AccountController
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

using NLog;
using RemainsTest.Models;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RemainsTest.Controllers
{
  [AllowAnonymous]
  public class AccountController : Controller
  {
    private static Logger logger = LogManager.GetCurrentClassLogger();

    public ActionResult Login()
    {
      return (ActionResult) this.View();
    }

    [HttpPost]
    public ActionResult Login(LogOnModel model, string returnUrl)
    {
      if (this.ModelState.IsValid)
      {
        if (Membership.ValidateUser(model.UserName, model.Password))
        {
          AccountController.logger.Info("Авторизация. Пользователь - " + model.UserName + ".");
          FormsAuthentication.SetAuthCookie(model.UserName, true);
          if (this.Url.IsLocalUrl(returnUrl))
            return (ActionResult) this.Redirect(returnUrl);
          return (ActionResult) this.RedirectToAction("List", "Product", (object) new
          {
            groupId = 0
          });
        }
        this.ModelState.AddModelError("", "Неправильный пароль или логин");
      }
      return (ActionResult) this.View((object) model);
    }

    [Authorize(Roles = "admin")]
    public ActionResult CreateUser()
    {
      return (ActionResult) this.View();
    }

    [HttpPost]
    public ActionResult CreateUser(UserCreateModel model)
    {
      if (model.Password != model.PasswordRepeat)
        this.ModelState.AddModelError("Password", "Введенные пароли не совпадают.");
      if (!this.ModelState.IsValid)
        return (ActionResult) this.View((object) model);
      MembershipCreateStatus status;
      Membership.Provider.CreateUser(model.Login, model.Password, model.Login, model.Company, "", true, (object) null, out status);
      if (status == MembershipCreateStatus.Success)
        return (ActionResult) this.View();
      return (ActionResult) this.View();
    }

    [Authorize]
    public ActionResult LogOff()
    {
      this.Session.Clear();
      FormsAuthentication.SignOut();
      return (ActionResult) this.RedirectToAction("Login", "Account");
    }

    [Authorize]
    public ActionResult ChangePassword()
    {
      return (ActionResult) this.View();
    }

    [HttpPost]
    public ActionResult ChangePassword(ChangePasswordModel model)
    {
      if (this.ModelState.IsValid)
      {
        if (Membership.Provider.GetUser(HttpContext.Current.User.Identity.Name, true) != null && Membership.Provider.ChangePassword(HttpContext.Current.User.Identity.Name, model.OldPassword, model.NewPassword))
          return (ActionResult) this.RedirectToAction("List", "Product");
        this.ModelState.AddModelError("", "Что то пошло не так.");
      }
      return (ActionResult) this.View();
    }
  }
}
