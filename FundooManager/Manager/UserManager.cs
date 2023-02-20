// <copyright file="UserManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooManager.Manager
{
    using FundooManager.Interface;
    using FundooModel;
    using FundooRepository.Interface;

    /// <summary>
    /// UserManager.
    /// </summary>
    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class.
        /// </summary>
        /// <param name="userRepository">userRepository.</param>
        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Registration.
        /// </summary>
        /// <param name="userRegistration">userRegistration.</param>
        /// <returns>UserRegistrationModel.</returns>
        /// <exception cref="Exception">Exception.</exception>
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

        /// <summary>
        /// User Login.
        /// </summary>
        /// <param name="userLogin">userLogin.</param>
        /// <returns>UserRegistrationModel.</returns>
        /// <exception cref="Exception">Exception.</exception>
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

        /// <summary>
        /// CreateTicketForPassword.
        /// </summary>
        /// <param name="emailID">emailID.</param>
        /// <param name="token">token.</param>
        /// <returns>UserTicket.</returns>
        public UserTicket CreateTicketForPassword(string emailID, string token)
        {
            try
            {
                return this.userRepository.CreateTicketForPassword(emailID, token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ForgotPassword.
        /// </summary>
        /// <param name="emailID">emailID.</param>
        /// <returns>string.</returns>
        /// <exception cref="Exception">Exception.</exception>
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

        /// <summary>
        /// ResetPassword.
        /// </summary>
        /// <param name="userResetPassword">userResetPassword.</param>
        /// <param name="emailID">emailID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
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

        /// <summary>
        /// GetUser.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <returns>UserRegistrationModel.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public UserRegistrationModel GetUser(int userID)
        {
            try
            {
                return this.userRepository.GetUser(userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// GenerateJWTToken.
        /// </summary>
        /// <param name="emailID">emailID.</param>
        /// <param name="userID">UserID.</param>
        /// <returns>string.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public string GenerateJWTToken(string emailID, int userID)
        {
            try
            {
                return this.userRepository.GenerateJWTToken(emailID, userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
