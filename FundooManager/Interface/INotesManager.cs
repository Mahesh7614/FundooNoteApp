
using FundooModel;

namespace FundooManager.Interface
{
    public interface INotesManager
    {
        public NoteModel CreateNotes(NoteModel noteModel, int userID);
        public NoteModel DisplayNotes(int userID);
    }
}
