using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace web_service_api.Services
{
    public class MessageService : IMessagesService
    {

        private readonly WebServiceContext _context;
        private readonly IContactService _contactService;
        public MessageService(WebServiceContext context, IContactService contactService)
        {

            _context = context;
            _contactService = contactService;

        }
        public async Task add(string user, Message message)
        {
            
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            string id;
            if (user == message.sender)
            {
                id = message.reciver;
            } else id = message.sender;
            string contant;
            if (message.mediaType == "text")
            {
                contant = message.content;
            }
            else contant = message.mediaType;
            await _contactService.ChangeLast(user, contant, message.created, id);

        }

        public async Task changeSpecificMessage( int messageId, string contnt)
        {
            var message = await getSpecificMessage(messageId);
            await remove(messageId);
            message.content = contnt;
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Message>?> getMessagesOfUser(string user, string id)
        {
            var messages1 = await _context.Messages.Where(x => (x.sender == user && x.reciver == id)).ToListAsync();
            var messages2 = await _context.Messages.Where(x => (x.sender == id && x.reciver == user)).ToListAsync();
            messages1.AddRange(messages2);
            var messages = messages1.OrderBy((x) => x.created).ToList();
            return messages;
        }

        public async Task<int> getSize()
        {
            return await _context.Messages.CountAsync();
        }

        public async Task<Message?> getSpecificMessage(int messageId)
        {
            var message = await _context.Messages.Where(x => x.id == messageId).FirstOrDefaultAsync();
            return message;

        }

        public async Task remove(int messageId)
        {
            var message = await getSpecificMessage(messageId);
            if (message != null)
            {
                _context.Messages.Remove(message);
                await _context.SaveChangesAsync();

            }
            
        }
    }
}
