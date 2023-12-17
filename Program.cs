using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using TelegramBot_OpenAI.Configurations;
using TelegramBot_OpenAI.Controllers;
using TelegramBot_OpenAI.Data.DB;
using TelegramBot_OpenAI.Data.DB.Interfaces;
using TelegramBot_OpenAI.Data.DB.Repository;
using TelegramBot_OpenAI.Services;
using TelegramBot_OpenAI.Services.Updates;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TelegramBotOpenAI_DbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("SqlServer");

    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IUserRepository, UserRepository>();

var botConfigurationSection = builder.Configuration.GetSection(BotConfiguration.Configuration);
var botConfiguration = botConfigurationSection.Get<BotConfiguration>()!;
builder.Services.Configure<BotConfiguration>(botConfigurationSection);

builder.Services.AddHttpClient("telegram_bot_client")
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