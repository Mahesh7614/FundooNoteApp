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

        public CollaboratorController(ICollaboratorManager collaboratorManager)
        {
            this.collaboratorManager = collaboratorManager;
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
                    return this.Ok(new { success = true, message = $"Collaborator is Added Successfully to NoteID : {noteID}", result = collaboratorAdded });
                }
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
                    return this.Ok(new { success = true, message = $"Collaborator Removed Successfully", result = collaboratorRemoved });
                }
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
