using FundooModel;

namespace FundooRepository.Interface
{
    public interface INotesRepository
    {
        public NoteModel CreateNotes(NoteModel noteModel, int userID);
        public NoteModel DisplayNotes(int userID);
    }
}
