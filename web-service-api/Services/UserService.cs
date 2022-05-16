using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace web_service_api.Services
{
    public class UserService : IUsersService
    {
        private readonly WebServiceContext _context;
        public UserService(WebServiceContext context)
        {
            _context = context;
        }

    public Task<ICollection<User>?> addUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> checkValidPassword(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User?> getUser(string UserName)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<User>?> getUsers()
        {
            throw new NotImplementedException();
        }

        public Task<string> getUserUrl(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> isUserExist(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
