using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot_OpenAI.Data.DB.Interfaces;
using TelegramBot_OpenAI.Data.Enums;
using TelegramBot_OpenAI.Extensions;
using TelegramBot_OpenAI.Models;

namespace TelegramBot_OpenAI.Helpers
{
    public static class TelegramMessagesHelper
    {
        public static async Task<Message> MessageByActionWithTableUsers(TableOperation operation, TelegramUser user, string text, IUserRepository userRepository, ITelegramBotClient botClient, CancellationToken cancellationToken, ParseMode? parseMode = null, IReplyMarkup? replyMarkup = null)
        {
            Task<bool> action;

            action = operation switch
            {
                TableOperation.add => userRepository.Add(user),
                TableOperation.update => userRepository.Update(user),
                TableOperation.remove => throw new NotImplementedException(),
                TableOperation.save => throw new NotImplementedException(),
                _ => throw new Exception($"Not support at enum {nameof(TableOperation)}"), // ......damn
            };

            var complete = await action;

            if (!complete)
                await ExceptionActionWithDataBase(botClient: botClient,
                                                  chatId: user.TelegramId,
                                                  table: nameof(user).ToRightNameOfTable(),
                                                  operation: operation,
                                                  cancellationToken: cancellationToken);

            return await botClient.SendTextMessageAsync(chatId: user.TelegramId,
                                                        text: text,
                                                        replyMarkup: replyMarkup,
                                                        parseMode: parseMode,
                                                        cancellationToken: cancellationToken);
        }

        public static async Task ExceptionActionWithDataBase(ITelegramBotClient botClient, long chatId, string table, TableOperation operation, CancellationToken cancellationToken)
        {
            string messageText = "Something went wrong. Contact support @support";

            await botClient.SendTextMessageAsync(chatId: chatId,
                                                 text: messageText,
                                                 cancellationToken: cancellationToken);

            throw new Exception($"An error occurred with the operation of '{operation}' to the table '{table}'");
        }
    }
}
