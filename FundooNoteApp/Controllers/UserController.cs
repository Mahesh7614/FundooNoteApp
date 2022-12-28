﻿using FundooManager.Interface;
using FundooModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FundooNoteApp.Controllers
{
    [Route("fundoo/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;

        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }
        [HttpPost]
        [Route("fundoo/register")]
        public IActionResult Registration(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                UserRegistrationModel registrationData = this.userManager.Registration(userRegistrationModel);
                if (registrationData != null)
                {
                    return this.Ok(new { success = true, message = "Registration Successful", result = registrationData });
                }
                return this.Ok(new { success = true, message = "User Already Exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        [Route("fundoo/login")]
        public IActionResult Login(UserLoginModel userLogin)
        {
            try
            {
                string loginToken = this.userManager.Login(userLogin);
                if (loginToken != null)
                {
                    return this.Ok(new { success = true, message = "Login Successful", result = loginToken });
                }
                return this.Ok(new { success = true, message = "Enter Valid EmailID or Password" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        [Route("fundoo/forgotpassword")]
        public IActionResult ForgotPassword(string emailID)
        {
            try
            {
                string userEmailToken = this.userManager.ForgotPassword(emailID);
                if (userEmailToken != null)
                {
                    return this.Ok(new { success = true, message = "Forgot Sucessfull ", result = userEmailToken });
                }
                return this.Ok(new { success = true, message = "Enter Valid EmailID " });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("fundoo/resetpassword")]
        public IActionResult ResetPassword(UserResetPasswordModel userResetPassword)
        {
            try
            {
                string emailID = User.FindFirst(ClaimTypes.Email).Value.ToString();
                if (userResetPassword.Password == userResetPassword.ConfirmPassword)
                {
                    bool userPassword = this.userManager.ResetPassword(userResetPassword, emailID);
                    if (userPassword)
                    {
                        return this.Ok(new { success = true, message = "Password Reset Successful", result = userPassword });
                    }
                }
                return this.Ok(new { success = true, message = "Enter Password same as above" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
