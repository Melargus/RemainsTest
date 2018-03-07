using OnlineRemains.Models;
using OnlineRemains.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace RemainsTest.Controllers
{
    [Authorize]
    public class CartController : Controller
    {

        public ActionResult Index(Cart cart)
        {
            return View(cart);
        }

        public RedirectResult AddToCart(Cart cart, int productId, int charId, string shk, string quantity, string cutting, string returnUrl)
        {
            Product p;
            ProductChars pc;
            var dc = new DataContext();

            if ((productId == 0 || charId == 0) && shk.Length > 0)
            {
                var t = (from pcr in dc.ProductCharRemains
                    where pcr.Shk == shk
                    select pcr).FirstOrDefault();

                productId = t.ProductId;
                charId = t.ProductCharId;
            }

            p = (from pr in dc.Products
                where pr.Id == productId
                select pr).FirstOrDefault();
            pc = (from pch in dc.ProductChars
                where pch.Id == charId
                select pch).FirstOrDefault();
            var pp = (from pr in dc.ProductPrice
                where pr.ProductId == productId
                select pr).SingleOrDefault();
            var price = pp?.Price ?? 0;

            cart.AddItem(p, pc, shk, double.Parse(quantity.Replace(".",",")), cutting, price);
            return Redirect(returnUrl);
        }

        public RedirectResult RemoveFromCart(Cart cart, Guid line, string returnUrl)
        {
            cart.RemoveItem(line);
            return Redirect(returnUrl);
        }

        public ViewResult Checkout()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "", Value = "-1", Selected = true });
            items.Add(new SelectListItem { Text = "Курьер", Value = "0" });
            items.Add(new SelectListItem { Text = "Самовывоз", Value = "1" });
            items.Add(new SelectListItem { Text = "Доставка", Value = "2"});
            items.Add(new SelectListItem { Text = "Экспресс-доставка", Value = "3" });
            ViewBag.DeliveryType = items;

            items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "", Value = "-1", Selected = true });
            items.Add(new SelectListItem { Text = "Другие способы оплаты", Value = "0"});
            items.Add(new SelectListItem { Text = "Безналичная", Value = "1" });
            ViewBag.PayType = items;

            items = new List<SelectListItem>();
            var dc = new DataContext();
            var tk = (from tcom in dc.TransportationCompanies
                select tcom);
            foreach (var comp in tk)
            {
                items.Add(new SelectListItem { Text =  comp.TKName, Value = comp.Id.ToString()});
            }
            ViewBag.TK = items;

            return View(new ShippingDetailsModel());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetailsModel shipping, string DeliveryType, string PayType, string TK)
        {
            string dt = "", pt = "", comp = "";

            switch (DeliveryType)
            {
                case "0":
                    dt = "Курьер";
                    break;
                case "1":
                    dt = "Самовывоз";
                    break;
                case "2":
                    dt = "Доставка";
                    break;
                case "3":
                    dt = "Экспресс-доставка";
                    break;
            }

            switch (PayType)
            {
                case "0":
                    pt = "Другие способы оплаты";
                    break;
                case "1":
                    pt = "Безналичная";
                    break;
            }

            if (DeliveryType == "2" || DeliveryType == "3")
            {
                var dc = new DataContext();
                var itk = int.Parse(TK);
                var tk = (from tcom in dc.TransportationCompanies
                    where tcom.Id == itk
                    select tcom).SingleOrDefault();
                comp = tk?.TKName;
            }

            if (DeliveryType == "-1")
            {
                ModelState.AddModelError("Delivery","Заполните поле Способ доставки.");
            }

            if (PayType == "-1")
            {
                ModelState.AddModelError("Pay", "Заполните поле Способ оплаты.");
            }

            if (ModelState.IsValid)
            {
                var dc = new DataContext();
                try
                {
                    StringWriter sw = new StringWriter();

                    string start = @"<html><head><meta charset=""utf-8""><meta name = ""viewport"" content = ""width=device-width, initial-scale=1""></head><body face=""Arial"">";
                    string img = "<img src=\"" + Server.MapPath("/") + "/Content/img/art.png\">";

                    sw.Write(start);
                    sw.Write(img);
                    sw.Write("<br>");

                    string empty = "<br>";
                    string header = "<div><table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border - collapse:collapse; \">";
                    string footer = "<div><table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border - collapse:collapse; \">";
                    string main = "<div><table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border - collapse:collapse; \"><tr><th scope=\"col\">Коллекция</th><th scope=\"col\">Артикул</th><th scope=\"col\">Ткань</th><th scope=\"col\">Цвет</th><th scope=\"col\">Штрихкод</th><th scope=\"col\">Разбивка</th><th scope=\"col\">Метраж</th></tr>";

                    header += "<tr><td>Клиент</td><td>" +
                              Membership.Provider.GetUser(System.Web.HttpContext.Current.User.Identity.Name, true).UserName +
                              "</td><td>Дата заказа</td><td>" + DateTime.Now.ToShortDateString() + "</td></tr>";
                    header += "<tr><td>Город</td><td>" + shipping.City.Trim() + "</td><td>Телефон</td><td>" +
                              shipping.Telephone.Trim() + "</td></tr>";
                    header += "<tr><td>Способ оплаты</td><td>" + pt + "</td><td>Менеджер</td><td>" + shipping.Name.Trim() +
                              "</td></tr>";
                    header += "</table>";

                    footer += "<tr><td>Адрес доставки</td><td>" + shipping.DeliveryAdress.Trim() + "</td></tr>";
                    footer += "<tr><td>Дата доставки</td><td>" + shipping.DeliveryDateFrom.ToShortDateString() + "-" + shipping.DeliveryDateTo.ToShortDateString() + "</td></tr>";
                    footer += "<tr><td>Комментарий</td><td>" + (shipping.Description?.Trim() ?? "") + "</td></tr>";
                    footer += "<tr><td>Способ доставки</td><td>" + dt + "</td></tr>";
                    footer += "<tr><td>Транспортная компания</td><td>" + comp + "</td></tr>";
                    footer += "</table>";

                    var order = new Order();
                    order.OrderDate = DateTime.Now;
                    order.UserId = HttpContext.User.Identity.Name;
                    order.OrderInfo = "";
                    order.OrderInfo += "Город: " + shipping.City.Trim() + ";";
                    order.OrderInfo += "Способ оплаты: " + pt + ";";
                    order.OrderInfo += "Менеджер: " + shipping.Name.Trim() + ";";
                    order.OrderInfo += "Адрес доставки: " + shipping.DeliveryAdress.Trim() + ";";
                    order.OrderInfo += "Способ доставки: " + dt + ";";
                    order.OrderInfo += "Комментарий: " + (shipping.Description?.Trim() ?? "") + ";";
                    order.OrderInfo += "Транспортная компания: " + comp + ";";

                    order.Details = new List<OrderDetails>();

                    sw.Write(header);
                    sw.Write(empty);

                    foreach (var line in cart.Lines)
                    {
                        main += "<tr>";
                        main += "<td>" + line.Product.ProductCollection + "</td>";
                        main += "<td>" + line.Product.ProductArticle + "</td>";
                        main += "<td>" + line.Product.ProductName + "</td>";
                        main += "<td>" + line.ProductChar.ProductCharName + "</td>";
                        main += "<td>" + line.Shk + "</td>";
                        main += "<td>" + line.Cutting + "</td>";
                        main += "<td>" + line.Quantity + "</td>";
                        main += "</tr>";

                        var od = new OrderDetails();
                        od.ProductId = line.Product.Id;
                        od.ProductCharId = line.ProductChar.Id;
                        od.Cutting = line.Cutting;
                        od.Quantity = line.Quantity;
                        od.Shk = line.Shk;

                        order.Details.Add(od);
                    }
                    main += "</table>";
                    sw.Write(main);
                    sw.Write(empty);
                    sw.Write(footer);
                    sw.Write("</body></html>");

                    //var r = new HtmlViewRenderer();
                    //var test = r.RenderViewToString(this, "Checkout", shipping);

                    SmtpClient smtpClient = new SmtpClient();
                    NetworkCredential basicCredential = new NetworkCredential("info@vipdekor.ru", "sd187jnw65");
                    //NetworkCredential basicCredential = new NetworkCredential("sr@prybet.com", "+79035249738abv!");
                    MailMessage message = new MailMessage();
                    MailAddress fromAddress = new MailAddress("info@vipdekor.ru");
                    //MailAddress fromAddress = new MailAddress("sr@prybet.com");

                    smtpClient.Host = "smtp.yandex.ru";
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = basicCredential;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    message.From = fromAddress;
                    message.Subject = "Заказ " + Membership.Provider.GetUser(System.Web.HttpContext.Current.User.Identity.Name, true).UserName;
                    message.IsBodyHtml = false;
                    message.Body = "order";
                    message.To.Add(new MailAddress("info@vipdekor.ru"));
                    //message.To.Add(new MailAddress("sr@prybet.com"));

                    //byte[] bytes = new byte[sw.ToString().Length * sizeof(char)];
                    //Buffer.BlockCopy(sw.ToString().ToCharArray(), 0, bytes, 0, bytes.Length);


                    var stream = new MemoryStream((new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(sw.ToString()));

                    var fn = Guid.NewGuid() + "_order.pdf";
                    //var attachment = Attachment.CreateAttachmentFromString(sw.ToString(), fn, Encoding.GetEncoding(1251), "application/vnd.ms-excel");
                    var attachment = new Attachment(stream, fn, "application/pdf");
                    message.Attachments.Add(attachment);
                    smtpClient.Send(message);
                    cart.ClearCart();

                    dc.Orders.Add(order);
                    dc.SaveChanges();

                    //foreach (OrderDetails t in order.Details)
                    //{
                    //    t.OrderId = order.OrderId;
                    //}

                    //dc.OrderDetails.AddRange(order.Details);
                    //dc.SaveChanges();
                }
                catch (Exception ex)
                {
                    SmtpClient errClient = new SmtpClient();
                    NetworkCredential err = new NetworkCredential("sr@prybet.com", "+79035249738abv!");
                    errClient.Host = "smtp.yandex.ru";
                    errClient.Port = 587;
                    errClient.EnableSsl = true;
                    errClient.Credentials = err;
                    errClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    MailAddress fromAddress = new MailAddress("sr@prybet.com");

                    MailMessage errMessage = new MailMessage();
                    errMessage.From = fromAddress;
                    errMessage.Subject = "Error vipdekor";
                    errMessage.IsBodyHtml = false;
                    errMessage.Body = ex.Message;
                    errMessage.To.Add(new MailAddress("sr@prybet.com"));
                    errClient.Send(errMessage);
                }
                

                //stream.Dispose();
                //sw.Dispose();

                return View("Completed");
            }
            else
            {
                List<SelectListItem> items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "", Value = "-1"});
                items.Add(new SelectListItem { Text = "Курьер", Value = "0"});
                items.Add(new SelectListItem { Text = "Самовывоз", Value = "1" });
                items.Add(new SelectListItem { Text = "Доставка", Value = "2" });
                items.Add(new SelectListItem { Text = "Экспресс-доставка", Value = "3" });
                items.Find(s => s.Value == DeliveryType).Selected = true;
                ViewBag.DeliveryType = items;

                items = new List<SelectListItem>();
                items.Add(new SelectListItem { Text = "", Value = "-1"});
                items.Add(new SelectListItem { Text = "Другие способы оплаты", Value = "0"});
                items.Add(new SelectListItem { Text = "Безналичная", Value = "1" });
                items.Find(s => s.Value == PayType).Selected = true;
                ViewBag.PayType = items;

                items = new List<SelectListItem>();
                var dc = new DataContext();
                var tk = (from tcom in dc.TransportationCompanies
                          select tcom).OrderBy(x=>x.TKName);
                foreach (var compp in tk)
                {
                    items.Add(new SelectListItem { Text = compp.TKName, Value = compp.Id.ToString() });
                }
                ViewBag.TK = items;

                return View(shipping);
            }
        }

        public RedirectResult SendOrder(Cart cart, string returnUrl)
        {
            var order = new DataTable("order");
            order.Columns.Add("Артикул", typeof(string));
            order.Columns.Add("Ткань", typeof(string));
            order.Columns.Add("Цвет", typeof(string));
            order.Columns.Add("Штрихкод", typeof(string));
            order.Columns.Add("Метраж", typeof(double));

            foreach (var line in cart.Lines)
            {
                order.Rows.Add(line.Product.ProductArticle, line.Product.ProductName, line.ProductChar.ProductCharName,
                    line.Shk, line.Quantity);
            }

            //var grid = new GridView();
            //grid.DataSource = order;
            //grid.DataBind();

            StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);

            //grid.RenderControl(htw);

            sw.Write("Артикул");
            sw.Write("\t");
            sw.Write("Ткань");
            sw.Write("\t");
            sw.Write("Цвет");
            sw.Write("\t");
            sw.Write("Штрихкод");
            sw.Write("\t");
            sw.Write("Метраж");
            sw.Write("\t");
            sw.WriteLine();

            foreach (var line in cart.Lines)
            {
                sw.Write(line.Product.ProductArticle);
                sw.Write("\t");
                sw.Write(line.Product.ProductName);
                sw.Write("\t");
                sw.Write(line.ProductChar.ProductCharName);
                sw.Write("\t");
                sw.Write(line.Shk);
                sw.Write("\t");
                sw.Write(line.Quantity);
                sw.Write("\t");
                sw.WriteLine();
            }

            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential = new NetworkCredential("der34@mail.ru", "89168351185alena");
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("der34@mail.ru");

            smtpClient.Host = "smtp.mail.ru";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = basicCredential;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            message.From = fromAddress;
            message.Subject = "order";
            message.IsBodyHtml = false;
            message.Body = "order";
            message.To.Add(new MailAddress("sr@prybet.com"));

            //byte[] bytes = new byte[sw.ToString().Length * sizeof(char)];
            //Buffer.BlockCopy(sw.ToString().ToCharArray(), 0, bytes, 0, bytes.Length);

            var fn = Guid.NewGuid() + "_order.xls";
            var attachment = Attachment.CreateAttachmentFromString(sw.ToString(), fn, Encoding.GetEncoding(1251), "application/vnd.ms-excel");
            message.Attachments.Add(attachment);

            smtpClient.Send(message);
            cart.ClearCart();

            return Redirect(returnUrl);
        }
    }
}
