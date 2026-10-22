using chat.DTOs;

namespace chat.Services
{
    public interface IChatService
    {
        string SendMessage(string message);
        Task<ChatMessageDto?> GetMessagesByIdAsync(int id);
        Task<List<ChatMessageDto>> GetMessagesAsync();
        Task<bool> DeleteMessageAsync(int id);
        Task<string> UpdateMessageAsync(int id, ChatRequestDto request);
    }
}
