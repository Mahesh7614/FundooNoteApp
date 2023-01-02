using FundooModel;

namespace FundooRepository.Interface
{
    public interface ICollaboratorRepository
    {
        public bool AddCollaborator(string collaboratorEmail, int UserID, int NoteID);
        public bool RemoveCollaborator(int collaboratorID);
        public List<CollaboratorModel> GetCollaborators(int noteID);
    }
}