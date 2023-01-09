
namespace FundooRepository.Interface
{
    using FundooModel;

    /// <summary>
    /// User Interface.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Generates the JWT token.
        /// </summary>
        /// <param name="emailID">The email identifier.</param>
        /// <param name="UserID">The user identifier.</param>
        /// <returns>string.</returns>
        public string GenerateJWTToken(string emailID, int UserID);

        public UserRegistrationModel Registration(UserRegistrationModel userRegistration);

        public UserRegistrationModel Login(UserLoginModel userLogin);

        public string ForgotPassword(string emailID);

        public bool ResetPassword(UserResetPasswordModel userResetPassword, string emailID);

        public UserRegistrationModel GetUser(int UserID);
    }
}
