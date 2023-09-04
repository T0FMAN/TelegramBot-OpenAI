using System.ComponentModel.DataAnnotations;

namespace TelegramBot_OpenAI.Models
{
    public class GeneratedImage : DefaultOptionsByGenerated
    {
        [Key]
        public int IdImage { get; set; }
    }
}
