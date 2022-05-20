using Microsoft.AspNetCore.Mvc;
using web_service_api.Services;

namespace web_service_api.Controllers
{



    [ApiController]
    [Route("api/contacts")]
    public class ContactsController : ControllerBase
    {
        public class ContactForAdd
        {

            public string Id { get; set; }

            public string Name { get; set; }
                   
            public string Server { get; set; }


        }

        public class ContactForChange
        {

            public string Name { get; set; }

            public string Server { get; set; }


        }

        public class ReturnContact
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public string Server { get; set; }

            public string? last { get; set; } 

            public DateTime? lastdate { get; set; }

        }

        

        private static IContactService _contactService;

        private static IConnectedUserService _user;

        public ContactsController(IContactService contactInterface, IConnectedUserService connectedUserService)
        {
            _user = connectedUserService;
            _contactService = contactInterface;
            
        }

        [HttpGet]
        public async Task<ICollection<ReturnContact>?> GetContact()
        {
            var userName = "";
            Request.Headers.TryGetValue("connectedUser", out var connectedUser);
            if (!connectedUser.Any())
            {
                userName = connectedUser[0];
            }
            else {
                userName = _user.GetUser().UserName;
            }
            var contacts = await _contactService.getContacts(userName);
            ICollection<ReturnContact> result = new List<ReturnContact>();
            foreach ( Contact contact in contacts)
            {
                result.Add(new ReturnContact()
                {
                    Id = contact.id,
                    last = contact.last,
                    lastdate = contact.lastdate,
                    Name = contact.name,
                    Server = contact.server

                });
            }


            return result;
        }

        [HttpPost]


        public async Task Creat([FromBody] ContactForAdd contactPayload)
        {
            var userName = "";
            Request.Headers.TryGetValue("connectedUser", out var connectedUser);
            if (!connectedUser.Any())
            {
                userName = connectedUser[0];
            }
            else
            {
                userName = _user.GetUser().UserName;
            }
            Contact newContact = new Contact();
            
            newContact.myContact = userName;
            newContact.server = contactPayload.Server;
            newContact.name = contactPayload.Name;
            newContact.id = contactPayload.Id;
            await _contactService.AddContact(newContact);

        }


        [HttpGet("{id}")]
        public async Task<ReturnContact> DetailsOfContact(string? id)
        {
            var userName = "";
            Request.Headers.TryGetValue("connectedUser", out var connectedUser);
            if (!connectedUser.Any())
            {
                userName = connectedUser[0];
            }
            else
            {
                userName = _user.GetUser().UserName;
            }



            if (id == null)
            {
                return null;
            }
            var contact = await _contactService.getContactById(userName, id);
            if (contact != null)
            {
                return new ReturnContact() { Id = contact.id, last = contact.last, lastdate = contact.lastdate, Name = contact.name, Server = contact.server };
            }
            else return null;
                        
        }

       

        [HttpPut("{id}")]
        public async Task Change([FromBody] ContactForChange contactPayload, string id)
        {

            var userName = "";
            Request.Headers.TryGetValue("connectedUser", out var connectedUser);
            if (!connectedUser.Any())
            {
                userName = connectedUser[0];
            }
            else
            {
                userName = _user.GetUser().UserName;
            }


            Contact newContact = new Contact();
            if (id != null)
            {
                var oldContact = await _contactService.getContactById(userName, id);               
                
                
                if (contactPayload.Server == "string")
                {
                    newContact.server = oldContact.server;
                }
                else newContact.server = contactPayload.Server;
                if (contactPayload.Name == "string")
                {
                    newContact.name = oldContact.name;
                  
                }
                else newContact.name = contactPayload.Name;
                newContact.key = oldContact.key;
                newContact.id = oldContact.id;
                newContact.lastdate = oldContact.lastdate;
                newContact.last = oldContact.last;
                newContact.myContact = userName;
                await _contactService.ChangeContact(userName, newContact, id);


            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(string? id)
        {
            await _contactService.deleteContact(_user.GetUser().UserName, id);
        }

    }
}