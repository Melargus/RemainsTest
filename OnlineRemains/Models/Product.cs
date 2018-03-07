
using System;

namespace OnlineRemains.Models
{
  [Serializable]
  public class Product
  {
    public int Id { get; set; }

    public string ProductName { get; set; }

    public string ProductArticle { get; set; }

    public string ProductCollection { get; set; }

    public int ProductGroup { get; set; }

    public double ProductPrice { get; set; }
  }
}
