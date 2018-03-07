using System.ComponentModel.DataAnnotations;

namespace OnlineRemains.Models
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
