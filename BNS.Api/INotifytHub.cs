namespace BNS.Api
{
    public interface INotifytHub
    {
        void SendChatMessage(string who, string message);
    }
}
