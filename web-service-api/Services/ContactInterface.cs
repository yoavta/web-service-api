using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_service_api.Services
{
    public interface ContactInterface
    {
        public Task<ICollection<Contact>?> getContacts(User _user);

        public Task<Contact?> getContactById(User _user,string id);

        public Task<Contact?> AddContact(User _user,Contact contact);

        public Task<Contact?> ChangeContact(User _user,Contact contact, string id);

        public Task<bool> deleteContact(User _user, string id);

        public Task<int> getSize();



    }
}
