namespace web_service_api.Services
{
    public interface IUtilsService
    {
        public Task Invitations(string from, string to, string server);

        public Task Transfer(string from, string to, string content);

    }
}
