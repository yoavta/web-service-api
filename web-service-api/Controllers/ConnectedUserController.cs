using Microsoft.AspNetCore.Mvc;
using web_service_api.Services;

namespace web_service_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConnectedUserController : ControllerBase
    {
        private readonly IConnectedUserService _ConnectedUserService;
        private readonly IUsersService _userService;
        public ConnectedUserController(IConnectedUserService connectedUserService, IUsersService usersService)
        {
            _ConnectedUserService = connectedUserService;    
            _userService = usersService;
        }

        [HttpPost]
        public bool connectUser(string userName)
        {
            var user = _userService.getUser(userName);

            _ConnectedUserService.connectUser(user.Result);


            if (_ConnectedUserService.GetUser() != null)
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public User getConnectedUser()
        {
            return _ConnectedUserService.GetUser();
        }

        [HttpDelete]
        public bool disconnectUser()
        {
            _ConnectedUserService.disconnectUser();
            if (_ConnectedUserService.GetUser() != null)
            {
                return false;
            }
            return true;
        }



    }
}
