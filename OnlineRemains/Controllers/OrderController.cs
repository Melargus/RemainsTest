using OnlineRemains.Models;
using OnlineRemains.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace OnlineRemains.Controllers
{
  public class OrderController : Controller
  {
    [Authorize]
    public ActionResult OrderHistory()
    {
      string curruser = this.HttpContext.User.Identity.Name;
      IQueryable<Order> queryable = new DataContext().Orders.Where<Order>((Expression<Func<Order, bool>>) (o => o.UserId == curruser));
      List<OrderViewModel> orderViewModelList = new List<OrderViewModel>();
      foreach (Order order1 in (IEnumerable<Order>) queryable)
      {
        Order order = order1;
        OrderViewModel orderViewModel = new OrderViewModel()
        {
          Id = order.OrderId,
          OrderDate = order.OrderDate,
          OrderInfo = order.OrderInfo
        };
        orderViewModel.Detail = new List<OrderDetailView>();
        DbSet<OrderDetails> orderDetails1 = new DataContext().OrderDetails;
        Expression<Func<OrderDetails, bool>> predicate = (Expression<Func<OrderDetails, bool>>) (o => o.OrderId == order.OrderId);
        foreach (OrderDetails orderDetails2 in (IEnumerable<OrderDetails>) orderDetails1.Where<OrderDetails>(predicate))
        {
          OrderDetails detail = orderDetails2;
          Product product = new DataContext().Products.Where<Product>((Expression<Func<Product, bool>>) (pr => pr.Id == detail.ProductId)).FirstOrDefault<Product>();
          OrderDetailView orderDetailView = new OrderDetailView();
          orderDetailView.Product = product.ProductName;
          orderDetailView.Cutting = detail.Cutting;
          orderDetailView.Quantity = detail.Quantity;
          orderDetailView.Shk = detail.Shk;
          ProductChars productChars = new DataContext().ProductChars.Where<ProductChars>((Expression<Func<ProductChars, bool>>) (pr => pr.Id == detail.ProductCharId)).FirstOrDefault<ProductChars>();
          orderDetailView.ProductChar = productChars.ProductCharName;
          orderViewModel.Detail.Add(orderDetailView);
        }
        orderViewModelList.Add(orderViewModel);
      }
      return (ActionResult) this.View((object) orderViewModelList);
    }
  }
}
