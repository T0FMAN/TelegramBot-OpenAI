using System.Configuration;
using TelegramBot_OpenAI.Configurations;

namespace TelegramBot_OpenAI.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetOpenAIToken(this IConfiguration configuration)
        {
            var openAIConfiguration = configuration
                .GetSection(OpenAiConfiguration.Configuration)
                .Get<OpenAiConfiguration>()
                ?? throw new ConfigurationErrorsException($"Section [{OpenAiConfiguration.Configuration}] in the configuration is null or empty");

            return openAIConfiguration.MyToken;
        }

        public static BotConfiguration GetBotConfiguration(this IConfiguration configuration)
        {
            var botConfigurationSection = configuration.GetSection(BotConfiguration.Configuration);
            var botConfiguration = botConfigurationSection.Get<BotConfiguration>()
                ?? throw new ConfigurationErrorsException($"Section [{BotConfiguration.Configuration}] in the configuration is null or empty");

            return botConfiguration;
        }
    }
}
