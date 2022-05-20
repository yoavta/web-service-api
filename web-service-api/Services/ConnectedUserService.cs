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

        public User GetUser()
        {
            User user = new User();
            user.UserName = "YoavTamir";
            user.NickName = "yoyo";
            user.Password = "12345";
            user.PictureUrl = "https://media-exp1.licdn.com/dms/image/C5603AQE_ZKnvhgLTSQ/profile-displayphoto-shrink_800_800/0/1602488234599?e=1654128000&v=beta&t=j0NHdvjAcjhnP8uwL9mt4yrayIx9ktY9aMUtekANz0U";

            return user;
        }
    }
}
