using System.ComponentModel.DataAnnotations;

namespace OnlineRemains.Models
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
