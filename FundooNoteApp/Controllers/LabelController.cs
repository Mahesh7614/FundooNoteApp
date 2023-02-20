// <copyright file="LabelController.cs" company="PlaceholderCompany">
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
    /// LabelController.
    /// </summary>
    [Route("fundoo/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController : ControllerBase
    {
        private readonly ILabelManager labelManager;
        private readonly ILogger<LabelController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelController"/> class.
        /// </summary>
        /// <param name="labelManager">labelManager.</param>
        /// <param name="logger">logger.</param>
        public LabelController(ILabelManager labelManager, ILogger<LabelController> logger)
        {
            this.labelManager = labelManager;
            this.logger = logger;
        }

        /// <summary>
        /// AddLabel.
        /// </summary>
        /// <param name="label">label.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("addlabel")]
        public IActionResult AddLabel(string label, int noteID)
        {
            try
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                int userID = Convert.ToInt32(this.User.FindFirst("UserID").Value);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                bool labelAdded = this.labelManager.AddLabel(label, noteID, userID);
                if (labelAdded)
                {
                    this.logger.LogInformation("Label is Added Successfully");
                    return this.Ok(new { success = true, message = $"Label is Added Successfully to NoteID : {noteID}", result = labelAdded });
                }

                this.logger.LogError("Label is Not Added");
                return this.BadRequest(new { success = true, message = "Enter valid NoteID" });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Add Label");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// GetLabels.
        /// </summary>
        /// <param name="noteID">noteID.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("getlabels")]
        public IActionResult GetLabels(int noteID)
        {
            try
            {
                List<LabelModel> listLabel = this.labelManager.GetLables(noteID);
                if (listLabel != null)
                {
                    this.logger.LogInformation("Get All Labels");
                    return this.Ok(new { success = true, message = $"Display Labels Successfully of NoteID : {noteID}", result = listLabel });
                }

                this.logger.LogError("All Labels not get");
                return this.BadRequest(new { success = true, message = "Enter valid NoteID" });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Get Labels");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// UpdateLabel.
        /// </summary>
        /// <param name="newLabel">newLabel.</param>
        /// <param name="labelID">labelID.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("renamelabel")]
        public IActionResult UpdateLabel(string newLabel, int labelID)
        {
            try
            {
                bool renameLabel = this.labelManager.UpdateLabel(newLabel, labelID);
                if (renameLabel)
                {
                    this.logger.LogInformation("Label is Updated");
                    return this.Ok(new { success = true, message = $"LabelID : {labelID} is renamed Successfully with new name : {newLabel}", result = renameLabel });
                }

                this.logger.LogError("Label is not Updated");
                return this.BadRequest(new { success = true, message = "Enter valid LabelID" });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Update Label");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// DeleteLabel.
        /// </summary>
        /// <param name="labelID">labelID.</param>
        /// <returns>IActionResult.</returns>
        [HttpDelete]
        [Route("deletelabel")]
        public IActionResult DeleteLabel(int labelID)
        {
            try
            {
                bool renameLabel = this.labelManager.DeleteLabel(labelID);
                if (renameLabel)
                {
                    this.logger.LogInformation("Label Deleted Successfully");
                    return this.Ok(new { success = true, message = $"LabelID : {labelID} is Deleted Successfully", result = renameLabel });
                }

                this.logger.LogError("Label Not Deleted");
                return this.BadRequest(new { success = true, message = "Enter valid LabelID" });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Delete Label");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}