using FundooManager.Interface;
using FundooModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FundooNoteApp.Controllers
{
    [Route("fundoo/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INotesManager notesManager;

        public NotesController(INotesManager notesManager)
        {
            this.notesManager = notesManager;
        }
        [HttpPost]
        [Route("createnotes")]
        public IActionResult CreateNotes(NoteModel noteModel)
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                NoteModel NotesData = this.notesManager.CreateNotes(noteModel, UserID);
                if (NotesData != null)
                {
                    return this.Ok(new { success = true, message = "Note Created Successful", result = NotesData });
                }
                return this.Ok(new { success = true, message = "Note Title Already Exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpGet]
        [Route("getnotes")]
        public IActionResult DisplayNotes()
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                NoteModel NotesData = this.notesManager.DisplayNotes(UserID);
                if (NotesData != null)
                {
                    return this.Ok(new { success = true, message = "Display Notes Successful", result = NotesData });
                }
                return this.Ok(new { success = true, message = "Notes are Empty" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
