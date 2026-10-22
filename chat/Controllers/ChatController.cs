using chat.DTOs;
using chat.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        [HttpPost("message")]
        public ActionResult<ChatResponseDto> SendMessage
            ([FromBody] ChatRequestDto request)
        {
            // Simulate a response from the chat service
            var reply = _chatService.SendMessage(request.Message);
            var response = new ChatResponseDto
            {
                Reply = reply
            };
            return Ok(response);
        }

        [HttpGet("messages")]
        public async Task<ActionResult<List<ChatMessageDto>>> GetMessagesAsync()
        {
            var messages = await _chatService.GetMessagesAsync();
            return Ok(messages);
        }

        [HttpGet("message/{id:int}")]
        public async Task<ActionResult<ChatMessageDto>> GetMessageById(int id)
        {
            var message = await _chatService.GetMessagesByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }


        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            bool message = await _chatService.DeleteMessageAsync(id);

            if (!message)
            {
                return NotFound();
            }
            // Implementation for deleting the message would go here
            return Ok();
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult> UpdateMessage(int id, [FromBody] ChatRequestDto request)
        {
            var message = await _chatService.UpdateMessageAsync(id, request);
            if (message == null)
            {
                return NotFound();
            }
            // Implementation for updating the message would go here
            return Ok(message);
        }
    }
}
