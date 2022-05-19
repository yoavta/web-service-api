using Microsoft.AspNetCore.Mvc;
using web_service_api.Services;

namespace web_service_api.Controllers
{
    [ApiController]
    [Route("")]
    public class UtilsController : ControllerBase
    {
        private static IMessagesService _messageService;

        private static IContactService _contactService;
       


        public UtilsController(IMessagesService messageService, IContactService contactService )
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
            Contact contact = new Contact() {  id =payload.to, myContact = payload.from , name = payload.to, server = payload.server};
            await _contactService.AddContact(contact);
        }

        [HttpPost("transfer")]

        public async Task Transfer([FromBody] BodyForTransfer payload)
        {
            Message message = new Message() { content = payload.content , created = DateTime.Now, reciver = payload.to, sender = payload.from , mediaType = "text"};
            await _messageService.add(payload.from, message);
        }

    }
}
