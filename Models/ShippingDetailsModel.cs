// Decompiled with JetBrains decompiler
// Type: RemainsTest.Models.ShippingDetailsModel
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

using System;
using System.ComponentModel.DataAnnotations;

namespace RemainsTest.Models
{
  public class ShippingDetailsModel
  {
    [Required]
    [Display(Name = "Имя")]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Город")]
    public string City { get; set; }

    [Required]
    [Display(Name = "Телефон")]
    public string Telephone { get; set; }

    [Required]
    [Display(Name = "Адрес доставки")]
    public string DeliveryAdress { get; set; }

    public DateTime DeliveryDateFrom { get; set; }

    public DateTime DeliveryDateTo { get; set; }

    public string Description { get; set; }

    public ShippingDetailsModel()
    {
      this.Description = "";
    }
  }
}
