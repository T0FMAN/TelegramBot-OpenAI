using System.ComponentModel.DataAnnotations;

namespace TelegramBot_OpenAI.Models
{
    public class GeneratedImage : DefaultOptionsByGenerated
    {
        [Key]
        public int ImageId { get; set; }
    }
}
