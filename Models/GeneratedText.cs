using System.ComponentModel.DataAnnotations;

namespace TelegramBot_OpenAI.Models
{
    public class GeneratedText : DefaultOptionsByGenerated
    {
        [Key]
        public int TextId { get; set; }
        public string TextOutput { get; set; }
    }
}
