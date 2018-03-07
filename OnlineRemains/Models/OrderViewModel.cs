
using System;
using System.Collections.Generic;

namespace OnlineRemains.Models
{
  public class OrderViewModel
  {
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public string OrderInfo { get; set; }

    public List<OrderDetailView> Detail { get; set; }
  }
}
