namespace TelegramBot_OpenAI.Configurations
{
    public class SecretsConfiguration // For json
    {
        public static readonly string OpenAiConfiguration = "OpenAiConfiguration";
        public OpenAiConfiguration OpenAiToken { get; init; } = default!;
        public BotConfiguration BotConfiguration { get; init; } = default!;
    }
}
