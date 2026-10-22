using System.ComponentModel.DataAnnotations;

namespace chat.DTOs
{
    public class ChatRequestDto
    {
        [Required] 
        public string Message { get; set; } = string.Empty;
    }
}
