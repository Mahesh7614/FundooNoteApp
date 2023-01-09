using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;

namespace FundooManager.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;

        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public UserRegistrationModel Registration(UserRegistrationModel userRegistration)
        {
            try
            {
                return this.userRepository.Registration(userRegistration);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public UserRegistrationModel Login(UserLoginModel userLogin)
        {
            try
            {
                return this.userRepository.Login(userLogin);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string ForgotPassword(string emailID)
        {
            try
            {
                return this.userRepository.ForgotPassword(emailID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ResetPassword(UserResetPasswordModel userResetPassword, string emailID)
        {
            try
            {
                return this.userRepository.ResetPassword(userResetPassword, emailID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public UserRegistrationModel GetUser(int UserID)
        {
            try
            {
                return this.userRepository.GetUser(UserID);
            }
            catch(Exception ex)
            { 
                throw new Exception(ex.Message);
            }
        }
        public string GenerateJWTToken(string emailID, int UserID)
        {
            try
            {
                return this.userRepository.GenerateJWTToken(emailID, UserID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
