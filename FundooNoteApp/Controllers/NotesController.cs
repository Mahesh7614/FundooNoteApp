// <copyright file="NotesController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooNoteApp.Controllers
{
    using System.Text;
    using FundooManager.Interface;
    using FundooModel;
    using FundooNoteApp.Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;

    /// <summary>
    /// NotesController.
    /// </summary>
    [Route("fundoo/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INotesManager notesManager;
        private readonly ILogger<NotesController> logger;
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesController"/> class.
        /// </summary>
        /// <param name="notesManager">notesManager.</param>
        /// <param name="logger">logger.</param>
        /// <param name="distributedCache">distributedCache.</param>
        public NotesController(INotesManager notesManager, IDistributedCache distributedCache, ILogger<NotesController> logger)
        {
            this.notesManager = notesManager;
            this.logger = logger;
            this.distributedCache = distributedCache;
            logger.LogDebug(1, "NLog injected into NoteController");
        }

        /// <summary>
        /// CreateNotes.
        /// </summary>
        /// <param name="notecreateModel">notecreateModel.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("createnotes")]
        public IActionResult CreateNotes(NoteCreateModel notecreateModel)
        {
            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("UserID").Value);
                NoteCreateModel notesData = this.notesManager.CreateNotes(notecreateModel, userID);
                if (notesData != null)
                {
                    this.logger.LogInformation("Note is Created");
                    return this.Ok(new { success = true, message = "Note Created Successfully", result = notesData });
                }

                this.logger.LogError("Note is not Created");
                return this.BadRequest(new { success = true, message = "Note not Created" });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Create Note");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// DisplayNotes.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("getnotes")]
        public IActionResult DisplayNotes()
        {
            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("UserID").Value);
                List<NoteModel> notesData = this.notesManager.DisplayNotes(userID);
                if (notesData != null)
                {
                    this.logger.LogInformation("All Notes are Displayed");
                    return this.Ok(new { success = true, message = "Display Notes Successfully", result = notesData });
                }

                this.logger.LogError("Notes not get");
                return this.BadRequest(new { success = true, message = "Notes are Empty" });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Display Notes");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// UpdateNotes.
        /// </summary>
        /// <param name="updateNote">updateNote.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>IActionResult.</returns>
        [HttpPatch]
        [Route("updatenote")]
        public IActionResult UpdateNotes(UpdateNoteModel updateNote, int noteID)
        {
            try
            {
                int userID = Convert.ToInt32(this.User.FindFirst("UserID").Value);
                UpdateNoteModel updateNoteData = this.notesManager.UpdateNotes(updateNote, userID, noteID);
                if (updateNote != null)
                {
                    this.logger.LogInformation("Note is Updated");
                    return this.Ok(new { success = true, message = "Note Updated Successfully", result = updateNote });
                }

                this.logger.LogError("Note is not Updated");
                return this.BadRequest(new { success = true, message = "Enter Valid NoteID" });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Update Note");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// DeleteNote.
        /// </summary>
        /// <param name="noteID">noteID.</param>
        /// <returns>IActionResult.</returns>
        [HttpDelete]
        [Route("deletenote")]
        public IActionResult DeleteNote(int noteID)
        {
            try
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                int userID = Convert.ToInt32(this.User.FindFirst("UserID").Value);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                bool deleteNote = this.notesManager.DeleteNote(userID, noteID);
                if (deleteNote)
                {
                    this.logger.LogInformation("Note is Deleted");
                    return this.Ok(new { success = true, message = "Delete Note Successfully", result = deleteNote });
                }

                this.logger.LogError("Note is not Deleted");
                return this.BadRequest(new { success = true, message = "Enter Valid NoteID" });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Delete Note");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// PinNote.
        /// </summary>
        /// <param name="pinNote">pinNote.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("pinnote")]
        public IActionResult PinNote(bool pinNote, int noteID)
        {
            try
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                int userID = Convert.ToInt32(this.User.FindFirst("UserID").Value);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                bool pin = this.notesManager.PinNote(pinNote, userID, noteID);
                if (pin)
                {
                    this.logger.LogInformation("Note is Pinned");
                    return this.Ok(new { success = true, message = "PinNote Operation is Successfull", result = pin });
                }

                this.logger.LogError("Note is not Pinned");
                return this.BadRequest(new { success = true, message = "Enter Valid NoteID" });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Pin Note");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// ArchiveNote.
        /// </summary>
        /// <param name="archiveNote">archiveNote.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("archivenote")]
        public IActionResult ArchiveNote(bool archiveNote, int noteID)
        {
            try
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                int userID = Convert.ToInt32(this.User.FindFirst("UserID").Value);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                bool archive = this.notesManager.ArchiveNote(archiveNote, userID, noteID);
                if (archive)
                {
                    this.logger.LogInformation("Note is Archieved");
                    return this.Ok(new { success = true, message = "Archive Operation is Successfull", result = archive });
                }

                this.logger.LogError("Note is not Archieved");
                return this.BadRequest(new { success = true, message = "Enter Valid NoteID" });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Archieve Note");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// TrashNote.
        /// </summary>
        /// <param name="trashNote">trashNote.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("trashnote")]
        public IActionResult TrashNote(bool trashNote, int noteID)
        {
            try
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                int userID = Convert.ToInt32(this.User.FindFirst("UserID").Value);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                bool trash = this.notesManager.TrashNote(trashNote, userID, noteID);
                if (trash)
                {
                    this.logger.LogInformation("Trash Operation is Successfull");
                    return this.Ok(new { success = true, message = "Trash Operation is Successfull", result = trash });
                }

                this.logger.LogError("Trash Operation is not Successfull");
                return this.BadRequest(new { success = true, message = "Enter Valid NoteID" });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Trash Note");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Color.
        /// </summary>
        /// <param name="color">color.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("updatecolor")]
        public IActionResult Color(string color, int noteID)
        {
            try
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                int userID = Convert.ToInt32(this.User.FindFirst("UserID").Value);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                bool updateColor = this.notesManager.Color(color, userID, noteID);
                if (updateColor)
                {
                    this.logger.LogInformation("Note Color is Updated");
                    return this.Ok(new { success = true, message = "Color Updated Successfully", result = updateColor });
                }

                this.logger.LogError("Note Color is not Updated");
                return this.BadRequest(new { success = true, message = "Enter valid NoteID " });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Note Color");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Remainder.
        /// </summary>
        /// <param name="remainder">remainder.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("setremainder")]
        public IActionResult Remainder(DateTime remainder, int noteID)
        {
            try
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                int userID = Convert.ToInt32(this.User.FindFirst("UserID").Value);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                bool updateRemainder = this.notesManager.Remainder(remainder, userID, noteID);
                if (updateRemainder)
                {
                    this.logger.LogInformation("Note Remainder is Updated");
                    return this.Ok(new { success = true, message = "Remainder Updated Successfully", result = updateRemainder });
                }

                this.logger.LogError("Note Remainder is not Updated");
                return this.BadRequest(new { success = true, message = "Enter valid NoteID " });
            }
            catch (FundooAppException ex)
            {
                this.logger.LogError("Getting an Exception for Note Remainder");
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetAllNotesUsingRadisCache")]
        public async Task<IActionResult> GetAllNotesUsingRadis()
        {
            try
            {
                var cacheKey = "NotesList";
                List<NoteModel> noteList;
                byte[] redisNoteList = await this.distributedCache.GetAsync(cacheKey);
                if (redisNoteList != null)
                {
                    this.logger.LogDebug("Getting the list from Redis Cache");
                    var serializedNoteList = Encoding.UTF8.GetString(redisNoteList);
                    noteList = JsonConvert.DeserializeObject<List<NoteModel>>(serializedNoteList);
                }
                else
                {
                    this.logger.LogDebug("Setting the list to cache which is required for the first time");
                    int userID = Convert.ToInt32(this.User.FindFirst("UserID").Value);
                    noteList = this.notesManager.DisplayNotes(userID);
                    var serializedNoteList = JsonConvert.SerializeObject(noteList);
                    var newRedisNoteList = Encoding.UTF8.GetBytes(serializedNoteList);
                    var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)).SetSlidingExpiration(TimeSpan.FromMinutes(5));
                    await this.distributedCache.SetAsync(cacheKey, newRedisNoteList, options);
                }

                this.logger.LogInformation("Got the notes list sucessfully from Redis");
                return this.Ok(noteList);
            }
            catch (FundooAppException ex)
            {
                this.logger.LogCritical(ex, "Exception thrown...");
                return this.BadRequest(new { sucess = false, message = ex.Message });
            }
        }

        // [HttpPut]
        // [Route("uploadimage")]
        // public IActionResult UploadImage(string filepath, int noteID)
        // {
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
        // }
    }
}
