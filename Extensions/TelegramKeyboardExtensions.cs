using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot_OpenAI.Extensions
{
    public static class TelegramKeyboardExtensions
    {
        public readonly static ReplyKeyboardMarkup Menu =
            new(
                new[]
                {
                    new KeyboardButton[] { "👤My profile" },
                    new KeyboardButton[] { "©️About bot", "My referals" },
                })
            {
                ResizeKeyboard = true
            };
    }
}
