
namespace OnlineRemains.Models.EntityModel
{
  public class ProductCharRemains
  {
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int ProductCharId { get; set; }

    public int StockId { get; set; }

    public string Shk { get; set; }

    public double Remains { get; set; }
  }
}
