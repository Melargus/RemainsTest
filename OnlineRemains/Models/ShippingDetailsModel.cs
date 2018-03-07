using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineRemains.Models
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
