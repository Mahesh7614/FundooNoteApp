// <copyright file="CollaboratorController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooNoteApp.Controllers
{
    using FundooManager.Interface;
    using FundooModel;
    using FundooNoteApp.Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// CollaboratorController.
    /// </summary>
    [Route("fundoo/[controller]")]
    [ApiController]
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorManager collaboratorManager;
        private readonly ILogger<CollaboratorController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollaboratorController"/> class.
        /// </summary>
        /// <param name="collaboratorManager">collaboratorManager.</param>
        /// <param name="logger">logger.</param>
        public CollaboratorController(ICollaboratorManager collaboratorManager, ILogger<CollaboratorController> logger)
        {
            this.collaboratorManager = collaboratorManager;
            this.logger = logger;
        }

        /// <summary>
        /// AddCollaborator.
        /// </summary>
        /// <param name="collaboratorEmail">collaboratorEmail.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("addcollaborator")]
        public IActionResult AddCollaborator(string collaboratorEmail, int noteID)
        {
            try
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                int userID = Convert.ToInt32(this.User.FindFirst("UserID").Value);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                bool collaboratorAdded = this.collaboratorManager.AddCollaborator(collaboratorEmail, userID, noteID);
                if (collaboratorAdded)
                {
                    this.logger.LogInformation("Collaborator is Added Successfully");
                    return this.Ok(new { success = true, message = $"Collaborator is Added Successfully to NoteID : {noteID}", result = collaboratorAdded });
                }

                this.logger.LogError("Collaborator is Not Added");
                return this.BadRequest(new { success = true, message = "Enter valid NoteID" });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Add Collaborator");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// RemoveCollaborator.
        /// </summary>
        /// <param name="collaboratorID">collaboratorID.</param>
        /// <returns>IActionResult.</returns>
        [HttpDelete]
        [Route("removecollaborator")]
        public IActionResult RemoveCollaborator(int collaboratorID)
        {
            try
            {
                bool collaboratorRemoved = this.collaboratorManager.RemoveCollaborator(collaboratorID);
                if (collaboratorRemoved)
                {
                    this.logger.LogInformation("Collaborator is Removed Successfully");
                    return this.Ok(new { success = true, message = $"Collaborator Removed Successfully", result = collaboratorRemoved });
                }

                this.logger.LogError("Collaborator is Not Removed");
                return this.BadRequest(new { success = true, message = "Enter valid CollaboratorID" });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Remove Collaborator");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// GetCollaborators.
        /// </summary>
        /// <param name="noteID">noteID.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("getcollaborators")]
        public IActionResult GetCollaborators(int noteID)
        {
            try
            {
                List<CollaboratorModel> collaborators = this.collaboratorManager.GetCollaborators(noteID);
                if (collaborators != null)
                {
                    this.logger.LogInformation("Get All Collaborators");
                    return this.Ok(new { success = true, message = $"Collaborators Retrived Successfully of NoteID : {noteID}", result = collaborators });
                }

                this.logger.LogError("All Collaborators not get");
                return this.BadRequest(new { success = true, message = "Enter valid NoteID" });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Get All Collaborators");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
