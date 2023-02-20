// <copyright file="IUserManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooManager.Interface
{
    using FundooModel;

    /// <summary>
    /// IUserManager interface.
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Generate JWT Token.
        /// </summary>
        /// <param name="emailID">emailID.</param>
        /// <param name="userID">UserID.</param>
        /// <returns>string.</returns>
        public string GenerateJWTToken(string emailID, int userID);

        /// <summary>
        /// Registration.
        /// </summary>
        /// <param name="userRegistration">userRegistration.</param>
        /// <returns>UserRegistrationModel.</returns>
        public UserRegistrationModel Registration(UserRegistrationModel userRegistration);

        /// <summary>
        /// User Login.
        /// </summary>
        /// <param name="userLogin">userLogin.</param>
        /// <returns>UserRegistrationModel.</returns>
        public UserRegistrationModel Login(UserLoginModel userLogin);

        /// <summary>
        /// CreateTicketForPassword.
        /// </summary>
        /// <param name="emailID">emailID.</param>
        /// <param name="token">token.</param>
        /// <returns>UserTicket.</returns>
        public UserTicket CreateTicketForPassword(string emailID, string token);

        /// <summary>
        /// ForgotPassword.
        /// </summary>
        /// <param name="emailID">emailID.</param>
        /// <returns>string.</returns>
        public string ForgotPassword(string emailID);

        /// <summary>
        /// ResetPassword.
        /// </summary>
        /// <param name="userResetPassword">userResetPassword.</param>
        /// <param name="emailID">emailID.</param>
        /// <returns>bool.</returns>
        public bool ResetPassword(UserResetPasswordModel userResetPassword, string emailID);

        /// <summary>
        /// GetUser.
        /// </summary>
        /// <param name="userID">UserID.</param>
        /// <returns>UserRegistrationModel.</returns>
        public UserRegistrationModel GetUser(int userID);
    }
}
