using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_service_api.Services
{
    interface IUsersService
    {
        public Task<ICollection<User>?> getUsers();

        public Task<ICollection<User>?> addUser(User user);

        public Task<User?> getUser(string userName);

        public Task<string> getUserUrl(string userName);

        public Task<bool> checkValidPassword(string userName, string password);     

        public Task<bool> isUserExist(string userName);

        

     /*   public Task<> getUserPassword(string UserName);

        public Task<ICollection<User>?> getUsersNickname(string UserName);

        public Task<ICollection<User>?> getUserUrl(string UserName);*/

    }
}
