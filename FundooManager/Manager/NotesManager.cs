// <copyright file="NotesManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooManager.Manager
{
    using FundooManager.Interface;
    using FundooModel;
    using FundooRepository.Interface;

    /// <summary>
    /// NotesManager.
    /// </summary>
    public class NotesManager : INotesManager
    {
        private readonly INotesRepository notesRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotesManager"/> class.
        /// </summary>
        /// <param name="notesRepository">notesRepository.</param>
        public NotesManager(INotesRepository notesRepository)
        {
            this.notesRepository = notesRepository;
        }

        /// <summary>
        /// CreateNotes.
        /// </summary>
        /// <param name="notecreateModel">notecreateModel.</param>
        /// <param name="userID">userID.</param>
        /// <returns>NoteCreateModel.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public NoteCreateModel CreateNotes(NoteCreateModel notecreateModel, int userID)
        {
            try
            {
                return this.notesRepository.CreateNotes(notecreateModel, userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// DisplayNotes.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <returns>List of NoteModel.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public List<NoteModel> DisplayNotes(int userID)
        {
            try
            {
                return this.notesRepository.DisplayNotes(userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// UpdateNotes.
        /// </summary>
        /// <param name="updateNote">updateNote.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>UpdateNoteModel.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public UpdateNoteModel UpdateNotes(UpdateNoteModel updateNote, int userID, int noteID)
        {
            try
            {
                return this.notesRepository.UpdateNotes(updateNote, userID, noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// DeleteNote.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool DeleteNote(int userID, int noteID)
        {
            try
            {
                return this.notesRepository.DeleteNote(userID, noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// PinNote.
        /// </summary>
        /// <param name="pinNote">pinNote.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool PinNote(bool pinNote, int userID, int noteID)
        {
            try
            {
                return this.notesRepository.PinNote(pinNote, userID, noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// ArchiveNote.
        /// </summary>
        /// <param name="archiveNote">archiveNote.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool ArchiveNote(bool archiveNote, int userID, int noteID)
        {
            try
            {
                return this.notesRepository.ArchiveNote(archiveNote, userID, noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// TrashNote.
        /// </summary>
        /// <param name="trashNote">trashNote.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool TrashNote(bool trashNote, int userID, int noteID)
        {
            try
            {
                return this.notesRepository.TrashNote(trashNote, userID, noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Color.
        /// </summary>
        /// <param name="color">color.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool Color(string color, int userID, int noteID)
        {
            try
            {
                return this.notesRepository.Color(color, userID, noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Remainder.
        /// </summary>
        /// <param name="remainder">remainder.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        /// <exception cref="Exception">Exception.</exception>
        public bool Remainder(DateTime remainder, int userID, int noteID)
        {
            try
            {
                return this.notesRepository.Remainder(remainder, userID, noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // public bool UploadImage(string filePath, int noteID, int userID)
        // {
        //    try
        //    {
        //        return this.notesRepository.UploadImage(filePath, userID, noteID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        // }
    }
}
