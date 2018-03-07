using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OnlineRemains.Models.EntityModel
{
  [Serializable]
  public class Cart
  {
    private List<CartLine> _cartLines = new List<CartLine>();

    public void AddItem(Product p, ProductChars pchar, string shk, double q, string cutting, double price)
    {
      this._cartLines.Add(new CartLine()
      {
        Product = p,
        ProductChar = pchar,
        Shk = shk,
        Quantity = q,
        Price = price,
        Cutting = cutting,
        Id = Guid.NewGuid()
      });
    }

    public void AddItem(ProductChars pchar, string shk, double q, string cutting)
    {
      Product product;
      using (DataContext dataContext = new DataContext())
        product = dataContext.Products.Where<Product>((Expression<Func<Product, bool>>) (pr => pr.Id == pchar.ProductId)).FirstOrDefault<Product>();
      this._cartLines.Add(new CartLine()
      {
        Product = product,
        ProductChar = pchar,
        Shk = shk,
        Quantity = q,
        Cutting = cutting
      });
    }

    public void AddItem(ProductChars pchar, double q, string cutting)
    {
      Product product;
      using (DataContext dataContext = new DataContext())
        product = dataContext.Products.Where<Product>((Expression<Func<Product, bool>>) (pr => pr.Id == pchar.ProductId)).FirstOrDefault<Product>();
      this._cartLines.Add(new CartLine()
      {
        Product = product,
        ProductChar = pchar,
        Quantity = q,
        Cutting = cutting
      });
    }

    public void ClearCart()
    {
      this._cartLines.Clear();
    }

    public void RemoveItem(Guid line)
    {
      this._cartLines.RemoveAt(this._cartLines.FindIndex((Predicate<CartLine>) (x => x.Id == line)));
    }

    public List<CartLine> Lines
    {
      get
      {
        return this._cartLines;
      }
    }
  }
}
