using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Security;
using NLog;
using OnlineRemains.Models.EntityModel;

namespace OnlineRemains.Providers
{
  public class SimpleMembership : MembershipProvider
  {
    private static Logger logger = LogManager.GetCurrentClassLogger();

    public override bool ValidateUser(string username, string password)
    {
      bool flag = false;
      using (DataContext dataContext = new DataContext())
      {
        try
        {
          User user = dataContext.Users.FirstOrDefault(u => u.Login == username);
          if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
            flag = true;
        }
        catch (Exception ex)
        {
          logger.Error(ex.Message);
          flag = false;
        }
      }
      return flag;
    }

    public MembershipUser CreateUser(string username, string password, string company)
    {
      User entity = null;
      using (DataContext dataContext = new DataContext())
      {
        entity = new User
        {
          CompanyName = company,
          Login = username,
          Email = username,
          Password = Crypto.HashPassword(password),
          RoleId = 2
        };
        dataContext.Users.Add(entity);
        dataContext.SaveChanges();
      }
      
      return new MembershipUser("SimpleProvider", entity.CompanyName, null, entity.Login, null, null, false, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
    }

    public override MembershipUser GetUser(string login, bool userIsOnline = true)
    {
      try
      {
        using (DataContext dataContext = new DataContext())
        {
          IQueryable<User> source = dataContext.Users.Where(u => u.Login == login);
          if (source.Any())
            return new MembershipUser("SimpleProvider", source.First().CompanyName, null, null, null, null, false, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
        }
      }
      catch (Exception ex)
      {
        return null;
      }
      return null;
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

    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
      bool flag = false;
      using (DataContext dataContext = new DataContext())
      {
        User entity = dataContext.Users.FirstOrDefault(u => u.Login == username);
        if (entity != null && Crypto.VerifyHashedPassword(entity.Password, oldPassword))
        {
          entity.Password = Crypto.HashPassword(newPassword);
          dataContext.Users.Attach(entity);
          dataContext.Entry(entity).Property(e => e.Password).IsModified = true;
          dataContext.SaveChanges();
          flag = true;
        }
      }
      return flag;
    }

    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
    {
      throw new NotImplementedException();
    }

    public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
    {
      status = MembershipCreateStatus.Success;
      return CreateUser(username, password, passwordQuestion);
    }

    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
      throw new NotImplementedException();
    }

    public override bool EnablePasswordReset
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public override bool EnablePasswordRetrieval
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotImplementedException();
    }

    public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotImplementedException();
    }

    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
    {
      throw new NotImplementedException();
    }

    public override int GetNumberOfUsersOnline()
    {
      throw new NotImplementedException();
    }

    public override string GetPassword(string username, string answer)
    {
      throw new NotImplementedException();
    }

    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
      throw new NotImplementedException();
    }

    public override string GetUserNameByEmail(string email)
    {
      throw new NotImplementedException();
    }

    public override int MaxInvalidPasswordAttempts
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public override int MinRequiredNonAlphanumericCharacters
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public override int MinRequiredPasswordLength
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public override int PasswordAttemptWindow
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public override MembershipPasswordFormat PasswordFormat
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public override string PasswordStrengthRegularExpression
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public override bool RequiresQuestionAndAnswer
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public override bool RequiresUniqueEmail
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public override string ResetPassword(string username, string answer)
    {
      throw new NotImplementedException();
    }

    public override bool UnlockUser(string userName)
    {
      throw new NotImplementedException();
    }

    public override void UpdateUser(MembershipUser user)
    {
      throw new NotImplementedException();
    }
  }
}
