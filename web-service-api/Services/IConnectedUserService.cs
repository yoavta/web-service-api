namespace web_service_api.Services
{
    public interface IConnectedUserService
    {

        public void connectUser(User? user);
        

        public void disconnectUser();

        public User GetUser();


    }
}
