using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;

namespace FundooManager.Manager
{
    public class CollaboratorManager : ICollaboratorManager
    {
        private readonly ICollaboratorRepository collaboratorRepository;

        public CollaboratorManager(ICollaboratorRepository collaboratorRepository)
        {
            this.collaboratorRepository = collaboratorRepository;
        }
        public bool AddCollaborator(string collaboratorEmail, int UserID, int NoteID)
        {
            try
            {
                return this.collaboratorRepository.AddCollaborator(collaboratorEmail, UserID, NoteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool RemoveCollaborator(int collaboratorID)
        {
            try
            {
                return this.collaboratorRepository.RemoveCollaborator(collaboratorID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<CollaboratorModel> GetCollaborators(int noteID)
        {
            try
            {
                return this.collaboratorRepository.GetCollaborators(noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
