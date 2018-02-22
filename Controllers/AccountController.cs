using System.Web.Mvc;
using System.Web.Security;
using NLog;
using RemainsTest.Models;

namespace RemainsTest.Controllers
{
  [AllowAnonymous]
  public class AccountController : Controller
  {
    private static Logger logger = LogManager.GetCurrentClassLogger();

    public ActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Login(LogOnModel model, string returnUrl)
    {
      if (ModelState.IsValid)
      {
        if (Membership.ValidateUser(model.UserName, model.Password))
        {
          logger.Info("Авторизация. Пользователь - " + model.UserName + ".");
          FormsAuthentication.SetAuthCookie(model.UserName, true);
          if (Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);
          return RedirectToAction("List", "Product", new
          {
            groupId = 0
          });
        }
        ModelState.AddModelError("", "Неправильный пароль или логин");
      }
      return View(model);
    }

    [Authorize(Roles = "admin")]
    public ActionResult CreateUser()
    {
      return View();
    }

    [HttpPost]
    public ActionResult CreateUser(UserCreateModel model)
    {
      if (model.Password != model.PasswordRepeat)
        ModelState.AddModelError("Password", "Введенные пароли не совпадают.");
      if (!ModelState.IsValid)
        return View(model);
      MembershipCreateStatus status;
      Membership.Provider.CreateUser(model.Login, model.Password, model.Login, model.Company, "", true, null, out status);
      if (status == MembershipCreateStatus.Success)
        return View();
      return View();
    }

    [Authorize]
    public ActionResult LogOff()
    {
      Session.Clear();
      FormsAuthentication.SignOut();
      return RedirectToAction("Login", "Account");
    }

    [Authorize]
    public ActionResult ChangePassword()
    {
      return View();
    }

    [HttpPost]
    public ActionResult ChangePassword(ChangePasswordModel model)
    {
      if (ModelState.IsValid)
      {
        if (Membership.Provider.GetUser(HttpContext.User.Identity.Name, true) != null && Membership.Provider.ChangePassword(HttpContext.User.Identity.Name, model.OldPassword, model.NewPassword))
          return RedirectToAction("List", "Product");
        ModelState.AddModelError("", "Что то пошло не так.");
      }
      return View();
    }
  }
}
