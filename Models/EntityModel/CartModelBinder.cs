// Decompiled with JetBrains decompiler
// Type: RemainsTest.Models.EntityModel.CartModelBinder
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

using System.Web.Mvc;

namespace RemainsTest.Models.EntityModel
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
