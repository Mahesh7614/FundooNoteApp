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
        private readonly ILogger<NotesController> logger;

        public NotesController(INotesManager notesManager, ILogger<NotesController> logger)
        {
            this.notesManager = notesManager;
            this.logger = logger;
            logger.LogDebug(1, "NLog injected into NoteController");
        }
        [HttpPost]
        [Route("createnotes")]
        public IActionResult CreateNotes(NoteCreateModel notecreateModel)
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                NoteCreateModel NotesData = this.notesManager.CreateNotes(notecreateModel, UserID);
                if (NotesData != null)
                {
                    this.logger.LogInformation("Note is Created");
                    return this.Ok(new { success = true, message = "Note Created Successfully", result = NotesData });
                }
                this.logger.LogInformation("Note is not Created");
                return this.Ok(new { success = true, message = "Note not Created" });
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
                List<NoteModel> NotesData = this.notesManager.DisplayNotes(UserID);
                if (NotesData != null)
                {
                    this.logger.LogInformation("All Notes are Displayed");
                    return this.Ok(new { success = true, message = "Display Notes Successfully", result = NotesData });
                }
                this.logger.LogInformation("Notes not get");

                return this.Ok(new { success = true, message = "Notes are Empty" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPatch]
        [Route("updatenote")]
        public IActionResult UpdateNotes(UpdateNoteModel updateNote, int noteID)
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                UpdateNoteModel updateNoteData = this.notesManager.UpdateNotes(updateNote, UserID, noteID);
                if (updateNote != null)
                {
                    return this.Ok(new { success = true, message = "Note Updated Successfully", result = updateNote });
                }
                return this.Ok(new { success = true, message = "Enter Valid NoteID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("deletenote")]
        public IActionResult DeleteNote(int noteID)
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                bool deleteNote = this.notesManager.DeleteNote(UserID, noteID);
                if (deleteNote)
                {
                    return this.Ok(new { success = true, message = "Delete Note Successfully", result = deleteNote });
                }
                return this.Ok(new { success = true, message = "Enter Valid NoteID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut]
        [Route("pinnote")]
        public IActionResult PinNote(bool pinNote, int noteID)
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                bool pin = this.notesManager.PinNote(pinNote, UserID, noteID);
                if (pin)
                {
                    return this.Ok(new { success = true, message = "PinNote Operation is Successfull", result = pin });
                }
                return this.Ok(new { success = true, message = "Enter Valid NoteID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut]
        [Route("archivenote")]
        public IActionResult ArchiveNote(bool archiveNote, int noteID)
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                bool archive = this.notesManager.ArchiveNote(archiveNote, UserID, noteID);
                if (archive)
                {
                    return this.Ok(new { success = true, message = "Archive Operation is Successfull", result = archive });
                }
                return this.Ok(new { success = true, message = "Enter Valid NoteID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut]
        [Route("trashnote")]
        public IActionResult TrashNote(bool trashNote, int noteID)
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                bool trash = this.notesManager.TrashNote(trashNote, UserID, noteID);
                if (trash)
                {
                    return this.Ok(new { success = true, message = "Trash Operation is Successfull", result = trash });
                }
                return this.Ok(new { success = true, message = "Enter Valid NoteID" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut]
        [Route("updatecolor")]
        public IActionResult Color(string color, int noteID)
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                bool updateColor = this.notesManager.Color(color, UserID, noteID);
                if (updateColor)
                {
                    return this.Ok(new { success = true, message = "Color Updated Successfully", result = updateColor });
                }
                return this.Ok(new { success = true, message = "Enter valid NoteID " });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut]
        [Route("setremainder")]
        public IActionResult Remainder(DateTime remainder, int noteID)
        {
            try
            {
                int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
                bool updateRemainder = this.notesManager.Remainder(remainder, UserID, noteID);
                if (updateRemainder)
                {
                    return this.Ok(new { success = true, message = "Remainder Updated Successfully", result = updateRemainder });
                }
                return this.Ok(new { success = true, message = "Enter valid NoteID " });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });
            }
        }
        //[HttpPut]
        //[Route("uploadimage")]
        //public IActionResult UploadImage(string filepath, int noteID)
        //{
        //    try
        //    {
        //        int UserID = Convert.ToInt32(User.FindFirst("UserID").Value);
        //        bool upload = this.notesManager.UploadImage(filepath, UserID, noteID);
        //        if (upload)
        //        {
        //            return this.Ok(new { success = true, message = "Image Uploaded Successfully", result = upload });
        //        }
        //        return this.Ok(new { success = true, message = "Enter valid NoteID or File Path" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return this.BadRequest(new { success = false, message = ex.Message });
        //    }
        //}
    }
}
