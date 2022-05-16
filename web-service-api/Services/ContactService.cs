using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace web_service_api.Services
{
    public class ContactService : ContactInterface
    {
      

        private readonly WebServiceContext _context;
        public ContactService(WebServiceContext context)
        {
            
            _context = context;


        }
        public async Task<ICollection<Contact>?> getContacts(User _user)
        {
            var contacts = await _context.Contacts.Where(x => x.myContact == _user.UserName).ToListAsync();
            return contacts;
        }

        public async Task<Contact?> getContactById(User _user, string id)
        {
            var contacts = await getContacts(_user);
            if (contacts !=  null)
            {
                var contact = contacts.Where(x => x.id == id).FirstOrDefault();
                return contact;
            }
            return null;
            

        }

        public async Task<Contact?> AddContact(User _user,Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return await getContactById(_user, contact.id);
        }

        public async Task<Contact?> ChangeContact(User _user, Contact contact, string id)
        {
            if (await deleteContact(_user,id) == true)
            {
                var newContact = await AddContact(_user,contact);
                return newContact;
            }

            else return null;
        }


        public async Task<bool> deleteContact(User _user, string id)
        {
            var contact = await getContactById(_user, id);
            if (contact != null)
            {
                var removed = _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
                if (removed != null)
                {
                    return true;
                }
                
            }
            return false;
           
        }

        public async Task<int> getSize()
        {
            var size = await _context.Contacts.CountAsync();
            return size;
        }
    }
}
