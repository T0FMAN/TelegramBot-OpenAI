using Telegram.Bot;

namespace TelegramBot_OpenAI.Extensions
{
    public static class TelegramExtenisons
    {
        public static async Task ExceptionActionWithDataBase(ITelegramBotClient botClient, long chatId, string table, Data.Enums.TableOperation operation, CancellationToken cancellationToken)
        {
            string messageText = "Something went wrong. Contact support @support";

            await botClient.SendTextMessageAsync(chatId: chatId, 
                                                 text: messageText,
                                                 cancellationToken: cancellationToken);

            throw new Exception($"An error occurred with the operation of '{operation}' to the table '{table}'");
        }
    }
}
