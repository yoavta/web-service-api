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
        public MessageService(WebServiceContext context)
        {

            _context = context;


        }
        public async Task add(Message message)
        {
            
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        public async Task changeSpecificMessage(int messageId, string contnt)
        {
            var message = await getSpecificMessage(messageId);
            await remove(messageId);
            message.content = contnt;
            await add(message);
        }

        public async Task<ICollection<Message>?> getMessagesOfUser(User user, string id)
        {
            var messages = await _context.Messages.Where(x => (x.sender == user.UserName && x.reciver == id) || (x.sender == id && x.reciver == user.UserName)).OrderBy(x => x.created).ToListAsync();
                
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
