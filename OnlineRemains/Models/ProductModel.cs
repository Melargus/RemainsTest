using System;
using System.Collections.Generic;

namespace OnlineRemains.Models
{
  public class ProductModel
  {
    public List<Tuple<Stock, double>> Remains = new List<Tuple<Stock, double>>();

    public int Id { get; set; }

    public string ProductName { get; set; }

    public string ProductArticle { get; set; }

    public string ProductCollection { get; set; }

    public int ProductGroup { get; set; }
  }
}
