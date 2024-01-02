using System.Configuration;
using TelegramBot_OpenAI.Configurations;

namespace TelegramBot_OpenAI.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetOpenAIToken(this IConfiguration configuration)
        {
            var openAIConfiguration = configuration.GetSection(SecretsConfiguration.OpenAiConfiguration)
                                                   .Get<OpenAiConfiguration>()
                                                   ?? throw new ConfigurationErrorsException("Open AI TOKEN NOT CONTAINS IN JSON");

            return openAIConfiguration.MyToken;
        }
    }
}
