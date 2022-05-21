using Microsoft.AspNetCore.Mvc;
using web_service_api.Services;
using web_service_api.Hubs;

namespace web_service_api.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UtilsController : ControllerBase
    {
        private static IMessagesService _messageService;

        private static IContactService _contactService;



        public UtilsController(IMessagesService messageService, IContactService contactService)
        {
            
            _messageService = messageService;
            _contactService = contactService;
        }

        public class BodyForInvitation
        {
            public string from { get; set; }
            public string to { get; set; }
            public string server { get; set; }
        }
        public class BodyForTransfer
        {
            public string from { get; set; }
            public string to { get; set; }
            public string content { get; set; }
        }

        [HttpPost("invitations")]

        public async Task invitations([FromBody] BodyForInvitation payload)
        {
            Contact contact = new Contact() { id = payload.from, myContact = payload.to, name = payload.from, server = "localhost:7093" };
            //Contact contact1 = new Contact() { id = payload.from, myContact = payload.to, name = payload.from, server = "localhost:7093" };
            //await _contactService.AddContact(contact1);
            await _contactService.AddContact(contact);

        }

        [HttpPost("transfer")]

        public async Task Transfer([FromBody] BodyForTransfer payload)
        {
            Message message = new Message() { content = payload.content , created = DateTime.Now, reciver = payload.to, sender = payload.from , mediaType = "text"};
            if (await _contactService.getContacts(payload.from) == null)
            {
                await _messageService.add(payload.from, message);
                await _contactService.ChangeLast(payload.to, payload.content, DateTime.Now, payload.from);
            }
            else await _contactService.ChangeLast(payload.to, payload.content, DateTime.Now, payload.from);

        }

    }
}
