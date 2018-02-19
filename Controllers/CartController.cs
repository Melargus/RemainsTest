// Decompiled with JetBrains decompiler
// Type: RemainsTest.Controllers.CartController
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

using Microsoft.CSharp.RuntimeBinder;
using NReco.PdfGenerator;
using RemainsTest.Models;
using RemainsTest.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RemainsTest.Controllers
{
  [Authorize]
  public class CartController : Controller
  {
    public ActionResult Index(Cart cart)
    {
      return (ActionResult) this.View((object) cart);
    }

    public RedirectResult AddToCart(Cart cart, int productId, int charId, string shk, string quantity, string cutting, string returnUrl)
    {
      DataContext dataContext = new DataContext();
      if ((productId == 0 || charId == 0) && shk.Length > 0)
      {
        ProductCharRemains productCharRemains = dataContext.ProductCharRemains.Where<ProductCharRemains>((Expression<Func<ProductCharRemains, bool>>) (pcr => pcr.Shk == shk)).FirstOrDefault<ProductCharRemains>();
        productId = productCharRemains.ProductId;
        charId = productCharRemains.ProductCharId;
      }
      Product p = dataContext.Products.Where<Product>((Expression<Func<Product, bool>>) (pr => pr.Id == productId)).FirstOrDefault<Product>();
      ProductChars pchar = dataContext.ProductChars.Where<ProductChars>((Expression<Func<ProductChars, bool>>) (pch => pch.Id == charId)).FirstOrDefault<ProductChars>();
      ProductPrice productPrice = dataContext.ProductPrice.Where<ProductPrice>((Expression<Func<ProductPrice, bool>>) (pr => pr.ProductId == productId)).SingleOrDefault<ProductPrice>();
      double price = productPrice != null ? productPrice.Price : 0.0;
      cart.AddItem(p, pchar, shk, double.Parse(quantity.Replace(".", ",")), cutting, price);
      return this.Redirect(returnUrl);
    }

    public RedirectResult RemoveFromCart(Cart cart, Guid line, string returnUrl)
    {
      cart.RemoveItem(line);
      return this.Redirect(returnUrl);
    }

    public ViewResult Checkout()
    {
      List<SelectListItem> selectListItemList1 = new List<SelectListItem>();
      selectListItemList1.Add(new SelectListItem()
      {
        Text = "",
        Value = "-1",
        Selected = true
      });
      selectListItemList1.Add(new SelectListItem()
      {
        Text = "Курьер",
        Value = "0"
      });
      selectListItemList1.Add(new SelectListItem()
      {
        Text = "Самовывоз",
        Value = "1"
      });
      selectListItemList1.Add(new SelectListItem()
      {
        Text = "Доставка",
        Value = "2"
      });
      selectListItemList1.Add(new SelectListItem()
      {
        Text = "Экспресс-доставка",
        Value = "3"
      });
      // ISSUE: reference to a compiler-generated field
      if (CartController.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CartController.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<SelectListItem>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "DeliveryType", typeof (CartController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CartController.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) CartController.\u003C\u003Eo__3.\u003C\u003Ep__0, this.ViewBag, selectListItemList1);
      List<SelectListItem> selectListItemList2 = new List<SelectListItem>();
      selectListItemList2.Add(new SelectListItem()
      {
        Text = "",
        Value = "-1",
        Selected = true
      });
      selectListItemList2.Add(new SelectListItem()
      {
        Text = "Другие способы оплаты",
        Value = "0"
      });
      selectListItemList2.Add(new SelectListItem()
      {
        Text = "Безналичная",
        Value = "1"
      });
      // ISSUE: reference to a compiler-generated field
      if (CartController.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CartController.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<SelectListItem>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "PayType", typeof (CartController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = CartController.\u003C\u003Eo__3.\u003C\u003Ep__1.Target((CallSite) CartController.\u003C\u003Eo__3.\u003C\u003Ep__1, this.ViewBag, selectListItemList2);
      List<SelectListItem> selectListItemList3 = new List<SelectListItem>();
      DbSet<TransportationCompanies> transportationCompanies1 = new DataContext().TransportationCompanies;
      Expression<Func<TransportationCompanies, TransportationCompanies>> selector = (Expression<Func<TransportationCompanies, TransportationCompanies>>) (tcom => tcom);
      foreach (TransportationCompanies transportationCompanies2 in (IEnumerable<TransportationCompanies>) transportationCompanies1.Select<TransportationCompanies, TransportationCompanies>(selector))
        selectListItemList3.Add(new SelectListItem()
        {
          Text = transportationCompanies2.TKName,
          Value = transportationCompanies2.Id.ToString()
        });
      // ISSUE: reference to a compiler-generated field
      if (CartController.\u003C\u003Eo__3.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CartController.\u003C\u003Eo__3.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, List<SelectListItem>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "TK", typeof (CartController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = CartController.\u003C\u003Eo__3.\u003C\u003Ep__2.Target((CallSite) CartController.\u003C\u003Eo__3.\u003C\u003Ep__2, this.ViewBag, selectListItemList3);
      return this.View((object) new ShippingDetailsModel());
    }

    [HttpPost]
    public ViewResult Checkout(Cart cart, ShippingDetailsModel shipping, string DeliveryType, string PayType, string TK)
    {
      string str1 = "";
      string str2 = "";
      string str3 = "";
      string str4 = DeliveryType;
      if (!(str4 == "0"))
      {
        if (!(str4 == "1"))
        {
          if (!(str4 == "2"))
          {
            if (str4 == "3")
              str1 = "Экспресс-доставка";
          }
          else
            str1 = "Доставка";
        }
        else
          str1 = "Самовывоз";
      }
      else
        str1 = "Курьер";
      string str5 = PayType;
      if (!(str5 == "0"))
      {
        if (str5 == "1")
          str2 = "Безналичная";
      }
      else
        str2 = "Другие способы оплаты";
      if (DeliveryType == "2" || DeliveryType == "3")
      {
        DataContext dataContext = new DataContext();
        int itk = int.Parse(TK);
        TransportationCompanies transportationCompanies = dataContext.TransportationCompanies.Where<TransportationCompanies>((Expression<Func<TransportationCompanies, bool>>) (tcom => tcom.Id == itk)).SingleOrDefault<TransportationCompanies>();
        str3 = transportationCompanies != null ? transportationCompanies.TKName : (string) null;
      }
      if (DeliveryType == "-1")
        this.ModelState.AddModelError("Delivery", "Заполните поле Способ доставки.");
      if (PayType == "-1")
        this.ModelState.AddModelError("Pay", "Заполните поле Способ оплаты.");
      if (this.ModelState.IsValid)
      {
        DataContext dataContext = new DataContext();
        try
        {
          StringWriter stringWriter = new StringWriter();
          string str6 = "<html><head><meta charset=\"utf-8\"><meta name = \"viewport\" content = \"width=device-width, initial-scale=1\"></head><body face=\"Arial\">";
          string str7 = "<img src=\"" + this.Server.MapPath("/") + "/Content/img/art.png\">";
          stringWriter.Write(str6);
          stringWriter.Write(str7);
          stringWriter.Write("<br>");
          string str8 = "<br>";
          string str9 = "<div><table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border - collapse:collapse; \">";
          string str10 = "<div><table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border - collapse:collapse; \">";
          string str11 = "<div><table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border - collapse:collapse; \"><tr><th scope=\"col\">Коллекция</th><th scope=\"col\">Артикул</th><th scope=\"col\">Ткань</th><th scope=\"col\">Цвет</th><th scope=\"col\">Штрихкод</th><th scope=\"col\">Разбивка</th><th scope=\"col\">Метраж</th></tr>";
          string str12 = str9 + "<tr><td>Клиент</td><td>" + Membership.Provider.GetUser(HttpContext.Current.User.Identity.Name, true).UserName + "</td><td>Дата заказа</td><td>" + DateTime.Now.ToShortDateString() + "</td></tr>" + "<tr><td>Город</td><td>" + shipping.City.Trim() + "</td><td>Телефон</td><td>" + shipping.Telephone.Trim() + "</td></tr>" + "<tr><td>Способ оплаты</td><td>" + str2 + "</td><td>Менеджер</td><td>" + shipping.Name.Trim() + "</td></tr>" + "</table>";
          string str13 = str10 + "<tr><td>Адрес доставки</td><td>" + shipping.DeliveryAdress.Trim() + "</td></tr>" + "<tr><td>Дата доставки</td><td>" + shipping.DeliveryDateFrom.ToShortDateString() + "-" + shipping.DeliveryDateTo.ToShortDateString() + "</td></tr>";
          string str14 = "<tr><td>Комментарий</td><td>";
          string description1 = shipping.Description;
          string str15 = description1 != null ? description1.Trim() : (string) null;
          string str16 = "</td></tr>";
          string str17 = str13 + str14 + str15 + str16 + "<tr><td>Способ доставки</td><td>" + str1 + "</td></tr>" + "<tr><td>Транспортная компания</td><td>" + str3 + "</td></tr>" + "</table>";
          Order entity = new Order();
          entity.OrderDate = DateTime.Now;
          entity.UserId = this.HttpContext.User.Identity.Name;
          entity.OrderInfo = "";
          Order order1 = entity;
          order1.OrderInfo = order1.OrderInfo + "Город: " + shipping.City.Trim() + ";";
          Order order2 = entity;
          order2.OrderInfo = order2.OrderInfo + "Способ оплаты: " + str2 + ";";
          Order order3 = entity;
          order3.OrderInfo = order3.OrderInfo + "Менеджер: " + shipping.Name.Trim() + ";";
          Order order4 = entity;
          order4.OrderInfo = order4.OrderInfo + "Адрес доставки: " + shipping.DeliveryAdress.Trim() + ";";
          Order order5 = entity;
          order5.OrderInfo = order5.OrderInfo + "Способ доставки: " + str1 + ";";
          Order order6 = entity;
          Order order7 = order6;
          string orderInfo = order6.OrderInfo;
          string str18 = "Комментарий: ";
          string description2 = shipping.Description;
          string str19 = description2 != null ? description2.Trim() : (string) null;
          string str20 = ";";
          string str21 = orderInfo + str18 + str19 + str20;
          order7.OrderInfo = str21;
          Order order8 = entity;
          order8.OrderInfo = order8.OrderInfo + "Транспортная компания: " + str3 + ";";
          entity.Details = new List<OrderDetails>();
          stringWriter.Write(str12);
          stringWriter.Write(str8);
          foreach (CartLine line in cart.Lines)
          {
            str11 += "<tr>";
            str11 = str11 + "<td>" + line.Product.ProductCollection + "</td>";
            str11 = str11 + "<td>" + line.Product.ProductArticle + "</td>";
            str11 = str11 + "<td>" + line.Product.ProductName + "</td>";
            str11 = str11 + "<td>" + line.ProductChar.ProductCharName + "</td>";
            str11 = str11 + "<td>" + line.Shk + "</td>";
            str11 = str11 + "<td>" + line.Cutting + "</td>";
            str11 = str11 + "<td>" + (object) line.Quantity + "</td>";
            str11 += "</tr>";
            entity.Details.Add(new OrderDetails()
            {
              ProductId = line.Product.Id,
              ProductCharId = line.ProductChar.Id,
              Cutting = line.Cutting,
              Quantity = line.Quantity,
              Shk = line.Shk
            });
          }
          string str22 = str11 + "</table>";
          stringWriter.Write(str22);
          stringWriter.Write(str8);
          stringWriter.Write(str17);
          stringWriter.Write("</body></html>");
          SmtpClient smtpClient = new SmtpClient();
          NetworkCredential networkCredential = new NetworkCredential("info@vipdekor.ru", "vip123dekor123");
          MailMessage message = new MailMessage();
          MailAddress mailAddress = new MailAddress("info@vipdekor.ru");
          smtpClient.Host = "smtp.yandex.ru";
          smtpClient.Port = 587;
          smtpClient.EnableSsl = true;
          smtpClient.Credentials = (ICredentialsByHost) networkCredential;
          smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
          message.From = mailAddress;
          message.Subject = "Заказ " + Membership.Provider.GetUser(HttpContext.Current.User.Identity.Name, true).UserName;
          message.IsBodyHtml = false;
          message.Body = "order";
          message.To.Add(new MailAddress("info@vipdekor.ru"));
          Attachment attachment = new Attachment((Stream) new MemoryStream(new HtmlToPdfConverter().GeneratePdf(stringWriter.ToString())), Guid.NewGuid().ToString() + "_order.pdf", "application/pdf");
          message.Attachments.Add(attachment);
          smtpClient.Send(message);
          cart.ClearCart();
          dataContext.Orders.Add(entity);
          dataContext.SaveChanges();
        }
        catch (Exception ex)
        {
          SmtpClient smtpClient = new SmtpClient();
          NetworkCredential networkCredential = new NetworkCredential("sr@prybet.com", "+79035249738abv!");
          smtpClient.Host = "smtp.yandex.ru";
          smtpClient.Port = 587;
          smtpClient.EnableSsl = true;
          smtpClient.Credentials = (ICredentialsByHost) networkCredential;
          smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
          MailAddress mailAddress = new MailAddress("sr@prybet.com");
          MailMessage message = new MailMessage();
          message.From = mailAddress;
          message.Subject = "Error vipdekor";
          message.IsBodyHtml = false;
          message.Body = ex.Message;
          message.To.Add(new MailAddress("sr@prybet.com"));
          smtpClient.Send(message);
        }
        return this.View("Completed");
      }
      List<SelectListItem> selectListItemList1 = new List<SelectListItem>();
      selectListItemList1.Add(new SelectListItem()
      {
        Text = "",
        Value = "-1"
      });
      selectListItemList1.Add(new SelectListItem()
      {
        Text = "Курьер",
        Value = "0"
      });
      selectListItemList1.Add(new SelectListItem()
      {
        Text = "Самовывоз",
        Value = "1"
      });
      selectListItemList1.Add(new SelectListItem()
      {
        Text = "Доставка",
        Value = "2"
      });
      selectListItemList1.Add(new SelectListItem()
      {
        Text = "Экспресс-доставка",
        Value = "3"
      });
      selectListItemList1.Find((Predicate<SelectListItem>) (s => s.Value == DeliveryType)).Selected = true;
      // ISSUE: reference to a compiler-generated field
      if (CartController.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CartController.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, List<SelectListItem>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (DeliveryType), typeof (CartController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CartController.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) CartController.\u003C\u003Eo__4.\u003C\u003Ep__0, this.ViewBag, selectListItemList1);
      List<SelectListItem> selectListItemList2 = new List<SelectListItem>();
      selectListItemList2.Add(new SelectListItem()
      {
        Text = "",
        Value = "-1"
      });
      selectListItemList2.Add(new SelectListItem()
      {
        Text = "Другие способы оплаты",
        Value = "0"
      });
      selectListItemList2.Add(new SelectListItem()
      {
        Text = "Безналичная",
        Value = "1"
      });
      selectListItemList2.Find((Predicate<SelectListItem>) (s => s.Value == PayType)).Selected = true;
      // ISSUE: reference to a compiler-generated field
      if (CartController.\u003C\u003Eo__4.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CartController.\u003C\u003Eo__4.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, List<SelectListItem>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (PayType), typeof (CartController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = CartController.\u003C\u003Eo__4.\u003C\u003Ep__1.Target((CallSite) CartController.\u003C\u003Eo__4.\u003C\u003Ep__1, this.ViewBag, selectListItemList2);
      List<SelectListItem> selectListItemList3 = new List<SelectListItem>();
      IQueryable<TransportationCompanies> source = new DataContext().TransportationCompanies.Select<TransportationCompanies, TransportationCompanies>((Expression<Func<TransportationCompanies, TransportationCompanies>>) (tcom => tcom));
      Expression<Func<TransportationCompanies, string>> keySelector = (Expression<Func<TransportationCompanies, string>>) (x => x.TKName);
      foreach (TransportationCompanies transportationCompanies in (IEnumerable<TransportationCompanies>) source.OrderBy<TransportationCompanies, string>(keySelector))
        selectListItemList3.Add(new SelectListItem()
        {
          Text = transportationCompanies.TKName,
          Value = transportationCompanies.Id.ToString()
        });
      // ISSUE: reference to a compiler-generated field
      if (CartController.\u003C\u003Eo__4.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CartController.\u003C\u003Eo__4.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, List<SelectListItem>, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, nameof (TK), typeof (CartController), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = CartController.\u003C\u003Eo__4.\u003C\u003Ep__2.Target((CallSite) CartController.\u003C\u003Eo__4.\u003C\u003Ep__2, this.ViewBag, selectListItemList3);
      return this.View((object) shipping);
    }

    public RedirectResult SendOrder(Cart cart, string returnUrl)
    {
      DataTable dataTable = new DataTable("order");
      dataTable.Columns.Add("Артикул", typeof (string));
      dataTable.Columns.Add("Ткань", typeof (string));
      dataTable.Columns.Add("Цвет", typeof (string));
      dataTable.Columns.Add("Штрихкод", typeof (string));
      dataTable.Columns.Add("Метраж", typeof (double));
      foreach (CartLine line in cart.Lines)
        dataTable.Rows.Add((object) line.Product.ProductArticle, (object) line.Product.ProductName, (object) line.ProductChar.ProductCharName, (object) line.Shk, (object) line.Quantity);
      StringWriter stringWriter = new StringWriter();
      stringWriter.Write("Артикул");
      stringWriter.Write("\t");
      stringWriter.Write("Ткань");
      stringWriter.Write("\t");
      stringWriter.Write("Цвет");
      stringWriter.Write("\t");
      stringWriter.Write("Штрихкод");
      stringWriter.Write("\t");
      stringWriter.Write("Метраж");
      stringWriter.Write("\t");
      stringWriter.WriteLine();
      foreach (CartLine line in cart.Lines)
      {
        stringWriter.Write(line.Product.ProductArticle);
        stringWriter.Write("\t");
        stringWriter.Write(line.Product.ProductName);
        stringWriter.Write("\t");
        stringWriter.Write(line.ProductChar.ProductCharName);
        stringWriter.Write("\t");
        stringWriter.Write(line.Shk);
        stringWriter.Write("\t");
        stringWriter.Write(line.Quantity);
        stringWriter.Write("\t");
        stringWriter.WriteLine();
      }
      SmtpClient smtpClient = new SmtpClient();
      NetworkCredential networkCredential = new NetworkCredential("der34@mail.ru", "89168351185alena");
      MailMessage message = new MailMessage();
      MailAddress mailAddress = new MailAddress("der34@mail.ru");
      smtpClient.Host = "smtp.mail.ru";
      smtpClient.Port = 587;
      smtpClient.EnableSsl = true;
      smtpClient.Credentials = (ICredentialsByHost) networkCredential;
      smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
      message.From = mailAddress;
      message.Subject = "order";
      message.IsBodyHtml = false;
      message.Body = "order";
      message.To.Add(new MailAddress("sr@prybet.com"));
      string name = Guid.NewGuid().ToString() + "_order.xls";
      Attachment attachmentFromString = Attachment.CreateAttachmentFromString(stringWriter.ToString(), name, Encoding.GetEncoding(1251), "application/vnd.ms-excel");
      message.Attachments.Add(attachmentFromString);
      smtpClient.Send(message);
      cart.ClearCart();
      return this.Redirect(returnUrl);
    }
  }
}
