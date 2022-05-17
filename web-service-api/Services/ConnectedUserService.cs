namespace web_service_api.Services
{
    public class ConnectedUserService : IConnectedUserService
    {
        private static User? _user;

        
        public void connectUser(User? user)
        {
            _user = user;
        }

        public void disconnectUser()
        {
            _user = null;
        }

        public User? GetUser()
        {
            return _user;
        }
    }
}
