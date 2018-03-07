using OnlineRemains.Models.EntityModel;
using System.Collections.Generic;

namespace OnlineRemains.Models
{
  public class ProductCharModel
  {
    public ProductChars Characteristic { get; set; }

    public double TotalRemains { get; set; }

    public List<StockModel> StockList { get; set; }
  }
}
