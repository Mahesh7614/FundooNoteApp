using FundooModel;

namespace FundooManager.Interface
{
    public interface ICollaboratorManager
    {
        public bool AddCollaborator(string collaboratorEmail, int UserID, int NoteID);
        public bool RemoveCollaborator(int collaboratorID);
        public List<CollaboratorModel> GetCollaborators(int noteID);
    }
}
