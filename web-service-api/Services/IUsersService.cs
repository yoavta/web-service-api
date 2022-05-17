using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_service_api.Services
{
    public interface IUsersService
    {
        public Task<ICollection<User>?> getUsers();

        public Task addUser(User user);

        public Task<User?> getUser(string userName);



        public Task<bool> checkValidPassword(string userName, string password);     



        


    }
}
