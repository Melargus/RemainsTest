using System.Data.Entity;

namespace OnlineRemains.Models.EntityModel
{
  public class DataContext : DbContext
  {
    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<TransportationCompanies> TransportationCompanies { get; set; }

    public DbSet<ProductChars> ProductChars { get; set; }

    public DbSet<Stock> Stocks { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductPrice> ProductPrice { get; set; }

    public DbSet<ProductCharRemains> ProductCharRemains { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderDetails> OrderDetails { get; set; }

    public DataContext()
      : base(nameof (DataContext))
    {
    }
  }
}
