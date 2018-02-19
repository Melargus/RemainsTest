// Decompiled with JetBrains decompiler
// Type: RemainsTest.Providers.CustomRoleProvider
// Assembly: RemainsTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EDE8023D-7279-40AF-8338-41C7B4F89E43
// Assembly location: D:\GameDevProjectFile\Архив\mskvipdekor.ru\bin\RemainsTest.dll

using RemainsTest.Models.EntityModel;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Security;

namespace RemainsTest.Providers
{
  public class CustomRoleProvider : RoleProvider
  {
    public override string[] GetRolesForUser(string username)
    {
      string[] strArray = new string[0];
      using (DataContext dataContext = new DataContext())
      {
        User user = dataContext.Users.FirstOrDefault<User>((Expression<Func<User, bool>>) (u => u.Login == username));
        if (user != null)
        {
          Role role = dataContext.Roles.Find((object) user.RoleId);
          if (role != null)
            strArray = new string[1]{ role.Name };
        }
      }
      return strArray;
    }

    public override void CreateRole(string roleName)
    {
      Role role = new Role() { Name = roleName };
      new DataContext() { Roles = { role } }.SaveChanges();
    }

    public override bool IsUserInRole(string username, string roleName)
    {
      bool flag = false;
      using (DataContext dataContext = new DataContext())
      {
        User user = dataContext.Users.FirstOrDefault<User>((Expression<Func<User, bool>>) (u => u.Login == username));
        if (user != null)
        {
          Role role = dataContext.Roles.Find((object) user.RoleId);
          if (role != null && role.Name == roleName)
            flag = true;
        }
      }
      return flag;
    }

    public override void AddUsersToRoles(string[] usernames, string[] roleNames)
    {
      throw new NotImplementedException();
    }

    public override string ApplicationName
    {
      get
      {
        throw new NotImplementedException();
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
    {
      throw new NotImplementedException();
    }

    public override string[] FindUsersInRole(string roleName, string usernameToMatch)
    {
      throw new NotImplementedException();
    }

    public override string[] GetAllRoles()
    {
      throw new NotImplementedException();
    }

    public override string[] GetUsersInRole(string roleName)
    {
      throw new NotImplementedException();
    }

    public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
    {
      throw new NotImplementedException();
    }

    public override bool RoleExists(string roleName)
    {
      throw new NotImplementedException();
    }
  }
}
