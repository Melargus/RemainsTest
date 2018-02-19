// Decompiled with JetBrains decompiler
// Type: RemainsTest.Models.ChangePasswordModel
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

using System.ComponentModel.DataAnnotations;

namespace RemainsTest.Models
{
  public class ChangePasswordModel
  {
    [Required]
    [Display(Name = "Старый пароль")]
    public string OldPassword { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Новый пароль")]
    public string NewPassword { get; set; }
  }
}
