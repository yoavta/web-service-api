using Microsoft.AspNetCore.Mvc;
using web_service_api.Services;

namespace web_service_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        private readonly IUsersService _userService;
        public UserController(IUsersService userService)
        {
            
            _userService = userService;

        }

        [HttpGet]
        public async Task<ICollection<User>?> getAllUser()
        {

            var users = await _userService.getUsers();
            return users;
        }

        [HttpPost]
        public async Task addUser([FromBody] User user)
        {
            await _userService.addUser(user);
        }


        [HttpGet("{user}")]
        public async Task<User?> getUser(string user)
        {
            var theUser = await _userService.getUser(user);
            return theUser;
        }
        

        [HttpGet("{user}/{password}")]

        public async Task<bool> checkUserLogIn(string? user, string? password)
        {
            var result = await _userService.checkValidPassword(userName: user, password: password);
            return result;
        }



    }
}
