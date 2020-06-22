namespace IqOptionApi.Ws.Base
{
    public interface IWsIqOptionMessageCreator
    {
        string CreateIqOptionMessage(string requestId);
    }
}