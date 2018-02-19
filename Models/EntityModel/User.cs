// Decompiled with JetBrains decompiler
// Type: RemainsTest.Models.EntityModel.User
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

using System;

namespace RemainsTest.Models.EntityModel
{
  public class User
  {
    public Guid Id { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string CompanyName { get; set; }

    public string CompanyId { get; set; }

    public int? RoleId { get; set; }

    public Role Role { get; set; }
  }
}
