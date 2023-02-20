// <copyright file="UserTicketController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooNoteApp.Controllers
{
    using FundooManager.Interface;
    using FundooModel;
    using FundooNoteApp.Helpers;
    using MassTransit;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// UserTicketController.
    /// </summary>
    [Route("fundoo/[controller]")]
    [ApiController]
    public class UserTicketController : ControllerBase
    {
        private readonly IBus bus;
        private readonly IUserManager userManager;
        private readonly ILogger<UserController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserTicketController"/> class.
        /// </summary>
        /// <param name="bus">bus.</param>
        /// <param name="userManager">userManager.</param>
        /// <param name="logger">logger.</param>
        public UserTicketController(IBus bus, IUserManager userManager, ILogger<UserController> logger)
        {
            this.bus = bus;
            this.userManager = userManager;
            this.logger = logger;
        }

        /// <summary>
        /// CreateTicketForPassword.
        /// </summary>
        /// <param name="emailID">EmailID.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet("ForgotPassword")]
        public async Task<IActionResult> CreateTicketForPassword(string emailID)
        {
            try
            {
                if (emailID != null)
                {
                    var token = this.userManager.ForgotPassword(emailID);
                    if (!string.IsNullOrEmpty(token))
                    {
                        UserTicket userTicket = this.userManager.CreateTicketForPassword(emailID, token);
                        Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
                        var endPoint = await this.bus.GetSendEndpoint(uri);
                        await endPoint.Send(userTicket);
                        this.logger.LogInformation("Email Sent Successfully");
                        return this.Ok(new { sucess = true, message = "Email Sent Successfully" });
                    }
                    else
                    {
                        this.logger.LogError("Email Not Sent");
                        return this.BadRequest(new { success = false, message = "EmailID not Registered" });
                    }
                }
                else
                {
                    this.logger.LogError("Email Not Sent");
                    return this.BadRequest(new { success = false, message = "Something went Wrong" });
                }
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for sending mail");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
