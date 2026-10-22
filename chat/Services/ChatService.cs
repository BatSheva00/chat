using chat.Data;
using chat.Models;
using chat.DTOs;
using Microsoft.EntityFrameworkCore;

namespace chat.Services
{
    public class ChatService : IChatService
    {
        private readonly ChatDbContext _dbContext;
        public ChatService(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string SendMessage(string message)
        {
            string reply = $"You said: {message}";
 
            var chatMessage = new ChatMessage
            {
                Message = message,
                Reply = reply,
                CreatedAt = DateTime.UtcNow
            };
            _dbContext.ChatMessages.Add(chatMessage);
            _dbContext.SaveChanges();
            return reply;
        }
        public async Task<bool> DeleteMessageAsync(int id)
        {
            var chatMessage = await _dbContext.ChatMessages.FindAsync(id);
            if (chatMessage != null)
            {
                _dbContext.ChatMessages.Remove(chatMessage);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<List<ChatMessageDto>> GetMessagesAsync()
        {
            return await _dbContext.ChatMessages.AsNoTracking()
                .OrderByDescending(m => m.CreatedAt)
                .Select(m => new ChatMessageDto
                {
                    Id = m.Id,
                    Message = m.Message,
                    Reply = m.Reply,
                    CreatedAt = m.CreatedAt
                })
                .ToListAsync();
            
        }

        public async Task<ChatMessageDto?> GetMessagesByIdAsync(int id)
        {
            var chatMessage = await _dbContext.ChatMessages.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (chatMessage == null)
            {
                return null;
            }

            return new ChatMessageDto
            {
                Id = chatMessage.Id,
                Message = chatMessage.Message,
                Reply = chatMessage.Reply,
                CreatedAt = chatMessage.CreatedAt
            };
        }
        public async Task<string> UpdateMessageAsync(int id, ChatRequestDto request)
        {
            var chatMessage = await _dbContext.ChatMessages.FindAsync(id);
            if (chatMessage == null)
            {
                return $"Message with ID {id} not found.";
            }
            chatMessage.Message = request.Message;
            chatMessage.Reply = $"You said: {request.Message}";
            _dbContext.ChatMessages.Update(chatMessage);
            await _dbContext.SaveChangesAsync();
            return $"Message with ID {id} updated.";
        }

    }
   
}
