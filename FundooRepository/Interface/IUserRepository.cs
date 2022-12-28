
using FundooModel;

namespace FundooRepository.Interface
{
    public interface IUserRepository
    {
        public UserRegistrationModel Registration(UserRegistrationModel userRegistration);
        public string Login(UserLoginModel userLogin);
        public string ForgotPassword(string emailID);
        public bool ResetPassword(UserResetPasswordModel userResetPassword, string emailID);
    }
}
