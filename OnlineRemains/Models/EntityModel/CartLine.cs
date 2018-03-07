using System;

namespace OnlineRemains.Models.EntityModel
{
  [Serializable]
  public class CartLine
  {
    public Guid Id { get; set; }

    public Product Product { get; set; }

    public ProductChars ProductChar { get; set; }

    public string Shk { get; set; }

    public string Cutting { get; set; }

    public double Quantity { get; set; }

    public double Price { get; set; }

    public override int GetHashCode()
    {
      return this.Shk.GetHashCode() + this.Cutting.GetHashCode() + this.Quantity.GetHashCode() + this.Price.GetHashCode() + this.Product.GetHashCode() + this.ProductChar.GetHashCode();
    }
  }
}
