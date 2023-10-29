using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot_OpenAI.Data.Enums;

namespace TelegramBot_OpenAI
{
    public static class TelegramKeyboards
    {
        /// <summary>
        /// Method for get reply keyboards
        /// </summary>
        /// <param name="named"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static ReplyKeyboardMarkup GetReplyKeyboardMarkup(ReplyKeyboards named)
        {
            return named switch
            {
                ReplyKeyboards.LanguageInterface => LanguageInterface,
                ReplyKeyboards.Menu => Menu,
                ReplyKeyboards.ModelsChatGPT => ModelsChatGPT,
                _ => throw new Exception(),
            };
        }

        /// <summary>
        /// Method for get inline keyboards
        /// </summary>
        /// <param name="named"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static InlineKeyboardMarkup GetInlineKeyboardMarkup(InlineKeyboards named, string[]? parameters = null)
        {
            return named switch
            {
                InlineKeyboards.ChooseWhatGenerate => ChooseWhatGenerate,
                _ => throw new Exception(),
            };
        }

        #region ReplyKeyboards
        readonly static ReplyKeyboardMarkup LanguageInterface =
            new(
                new[]
                {
                    new KeyboardButton[] { "English 🇬🇧🇺🇸", "Russian 🇷🇺" },
                })
            {
                ResizeKeyboard = true
            };

        readonly static ReplyKeyboardMarkup Menu =
            new(
                new[]
                {
                    new KeyboardButton[] { "👤Profile", "🛠Generate!" },
                    new KeyboardButton[] { "©️About bot", "My referals" },
                })
            {
                ResizeKeyboard = true
            };

        readonly static ReplyKeyboardMarkup ModelsChatGPT =
            new(
                new[]
                {
                    new KeyboardButton[] { "GPT V3_5 (free)" },
                    new KeyboardButton[] { "GPT V4" },
                })
            {
                OneTimeKeyboard = true,
                ResizeKeyboard = true
            };
        #endregion

        #region InlineKeyboards
        readonly static InlineKeyboardMarkup ChooseWhatGenerate = new(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🖼Photo", "photo"),
                            InlineKeyboardButton.WithCallbackData("📝Text", "text"),
                        },
                    });
        #endregion
    }
}
