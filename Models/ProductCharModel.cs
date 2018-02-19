// Decompiled with JetBrains decompiler
// Type: RemainsTest.Models.ProductCharModel
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

using RemainsTest.Models.EntityModel;
using System.Collections.Generic;

namespace RemainsTest.Models
{
  public class ProductCharModel
  {
    public ProductChars Characteristic { get; set; }

    public double TotalRemains { get; set; }

    public List<StockModel> StockList { get; set; }
  }
}
