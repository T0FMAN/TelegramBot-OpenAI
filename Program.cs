using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using TelegramBot_OpenAI.Configurations;
using TelegramBot_OpenAI.Controllers;
using TelegramBot_OpenAI.Extensions;
using TelegramBot_OpenAI.Services;
using TelegramBot_OpenAI.Services.Updates;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDbContext(
    connectionString: builder.Configuration.GetConnectionString("Main")!);

builder.Services.Configure<BotConfiguration>(
    config: builder.Configuration.GetSection(BotConfiguration.Configuration));

var botConfiguration = builder.Configuration.GetBotConfiguration();

builder.Services.AddHttpClient(botConfiguration.HttpClientName)
    .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
                {
                    var botConfig = sp.GetConfiguration<BotConfiguration>();
                    var options = new TelegramBotClientOptions(botConfig.BotToken);

                    return new TelegramBotClient(options, httpClient);
                });

builder.Services.AddScoped<UpdateHandlers>();
builder.Services.AddHostedService<ConfigureWebhook>();
builder.Services.AddControllers()
    .AddNewtonsoftJson();

var app = builder.Build();

app.MapBotWebhookRoute<BotController>(route: botConfiguration.Route);
app.MapControllers();

await app.RunAsync();
