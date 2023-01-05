namespace FundooNoteApp.Helpers
{
    public class FundooAppException : Exception
    {
        public FundooAppException() : base()
        { }
        public FundooAppException(string message) : base(message) { }

    }
}
