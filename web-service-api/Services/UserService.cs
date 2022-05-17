using Microsoft.EntityFrameworkCore;
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

    public async Task addUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> checkValidPassword(string userName, string password)
        {
            var user = await getUser(userName);
            if (user != null)
            {
                if (user.Password == password)
                {
                    return true;
                }
                

            }return false;
            
        }

        public async Task<User?> getUser(string UserName)
        {
            var user = await _context.Users.FindAsync(UserName);
            if (user != null)
            {
                return user;
            }
            else return null;
            
        }

        public async Task<ICollection<User>?> getUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

       
    }
}
