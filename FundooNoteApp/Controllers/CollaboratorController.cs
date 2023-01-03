using FundooManager.Interface;
using FundooModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundooNoteApp.Controllers
{
    [Route("fundoo/[controller]")]
    [ApiController]
    [Authorize]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorManager collaboratorManager;
        private readonly ILogger<CollaboratorController> logger;
        public CollaboratorController(ICollaboratorManager collaboratorManager, ILogger<CollaboratorController> logger)
        {
            this.collaboratorManager = collaboratorManager;
            this.logger = logger;
        }
        [HttpPost]
        [Route("addcollaborator")]
        public IActionResult AddCollaborator(string collaboratorEmail, int noteID)
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                bool collaboratorAdded = this.collaboratorManager.AddCollaborator(collaboratorEmail, UserID, noteID);
                if (collaboratorAdded)
                {
                    this.logger.LogInformation("Collaborator is Added Successfully");
                    return this.Ok(new { success = true, message = $"Collaborator is Added Successfully to NoteID : {noteID}", result = collaboratorAdded });
                }
                this.logger.LogInformation("Collaborator is Not Added");
                return this.Ok(new { success = true, message = "Enter valid NoteID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
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
                this.logger.LogInformation("Collaborator is Not Removed");
                return this.Ok(new { success = true, message = "Enter valid CollaboratorID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        [Route("getcollaborators")]
        public IActionResult GetCollaborators(int noteID)
        {
            try
            {
                List<CollaboratorModel> collaborators = this.collaboratorManager.GetCollaborators(noteID);
                if (collaborators != null)
                {
                    return this.Ok(new { success = true, message = $"Collaborators Retrived Successfully of NoteID : {noteID}", result = collaborators });
                }
                return this.Ok(new { success = true, message = "Enter valid NoteID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
