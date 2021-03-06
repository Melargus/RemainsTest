﻿using System;

namespace OnlineRemains.Models.EntityModel
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
