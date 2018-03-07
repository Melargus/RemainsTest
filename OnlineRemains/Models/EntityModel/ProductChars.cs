using System;

namespace OnlineRemains.Models.EntityModel
{
  [Serializable]
  public class ProductChars
  {
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string ProductCharName { get; set; }

    public string ProductCharId { get; set; }
  }
}
