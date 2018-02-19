// Decompiled with JetBrains decompiler
// Type: RemainsTest.Models.EntityModel.ProductChars
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

using System;

namespace RemainsTest.Models.EntityModel
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
