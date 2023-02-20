// <copyright file="IUserRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
        /// <param name="userID">The user identifier.</param>
        /// <returns>string.</returns>
        public string GenerateJWTToken(string emailID, int userID);

        /// <summary>
        /// User Registration.
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
        /// <param name="userID">userID.</param>
        /// <returns>UserRegistrationModel.</returns>
        public UserRegistrationModel GetUser(int userID);
    }
}
