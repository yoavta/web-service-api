using Microsoft.AspNetCore.Mvc;
using web_service_api.Services;


namespace web_service_api.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessageController : ControllerBase
    {
      

        private static IMessagesService _messageService;

        private static IConnectedUserService _user;

        public class getByApiMessage
        {
            public int id { get; set; }
            public string contant { get; set; }
            public DateTime created { get; set; }
            public bool sent { get; set; }
        }

        public MessageController(IMessagesService messageService, IConnectedUserService connectedUserService)
        {
            _user = connectedUserService;
            _messageService = messageService;

        }

        [HttpGet("{id}/messages")]
        public async Task<ICollection<getByApiMessage>?> getAllMessages(string id)
        {
            var messages = await _messageService.getMessagesOfUser(_user.GetUser(), id);
            ICollection<getByApiMessage> result = new List<getByApiMessage>();
            foreach (Message message in messages)
            {
                bool isSent;
                if (message.sender == _user.GetUser().UserName)
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
                sender = _user.GetUser().UserName,
                reciver = id,
                mediaType = "text"
                
        };
            await _messageService.add(_user.GetUser().UserName, message);

        }

        [HttpPost("{id}/messagesType")]
        public async Task addMessageWithType(string id, [FromBody] Message message )
        {
            message.created = DateTime.Now;
            
            await _messageService.add(_user.GetUser().UserName, message);

        }

        [HttpGet("{id}/messagesType")]
        public async Task<ICollection<Message>?> getMessagesType(string id)
        {
            var messages = await _messageService.getMessagesOfUser(_user.GetUser(), id);
            return messages;
        }

        [HttpGet("{id}/messagesType/{id2}")]
        public async Task<Message?> getSpecificMessageType(string id, int id2)
        {
            var message = await _messageService.getSpecificMessage(id2);
            if (message != null)
            {
                if ((message.sender != id && message.reciver != id) || (message.sender != _user.GetUser().UserName && message.reciver != _user.GetUser().UserName))
                {
                    return null;
                }
                bool isSent;
                if (message.sender == _user.GetUser().UserName)
                {
                    isSent = true;
                }
                else isSent = false;
                return message;
            }
            else return null;


        }

        [HttpGet("{id}/messages/{id2}")]
        public async Task<getByApiMessage> getSpecificMessage(string id, int id2)
        {
            var message = await _messageService.getSpecificMessage(id2);
            if (message != null)
            {
                if ((message.sender != id && message.reciver != id) || (message.sender != _user.GetUser().UserName && message.reciver != _user.GetUser().UserName))
                {
                    return null;
                }
                bool isSent;
                if (message.sender == _user.GetUser().UserName)
                {
                    isSent = true;
                }
                else isSent = false;
                return new getByApiMessage() { id = message.id, contant = message.content, created = message.created, sent = isSent };
            }
            else return null;
            

        }

        [HttpPut("{id}/messages/{id2}")]
        public async Task changeMessage(string id, int id2, [FromBody] string contant)
        {
            var message = await _messageService.getSpecificMessage(id2);
            if (message != null)
            {
                if ((message.sender != id && message.reciver != id) || ( message.sender != _user.GetUser().UserName && message.reciver != _user.GetUser().UserName))
                {
                    return;
                }
            }
            await _messageService.changeSpecificMessage(id2, contant);
        }

        [HttpDelete("{id}/messages/{id2}")]
        public async Task deleteMessage(string id, int id2)
        {
            var message = await _messageService.getSpecificMessage(id2);
            if (message != null)
            {
                if ((message.sender != id && message.reciver != id) || (message.sender != _user.GetUser().UserName && message.reciver != _user.GetUser().UserName))
                {
                    return;
                }
            }
            await _messageService.remove(id2);
        }


    }
}
