namespace TelegramBot_OpenAI.Configurations
{
    public class BotConfiguration
    {
        public static readonly string Configuration = "BotConfiguration";

        public string HttpClientName { get; init; } = default!;
        public string BotToken { get; init; } = default!;
        public string HostAddress { get; init; } = default!;
        public string Route { get; init; } = default!;
        public string SecretToken { get; init; } = default!;
    }
}
