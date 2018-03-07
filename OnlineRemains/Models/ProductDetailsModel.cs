using System.Collections.Generic;

namespace OnlineRemains.Models
{
  public class ProductDetailsModel
  {
    public Product Parent { get; set; }

    public double Price { get; set; }

    public List<ProductCharModel> ProductCharsList { get; set; }
  }
}
