using TelegramBot_OpenAI.Data.Enums;

namespace TelegramBot_OpenAI.Helpers
{
    public static class EnumsHelper
    {
        public static bool SearchValueAtLanguageInterfaceDictionary(string value, out LanguageInterface result)
        {
            var languageInterfaceDictionary = new Dictionary<LanguageInterface, string>()
            {
                    { LanguageInterface.ES, "Spanish" },
                    { LanguageInterface.EN, "English" },
                    { LanguageInterface.RU, "Русский" }
            };

            result = default;

            foreach (var pair in languageInterfaceDictionary)
            {
                if (pair.Value == value)
                {
                    result = pair.Key;
                    return true;
                }
            }

            return false;
        }
    }
}
