
using FundooManager.Interface;
using FundooModel;
using FundooRepository.Interface;

namespace FundooManager.Manager
{
    public class LabelManager : ILabelManager
    {
        private readonly ILabelRepository labelRepository;

        public LabelManager(ILabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }
        public bool AddLabel(string label, int noteID, int userID)
        {
            try
            {
                return this.labelRepository.AddLabel(label, noteID, userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<LabelModel> GetLables(int noteID)
        {
            try
            {
                return this.labelRepository.GetLables(noteID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateLabel(string newlabel, int labelID)
        {
            try
            {
                return this.labelRepository.UpdateLabel(newlabel, labelID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteLabel(int labelID)
        {
            try
            {
                return this.labelRepository.DeleteLabel(labelID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
