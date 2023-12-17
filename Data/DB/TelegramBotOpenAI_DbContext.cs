using Microsoft.EntityFrameworkCore;
using TelegramBot_OpenAI.Models;

namespace TelegramBot_OpenAI.Data.DB
{
    public class TelegramBotOpenAI_DbContext : DbContext
    {
        public TelegramBotOpenAI_DbContext(DbContextOptions<TelegramBotOpenAI_DbContext> options) : base(options) { }

        public DbSet<TelegramUser> Users { get; set; }
        public DbSet<GeneratedImage> GeneratedImages { get; set; }
        public DbSet<GeneratedText> GeneratedTexts { get; set; }
    }
}
