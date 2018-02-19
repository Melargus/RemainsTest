// Decompiled with JetBrains decompiler
// Type: RemainsTest.Models.EntityModel.DataContext
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

using System.Data.Entity;

namespace RemainsTest.Models.EntityModel
{
  public class DataContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<RemainsTest.Models.EntityModel.TransportationCompanies> TransportationCompanies { get; set; }

    public DbSet<RemainsTest.Models.EntityModel.ProductChars> ProductChars { get; set; }

    public DbSet<Stock> Stocks { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<RemainsTest.Models.EntityModel.ProductPrice> ProductPrice { get; set; }

    public DbSet<RemainsTest.Models.EntityModel.ProductCharRemains> ProductCharRemains { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<RemainsTest.Models.EntityModel.OrderDetails> OrderDetails { get; set; }

    public DataContext()
      : base(nameof (DataContext))
    {
    }
  }
}
