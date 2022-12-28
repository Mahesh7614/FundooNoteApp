using FundooModel;

namespace FundooManager.Interface
{
    public interface INotesManager
    {
        public NoteModel CreateNotes(NoteModel noteModel, int userID);
        public NoteModel DisplayNotes(int userID);
        public UpdateNoteModel UpdateNotes(UpdateNoteModel updateNote, int userID, int noteID);
        public bool DeleteNote(int userID, int noteID);
    }
}
