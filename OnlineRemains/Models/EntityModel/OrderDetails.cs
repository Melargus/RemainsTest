
namespace OnlineRemains.Models.EntityModel
{
  public class OrderDetails
  {
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int ProductCharId { get; set; }

    public string Shk { get; set; }

    public string Cutting { get; set; }

    public double Quantity { get; set; }
  }
}
