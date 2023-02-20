// <copyright file="UserController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooNoteApp.Controllers
{
    using System.Security.Claims;
    using FundooManager.Interface;
    using FundooModel;
    using FundooNoteApp.Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using StackExchange.Redis;

    /// <summary>
    /// User Controller Class.
    /// </summary>
    [Route("fundoo/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;
        private readonly ILogger<UserController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// User Controller Constructor.
        /// </summary>
        /// <param name="userManager">usermanager.</param>
        /// <param name="logger">logger.</param>
        public UserController(IUserManager userManager, ILogger<UserController> logger)
        {
            this.userManager = userManager;
            this.logger = logger;
        }

        /// <summary>
        /// User Registration.
        /// </summary>
        /// <param name="userRegistrationModel">userRegistrationModel.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("fundoo/register")]
        public IActionResult Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserRegistrationModel registrationData = this.userManager.Registration(userRegistrationModel);
                if (registrationData != null)
                {
                    this.logger.LogInformation("Registration Successful");
                    return this.Ok(new { success = true, message = "Registration Successful", result = registrationData });
                }

                this.logger.LogError("User Not Registered");
                return this.BadRequest(new { success = true, message = "User Already Exists" });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Registration");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// User Login.
        /// </summary>
        /// <param name="userLogin">userLogin.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("fundoo/login")]
        public IActionResult Login(UserLoginModel userLogin)
        {
            try
            {
                UserRegistrationModel userData = this.userManager.Login(userLogin);
                if (userData != null)
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    string loginToken = this.userManager.GenerateJWTToken(userData.EmailID, userData.UserID);
#pragma warning restore CS8604 // Possible null reference argument.
                    this.SetSession(userData);
                    string? name = this.HttpContext.Session.GetString("UserName");
                    string? email = this.HttpContext.Session.GetString("UserEmail");
                    var userId = this.HttpContext.Session.GetInt32("UserId");
                    ConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
                    IDatabase database = multiplexer.GetDatabase();
                    string? firstName = database.StringGet("FirstName");
                    string? lastName = database.StringGet("LastName");
                    int userID = Convert.ToInt32(database.StringGet("UserID"));

#pragma warning disable CS8601 // Possible null reference assignment.
                    UserRegistrationModel registrationModel = new UserRegistrationModel()
                    {
                        UserID = userID,
                        FirstName = firstName,
                        LastName = lastName,
                        EmailID = userLogin.EmailID,
                    };
#pragma warning restore CS8601 // Possible null reference assignment.
                    this.logger.LogInformation("Login Successfull");
                    return this.Ok(new { success = true, message = "Login Successfull", result = loginToken });
                }

                this.logger.LogError("Doesn't Login");
                return this.BadRequest(new { success = true, message = "Enter Valid EmailID or Password" });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Login");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Forgot Password.
        /// </summary>
        /// <param name="emailID">emailID.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("fundoo/forgotpassword")]
        public IActionResult ForgotPassword(string emailID)
        {
            try
            {
                string? userEmailToken = this.userManager.ForgotPassword(emailID);
                if (userEmailToken != null)
                {
                    this.logger.LogInformation("Password Forgot Sucessfully");
                    return this.Ok(new { success = true, message = "Forgot Sucessfull ", result = userEmailToken });
                }

                this.logger.LogError("Password Not Forgot");
                return this.BadRequest(new { success = true, message = "Enter Valid EmailID " });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Forgot Password");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Reset Password.
        /// </summary>
        /// <param name="userResetPassword">userResetPassword.</param>
        /// <returns>IActionResult.</returns>
        [Authorize]
        [HttpPut]
        [Route("fundoo/resetpassword")]
        public IActionResult ResetPassword(UserResetPasswordModel userResetPassword)
        {
            try
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                string? emailID = this.User.FindFirst(ClaimTypes.Email).Value.ToString();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                if (userResetPassword.Password == userResetPassword.ConfirmPassword)
                {
                    bool userPassword = this.userManager.ResetPassword(userResetPassword, emailID);
                    if (userPassword)
                    {
                        this.logger.LogInformation("Password Reset Successfully");
                        return this.Ok(new { success = true, message = "Password Reset Successful", result = userPassword });
                    }
                }

                this.logger.LogError("Password Not Reset");
                return this.BadRequest(new { success = true, message = "Enter Password same as above" });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Reset Password");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// GetUser.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [Authorize]
        [HttpGet]
        public IActionResult GetUser()
        {
            try
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                int userID = Convert.ToInt32(this.User.FindFirst("UserID").Value);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                UserRegistrationModel registrationModel = this.userManager.GetUser(userID);
                if (registrationModel != null)
                {
                    this.logger.LogInformation("Get All User Successfully");
                    return this.Ok(new { Success = true, message = "Get User Sucessfully", result = registrationModel });
                }

                this.logger.LogError("All User Not Geted.");
                return this.BadRequest(new { Success = true, message = "User Not get", result = registrationModel });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Getting Users");
                return this.NotFound(new { Success = false, message = ex.Message });
            }
        }

        private void SetSession(UserRegistrationModel user)
        {
            this.HttpContext.Session.SetString("UserName", user.FirstName + " " + user.LastName);
#pragma warning disable CS8604 // Possible null reference argument.
            this.HttpContext.Session.SetString("UserEmail", user.EmailID);
#pragma warning restore CS8604 // Possible null reference argument.
            this.HttpContext.Session.SetInt32("UserId", user.UserID);
        }
    }
}
