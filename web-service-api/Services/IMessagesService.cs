namespace web_service_api.Services
{
    public interface IMessagesService
    {
        public Task add(string user, Message message);
        public Task remove(int messageId);

        public Task<ICollection<Message>?> getMessagesOfUser(string user, string id);

        public Task<Message?> getSpecificMessage(int messageId);

        public Task changeSpecificMessage(int messageId, string contant);

        public Task<int> getSize();
        
    }
}
