// Decompiled with JetBrains decompiler
// Type: RemainsTest.Models.UserCreateModel
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

using System.ComponentModel.DataAnnotations;

namespace RemainsTest.Models
{
  public class UserCreateModel
  {
    [Required]
    public string Company { get; set; }

    [Required]
    public string Login { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string PasswordRepeat { get; set; }
  }
}
