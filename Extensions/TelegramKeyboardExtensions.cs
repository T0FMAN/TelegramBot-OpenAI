using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot_OpenAI.Extensions
{
    public static class TelegramKeyboardExtensions
    {
        public readonly static ReplyKeyboardMarkup Menu =
            new(
                new[]
                {
                    new KeyboardButton[] { "👤Profile", "🛠Generate!" },
                    new KeyboardButton[] { "©️About bot", "My referals" },
                })
            {
                ResizeKeyboard = true
            };

        public readonly static InlineKeyboardMarkup ChooseWhatGenerate = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🖼Photo", $"photo"),
                            InlineKeyboardButton.WithCallbackData("📝Text", $"text"),
                        },
                    });
    }
}
