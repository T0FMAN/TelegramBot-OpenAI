using TelegramBot_OpenAI.Data.Enums;

namespace TelegramBot_OpenAI.Helpers
{
    public static class EnumsHelper
    {
        public static bool SearchValueAtLanguageInterfaceDictionary(string value, out LanguageInterface result)
        {
            result = default;

            foreach (var pair in LanguageInterfaceDictionary)
            {
                if (pair.Value == value)
                {
                    result = pair.Key;
                    return true;
                }
            }

            return false;
        }

        static Dictionary<LanguageInterface, string> LanguageInterfaceDictionary = new()
        {
                { LanguageInterface.ES, "Spanish" },
                { LanguageInterface.EN, "English" },
                { LanguageInterface.RU, "Русский" }
        };
    }
}
