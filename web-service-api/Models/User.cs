using Microsoft.AspNetCore.Mvc;

namespace web_service_api
{
public class User
{
        //public User(string _UserName, string _NickName, string _Password, string? _PictureUrl)
        //{
        //    UserName = _UserName;
        //    NickName = _NickName;
        //    Password = _Password;
        //    PictureUrl = _PictureUrl;

        //}
        public string UserName { get; set; }

    public string NickName { get; set; }

    public string Password { get; set; }

    public string? PictureUrl { get; set;}

        /*public List<Chat> Chats { get; set;}*/
    }
}

