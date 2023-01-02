using FundooModel;

namespace FundooManager.Interface
{
    public interface ILabelManager
    {
        public bool AddLabel(string label, int noteID, int userID);
        public List<LabelModel> GetLables(int noteID);
        public bool UpdateLabel(string newlabel, int labelID);
        public bool DeleteLabel(int labelID);
    }
}
