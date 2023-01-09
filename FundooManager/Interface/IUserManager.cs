
using FundooModel;

namespace FundooManager.Interface
{
    public interface IUserManager
    {
        public string GenerateJWTToken(string emailID, int UserID);
        public UserRegistrationModel Registration(UserRegistrationModel userRegistration);
        public UserRegistrationModel Login(UserLoginModel userLogin);
        public string ForgotPassword(string emailID);
        public bool ResetPassword(UserResetPasswordModel userResetPassword, string emailID);
        public UserRegistrationModel GetUser(int UserID);

    }
}
