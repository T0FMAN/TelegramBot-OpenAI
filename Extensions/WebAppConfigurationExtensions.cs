using TelegramBot_OpenAI.Configurations;

namespace TelegramBot_OpenAI.Extensions
{
    public static class WebAppConfigurationExtensions
    {
        /// <summary>
        /// Shorthand for GetSection(OpenAiToken)[value].
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns>The Open AI token.</returns>
        /// <exception cref="Exception"></exception>
        public static string GetOpenAiToken(this ConfigurationManager configuration)
        {
            var openAiToken = configuration.GetSection(SecretsConfiguration.OpenAiConfiguration).Value;

            return openAiToken ?? throw new Exception("Open AI token is empty..");
        }
    }
}
