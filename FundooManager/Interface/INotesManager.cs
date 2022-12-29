﻿using FundooModel;

namespace FundooManager.Interface
{
    public interface INotesManager
    {
        public NoteCreateModel CreateNotes(NoteCreateModel notecreateModel, int userID);
        public List<NoteModel> DisplayNotes(int userID);
        public UpdateNoteModel UpdateNotes(UpdateNoteModel updateNote, int userID, int noteID);
        public bool DeleteNote(int userID, int noteID);
        public bool PinNote(bool pinNote, int userID, int noteID);
        public bool ArchiveNote(bool archiveNote, int userID, int noteID);
        public bool TrashNote(bool trashNote, int userID, int noteID);
    }
}
