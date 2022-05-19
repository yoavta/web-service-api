using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace web_service_api.Services
{
    public class ContactService : IContactService
    {
      

        private readonly WebServiceContext _context;
        public ContactService(WebServiceContext context)
        {
            
            _context = context;


        }
        public async Task<ICollection<Contact>?> getContacts(string user)
        {
            var contacts = await _context.Contacts.Where(x => x.myContact == user).OrderBy(x => x.lastdate).ToListAsync();
            return contacts;
        }

        public async Task<Contact?> getContactById(string user, string id)
        {
            var contacts = await getContacts(user);
            if (contacts !=  null)
            {
                var contact = contacts.Where(x => x.id == id).FirstOrDefault();
                return contact;
            }
            return null;
            

        }

        public async Task AddContact(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            
        }

        public async Task ChangeContact(string _user, Contact contact, string id)
        {
            await deleteContact(_user, id);
            
        }

        public async Task ChangeLast(string user, string contant, DateTime created, string id)
        {
            var contact = await getContactById(user, id);
            contact.lastdate = created;
            contact.last = contant;
            await deleteContact(user, id); 
            await AddContact(contact);
        }


        public async Task deleteContact(string _user, string id)
        {
            var contact = await getContactById(_user, id);
            if (contact != null)
            {
                var removed = _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
                                
            }
            
           
        }

        public async Task<int> getSize()
        {
            var size = await _context.Contacts.CountAsync();
            return size;
        }
    }
}
