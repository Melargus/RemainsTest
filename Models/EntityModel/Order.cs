// Decompiled with JetBrains decompiler
// Type: RemainsTest.Models.EntityModel.Order
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

using System;
using System.Collections.Generic;

namespace RemainsTest.Models.EntityModel
{
  public class Order
  {
    public int OrderId { get; set; }

    public string UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public string OrderInfo { get; set; }

    public List<OrderDetails> Details { get; set; }
  }
}
