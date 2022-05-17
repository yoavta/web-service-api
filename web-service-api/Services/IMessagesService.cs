namespace web_service_api.Services
{
    public interface IMessagesService
    {
        public Task add(User user, Message message);
        public Task remove(int messageId);

        public Task<ICollection<Message>?> getMessagesOfUser(User user, string id);

        public Task<Message?> getSpecificMessage(int messageId);

        public Task changeSpecificMessage(int messageId, string contant);

        public Task<int> getSize();
        
    }
}
