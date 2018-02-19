// Decompiled with JetBrains decompiler
// Type: RemainsTest.Models.Product
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

using System;

namespace RemainsTest.Models
{
  [Serializable]
  public class Product
  {
    public int Id { get; set; }

    public string ProductName { get; set; }

    public string ProductArticle { get; set; }

    public string ProductCollection { get; set; }

    public int ProductGroup { get; set; }

    public double ProductPrice { get; set; }
  }
}
