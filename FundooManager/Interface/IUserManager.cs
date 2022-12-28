
using FundooModel;

namespace FundooManager.Interface
{
    public interface IUserManager
    {
        public UserRegistrationModel Registration(UserRegistrationModel userRegistration);
        public string Login(UserLoginModel userLogin);
        public string ForgotPassword(string emailID);
        public bool ResetPassword(UserResetPasswordModel userResetPassword, string emailID);

    }
}
