// Decompiled with JetBrains decompiler
// Type: RemainsTest.Models.ProductModel
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

using System;
using System.Collections.Generic;

namespace RemainsTest.Models
{
  public class ProductModel
  {
    public List<Tuple<Stock, double>> Remains = new List<Tuple<Stock, double>>();

    public int Id { get; set; }

    public string ProductName { get; set; }

    public string ProductArticle { get; set; }

    public string ProductCollection { get; set; }

    public int ProductGroup { get; set; }
  }
}
