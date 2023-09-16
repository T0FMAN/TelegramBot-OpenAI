using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using TelegramBot_OpenAI.Configurations;
using TelegramBot_OpenAI.Controllers;
using TelegramBot_OpenAI.Data.DB;
using TelegramBot_OpenAI.Extensions;
using TelegramBot_OpenAI.Interfaces;
using TelegramBot_OpenAI.Repository;
using TelegramBot_OpenAI.Services;

var path = Environment.CurrentDirectory + "\\secrets.json";

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile(path);

var botConfigurationSection = builder.Configuration.GetSection(BotConfiguration.Configuration);
var connectionString = builder.Configuration.GetConnectionString("SqlServer");
var openAiToken = builder.Configuration.GetOpenAiToken();

builder.Services.Configure<BotConfiguration>(botConfigurationSection);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDbContext<AppDbContext>(options => 
{
    options.UseSqlServer(connectionString);
});

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