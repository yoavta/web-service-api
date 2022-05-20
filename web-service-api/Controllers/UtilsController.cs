using Microsoft.AspNetCore.Mvc;
using web_service_api.Services;
using web_service_api;

namespace web_service_api.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UtilsController : ControllerBase
    {
        private static IMessagesService _messageService;

        private static IContactService _contactService;

        private static MyHub _hub;


        public UtilsController(IMessagesService messageService, IContactService contactService )
        {
            
            _messageService = messageService;
            _contactService = contactService;
            _hub = new MyHub();
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
            Contact contact = new Contact() {  id =payload.to, myContact = payload.from , name = payload.to, server = payload.server};
            if( await _contactService.getContacts(payload.from) == null)
            {
                await _contactService.AddContact(contact);
            }
            
            await _hub.render(payload.to);
        }

        [HttpPost("transfer")]

        public async Task Transfer([FromBody] BodyForTransfer payload)
        {
            Message message = new Message() { content = payload.content , created = DateTime.Now, reciver = payload.to, sender = payload.from , mediaType = "text"};
            if (await _contactService.getContacts(payload.from) == null)
            {
                await _messageService.add(payload.from, message);
            }
            else await _contactService.ChangeLast(payload.from, payload.content, DateTime.Now, payload.to);

            await _hub.render(payload.to);
        }

    }
}
