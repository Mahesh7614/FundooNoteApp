﻿using FundooManager.Interface;
using FundooModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundooNoteApp.Controllers
{
    [Route("fundoo/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController : ControllerBase
    {
        private readonly ILabelManager labelManager;

        public LabelController(ILabelManager labelManager)
        {
            this.labelManager = labelManager;
        }
        [HttpPost]
        [Route("addlabel")]
        public IActionResult AddLabel(string label, int noteID)
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                bool labelAdded = this.labelManager.AddLabel(label, noteID, UserID);
                if (labelAdded)
                {
                    return this.Ok(new { success = true, message = $"Label is Added Successfully to NoteID : {noteID}", result = labelAdded });
                }
                return this.Ok(new { success = true, message = "Enter valid NoteID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        [Route("getlabels")]
        public IActionResult GetLabels(int noteID)
        {
            try
            {
                List<LabelModel> listLabel = this.labelManager.GetLables(noteID);
                if (listLabel != null)
                {
                    return this.Ok(new { success = true, message = $"Display Labels Successfully of NoteID : {noteID}", result = listLabel });
                }
                return this.Ok(new { success = true, message = "Enter valid NoteID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut]
        [Route("renamelabel")]
        public IActionResult UpdateLabel(string newLabel, int labelID)
        {
            try
            {
                bool renameLabel = this.labelManager.UpdateLabel(newLabel, labelID);
                if (renameLabel)
                {
                    return this.Ok(new { success = true, message = $"LabelID : {labelID} is renamed Successfully with new name : {newLabel}", result = renameLabel });
                }
                return this.Ok(new { success = true, message = "Enter valid LabelID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("deletelabel")]
        public IActionResult DeleteLabel(int labelID)
        {
            try
            {
                bool renameLabel = this.labelManager.DeleteLabel(labelID);
                if (renameLabel)
                {
                    return this.Ok(new { success = true, message = $"LabelID : {labelID} is Deleted Successfully", result = renameLabel });
                }
                return this.Ok(new { success = true, message = "Enter valid LabelID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
