using FundooModel;

namespace FundooRepository.Interface
{
    public interface INotesRepository
    {
        public NoteCreateModel CreateNotes(NoteCreateModel notecreateModel, int userID);
        public List<NoteModel> DisplayNotes(int userID);
        public UpdateNoteModel UpdateNotes(UpdateNoteModel updateNote, int userID, int noteID);
        public bool DeleteNote(int userID, int noteID);
        public bool PinNote(bool pinNote, int userID, int noteID);
        public bool ArchiveNote(bool archiveNote, int userID, int noteID);
        public bool TrashNote(bool trashNote, int userID, int noteID);
        public bool Color(string color, int userID, int noteID);
        public bool Remainder(DateTime remainder, int userID, int noteID);
    }
}
