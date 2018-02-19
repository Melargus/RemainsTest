// Decompiled with JetBrains decompiler
// Type: RemainsTest.Models.EntityModel.OrderDetails
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

namespace RemainsTest.Models.EntityModel
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
