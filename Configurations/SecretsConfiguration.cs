namespace TelegramBot_OpenAI.Configurations
{
    public class SecretsConfiguration // For json
    {
        public static readonly string OpenAiConfiguration = "OpenAiToken";
        public string OpenAiToken { get; init; } = default!;
        public BotConfiguration BotConfiguration { get; init; }
    }
}
