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
    }
}
