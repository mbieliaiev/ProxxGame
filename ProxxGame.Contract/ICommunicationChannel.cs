namespace ProxxGame.Contract
{
    public interface ICommunicationChannel
    {
        string ReadWithMessage(string message);
        int ReadIntWithMessage(string message);

        void ShowMessage(string message);
    }
}
