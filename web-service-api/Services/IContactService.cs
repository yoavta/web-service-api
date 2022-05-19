using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_service_api.Services
{
    public interface IContactService
    {
        public Task<ICollection<Contact>?> getContacts(string _user);

        public Task<Contact?> getContactById(string _user,string id);

        public Task AddContact(Contact contact);

        public Task ChangeContact(string _user, Contact contact, string id);

        public Task deleteContact(string _user, string id);

        public Task<int> getSize();

        public Task ChangeLast(string user, string contant, DateTime created, string id);



    }
}
