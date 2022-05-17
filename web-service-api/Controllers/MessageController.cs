using Microsoft.AspNetCore.Mvc;
using web_service_api.Services;


namespace web_service_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private static User _user;

        private static IMessagesService _messageService;

        public class getByApiMessage
        {
            public int id { get; set; }
            public string contant { get; set; }
            public DateTime created { get; set; }
            public bool sent { get; set; }
        }

        public MessageController(IMessagesService messageService)
        {
            _user = new User()
            {
                UserName = "YahelJacoby",
                NickName = "Yahel",
                Password = "123a"
            };
            _messageService = messageService;

        }

        [HttpGet("{id}/messages")]
        public async Task<ICollection<getByApiMessage>?> getAllMessages(string id)
        {
            var messages = await _messageService.getMessagesOfUser(_user, id);
            ICollection<getByApiMessage> result = new List<getByApiMessage>();
            foreach (Message message in messages)
            {
                bool isSent;
                if (message.sender == _user.UserName)
                {
                    isSent = true;
                }
                else isSent = false;
                result.Add(new getByApiMessage() { id = message.id, contant = message.content , created = message.created , sent = isSent });
            }

            return result;
        }

        [HttpPost("{id}/messages")]
        public async Task addMessage(string id, [FromBody] string contant)
        {
            var message = new Message()
            {
                content = contant,
                created = DateTime.Now,
                sender = _user.UserName,
                reciver = id,
                
        };
            await _messageService.add(_user, message);

        }

        [HttpGet("{id}/messages/id2")]
        public async Task<getByApiMessage> getSpecificMessage(string id, int messageId)
        {
            var message = await _messageService.getSpecificMessage(messageId);
            if (message != null)
            {
                bool isSent;
                if (message.sender == _user.UserName)
                {
                    isSent = true;
                }
                else isSent = false;
                return new getByApiMessage() { id = message.id, contant = message.content, created = message.created, sent = isSent };
            }
            else return null;
            

        }

        [HttpPut("{id}/messages/id2")]
        public async Task changeMessage(string id, int messageId, [FromBody] string contant)
        {
            await _messageService.changeSpecificMessage(messageId, contant);
        }

        [HttpDelete("{id}/messages/id2")]
        public async Task deleteMessage(string id, int messageId)
        {
            await _messageService.remove(messageId);
        }


    }
}
