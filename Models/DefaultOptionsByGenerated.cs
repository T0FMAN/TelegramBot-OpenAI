using System.ComponentModel.DataAnnotations.Schema;

namespace TelegramBot_OpenAI.Models
{
    public class DefaultOptionsByGenerated
    {
        [ForeignKey(nameof(User))]
        public int IdGenerated { get; set; }
        public TelegramUser User { get; set; }
        public string? Prompt { get; set; }
        public DateTime DateGenerated { get; set; }
    }
}
