using System.Configuration;
using TelegramBot_OpenAI.Configurations;

namespace TelegramBot_OpenAI.Extensions
{
    public static class WebAppConfigurationExtensions
    {
        /// <summary>
        /// Shorthand for GetSection(OpenAIToken)[value].
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns>The Open AI token.</returns>
        /// <exception cref="ConfigurationErrorsException"></exception>
        public static string GetOpenAIToken(this IConfiguration configuration)
        {
            var openAIConfiguration = configuration.GetSection(SecretsConfiguration.OpenAiConfiguration)
                                                   .Get<OpenAiConfiguration>()
                                                   ?? throw new ConfigurationErrorsException("Ope");

            return openAIConfiguration.MyToken;
        }
    }
}
