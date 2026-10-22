using Microsoft.EntityFrameworkCore;
using chat.Models;
namespace chat.Data
{
    public class ChatDbContext: DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {
        }
        public DbSet<ChatMessage> ChatMessages { get; set; } = null!;
    }
}
