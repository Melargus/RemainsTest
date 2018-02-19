// Decompiled with JetBrains decompiler
// Type: RemainsTest.Models.EntityModel.CartLine
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

using System;

namespace RemainsTest.Models.EntityModel
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
