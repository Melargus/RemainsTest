using System.Web.Mvc;

namespace OnlineRemains.Models.EntityModel
{
  public class CartModelBinder : IModelBinder
  {
    private const string SessionKey = "Cart";

    public object BindModel(ControllerContext cntx, ModelBindingContext mbcntx)
    {
      Cart cart = (Cart) null;
      if (cntx.HttpContext.Session != null)
        cart = (Cart) cntx.HttpContext.Session["Cart"];
      if (cart == null)
      {
        cart = new Cart();
        if (cntx.HttpContext.Session != null)
          cntx.HttpContext.Session["Cart"] = (object) cart;
      }
      return (object) cart;
    }
  }
}
