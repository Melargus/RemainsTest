using System;
using System.Collections.Generic;

namespace OnlineRemains.Models.EntityModel
{
  public class Order
  {
    public int OrderId { get; set; }

    public string UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public string OrderInfo { get; set; }

    public List<OrderDetails> Details { get; set; }
  }
}
