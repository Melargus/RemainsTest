﻿using System.ComponentModel.DataAnnotations;

namespace OnlineRemains.Models
{
  public class LogOnModel
  {
    [Required]
    [Display(Name = "Логин")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
  }
}
