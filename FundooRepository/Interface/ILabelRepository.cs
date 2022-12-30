
using FundooModel;

namespace FundooRepository.Interface
{
    public interface ILabelRepository
    {
        public bool AddLabel(string label, int noteID, int userID);
        public List<LabelModel> GetLables(int noteID);
        public bool UpdateLabel(string newlabel, int labelID);
        public bool DeleteLabel(int labelID);
    }
}
