using Telegram.Bot;
using TelegramBot_OpenAI.Controllers;
using TelegramBot_OpenAI.Services;
using TelegramBot_OpenAI.Configurations;
using TelegramBot_OpenAI.Extensions;
using TelegramBot_OpenAI.Data.Enums;

var path = Environment.CurrentDirectory + "\\secrets.json";
var set = await path.SetEnvVariablesFromFile(FileType.JSON);
if (!set) return;

var builder = WebApplication.CreateBuilder(args);

var botConfigurationSection = builder.Configuration.GetSection(BotConfiguration.Configuration);
builder.Services.Configure<BotConfiguration>(botConfigurationSection);

var botConfiguration = botConfigurationSection.Get<BotConfiguration>();

builder.Services.AddHttpClient("telegram_bot_client")
                .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                {
                    BotConfiguration? botConfig = sp.GetConfiguration<BotConfiguration>();
                    TelegramBotClientOptions options = new(botConfig.BotToken);
                    return new TelegramBotClient(options, httpClient);
                });
builder.Services.AddScoped<UpdateHandlers>();
builder.Services.AddHostedService<ConfigureWebhook>();
builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

var app = builder.Build();
app.MapBotWebhookRoute<BotController>(route: botConfiguration.Route);
app.MapControllers();
app.Run();