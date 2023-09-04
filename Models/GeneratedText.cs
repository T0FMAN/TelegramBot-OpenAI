using System.ComponentModel.DataAnnotations;

namespace TelegramBot_OpenAI.Models
{
    public class GeneratedText : DefaultOptionsByGenerated
    {
        [Key]
        public int IdText { get; set; }
        public string TextOutput { get; set; }
    }
}
