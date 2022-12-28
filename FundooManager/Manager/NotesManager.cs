using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;

namespace FundooManager.Manager
{
    public class NotesManager : INotesManager
    {
        private readonly INotesRepository notesRepository;

        public NotesManager(INotesRepository notesRepository)
        {
            this.notesRepository = notesRepository;
        }
        public NoteModel CreateNotes(NoteModel noteModel, int userID)
        {
            try
            {
                return this.notesRepository.CreateNotes(noteModel, userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public NoteModel DisplayNotes(int userID)
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
    }
}
