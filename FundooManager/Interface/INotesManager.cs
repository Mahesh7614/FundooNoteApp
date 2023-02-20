// <copyright file="INotesManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace FundooManager.Interface
{
    using FundooModel;

    /// <summary>
    /// INotesManager interface.
    /// </summary>
    public interface INotesManager
    {
        /// <summary>
        /// CreateNotes.
        /// </summary>
        /// <param name="notecreateModel">notecreateModel.</param>
        /// <param name="userID">userID.</param>
        /// <returns>NoteCreateModel.</returns>
        public NoteCreateModel CreateNotes(NoteCreateModel notecreateModel, int userID);

        /// <summary>
        /// DisplayNotes.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <returns>List of NoteModel.</returns>
        public List<NoteModel> DisplayNotes(int userID);

        /// <summary>
        /// UpdateNotes.
        /// </summary>
        /// <param name="updateNote">updateNote.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>UpdateNoteModel.</returns>
        public UpdateNoteModel UpdateNotes(UpdateNoteModel updateNote, int userID, int noteID);

        /// <summary>
        /// DeleteNote.
        /// </summary>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        public bool DeleteNote(int userID, int noteID);

        /// <summary>
        /// PinNote.
        /// </summary>
        /// <param name="pinNote">pinNote.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        public bool PinNote(bool pinNote, int userID, int noteID);

        /// <summary>
        /// ArchiveNote.
        /// </summary>
        /// <param name="archiveNote">archiveNote.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        public bool ArchiveNote(bool archiveNote, int userID, int noteID);

        /// <summary>
        /// TrashNote.
        /// </summary>
        /// <param name="trashNote">trashNote.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        public bool TrashNote(bool trashNote, int userID, int noteID);

        /// <summary>
        /// Color.
        /// </summary>
        /// <param name="color">color.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        public bool Color(string color, int userID, int noteID);

        /// <summary>
        /// Remainder.
        /// </summary>
        /// <param name="remainder">remainder.</param>
        /// <param name="userID">userID.</param>
        /// <param name="noteID">noteID.</param>
        /// <returns>bool.</returns>
        public bool Remainder(DateTime remainder, int userID, int noteID);

        // public bool UploadImage(string filePath, int noteID, int userID);
    }
}
