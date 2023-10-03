﻿using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot_OpenAI.Data.DB.Interfaces;
using TelegramBot_OpenAI.Data.Enums;
using TelegramBot_OpenAI.Models;
using static TelegramBot_OpenAI.Helpers.TelegramMessagesHelper;

namespace TelegramBot_OpenAI.Services.Updates
{
    public partial class UpdateHandlers // In this part - messages update
    {
        private async Task BotOnMessageReceived(Message message, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Receive message type: {message.Type}", message.Type);
            if (message.Text is not { } messageText)
                return;

            try
            {
                Task<Message> action;

                if (_user is null)
                    action = FirstLaunchCommand(_userRepository, _botClient, message, cancellationToken);
                else if (!_user.IsReg)
                    action = Regestration(_user, _userRepository, _botClient, message, cancellationToken);
                else
                {
                    action = messageText.Split(' ')[0] switch
                    {
                        "/me" or "👤Profile" => AboutMeCommand(_user, _botClient, message, cancellationToken),
                        "/generate" or "🛠Generate!" => StartGenerateCommand(_user, _userRepository, _botClient, message, cancellationToken),
                        _ => Usage(_user, _userRepository, _botClient, message, cancellationToken)
                    };
                }

                Message sentMessage = await action;
                _logger.LogInformation("The message was sent with id: {messageId}", sentMessage.MessageId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            static async Task<Message> FirstLaunchCommand(IUserRepository userRepository, ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
            {
                var user = new TelegramUser(telegramId: message.Chat.Id,
                                            userName: message.Chat.Username,
                                            bio: message.Chat.Bio,
                                            dateTime: DateTime.UtcNow,
                                            userAction: UserAction.Regestration);

                return await MessageByActionWithTableUsers(operation: TableOperation.add,
                                                           user: user,
                                                           text: "Hello! This is Chat-GPT bot.\n\n" +
                                                                 "To continue working with the bot, you need to enter your age (18 - 100)",
                                                           userRepository: userRepository,
                                                           botClient: botClient,
                                                           cancellationToken: cancellationToken);
            }

            static async Task<Message> Regestration(TelegramUser user, IUserRepository userRepository, ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
            {
                var isAge = int.TryParse(message.Text, out int age);

                if (!isAge || age > 100 || age < 18)
                    return await botClient.SendTextMessageAsync(chatId: message.Chat.Id, 
                                                                text: "You entered an incorrect age\n\n" +
                                                                      "You must enter the age from 18 to 100",
                                                                cancellationToken: cancellationToken);

                user.RegestrationDate = DateTime.UtcNow;
                user.IsReg = true;
                user.Age = age;

                var keyboard = TelegramKeyboards.GetReplyKeyboardMarkup(ReplyKeyboards.Menu);

                return await MessageByActionWithTableUsers(operation: TableOperation.update,
                                                           user: user,
                                                           text: "You have successfully registered!\n\n" +
                                                                 "Check out the functionality",
                                                           userRepository: userRepository,
                                                           botClient: botClient,
                                                           replyMarkup: keyboard,
                                                           cancellationToken: cancellationToken);
            }

            static async Task<Message> AboutMeCommand(TelegramUser user, ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
            {
                var text = $"Regestration date: {user.RegestrationDate}\n" +
                           $"Account balance: {user.AccountBalance}$\n" +
                           $"Count generations: {user.CountGenerations}";

                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: text,
                                                            cancellationToken: cancellationToken);
            }

            static async Task<Message> StartGenerateCommand(TelegramUser user, IUserRepository userRepository, ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
            {
                var text = string.Empty;
                IReplyMarkup keyboard; 

                if (user.CountGenerations == 0)
                {
                    user.UserAction = UserAction.ChooseModel;
                    text = "Choose model\n\n P.S\n" +
                           "This is a <b>one-time</b> action, since you have not yet set the model to automatically select the generation model.\n" +
                           "At any time, you can cancel the selection and change it in the profile settings)";
                    keyboard = TelegramKeyboards.GetReplyKeyboardMarkup(ReplyKeyboards.ModelsChatGPT);
                }
                else
                {
                    user.UserAction = UserAction.ChooseWhatGenerate;
                    text = "Choose what you want to generate";
                    keyboard = TelegramKeyboards.GetInlineKeyboardMarkup(InlineKeyboards.ChooseWhatGenerate);
                }

                return await MessageByActionWithTableUsers(operation: TableOperation.update,
                                                           user: user,
                                                           text: text,
                                                           userRepository: userRepository,
                                                           botClient: botClient,
                                                           cancellationToken: cancellationToken,
                                                           parseMode: ParseMode.Html,
                                                           replyMarkup: keyboard);
            }

            static async Task<Message> Usage(TelegramUser user, IUserRepository repository, ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
            {
                var messageParams = message.Text.Split(' ');

                switch (user.UserAction)
                {
                    case UserAction.ChooseModel:
                        {
                            if (messageParams.Length < 2)
                                goto default;

                            if (Enum.TryParse<ModelChatGPT>(message.Text.Split(' ')[1], out var model))
                            {
                                user.DefaultModelChatGPT = model;
                                user.UserAction = UserAction.GeneratePhoto;

                                return await MessageByActionWithTableUsers(operation: TableOperation.update, 
                                                                           user: user,
                                                                           text: $"Great! Now your default model is GPT-{model}",
                                                                           userRepository: repository,
                                                                           botClient: botClient,
                                                                           cancellationToken: cancellationToken);
                            }
                            else goto default;
                        }
                    case UserAction.ChooseWhatGenerate:
                        {
                            return await botClient.SendTextMessageAsync(user.TelegramId, "");
                        }
                    default: return await DefaultCaseMessage(chatId: message.Chat.Id,
                                                             botClient: botClient);
                }
            }

            static async Task<Message> DefaultCaseMessage(long chatId, ITelegramBotClient botClient)
            {
                return await botClient.SendTextMessageAsync(chatId: chatId, 
                                                            text: "Undefined message or parameters in message for current action.\n" +
                                                                  "If you want to return to the menu, write /menu.\n" +
                                                                  "For restart bot - /restart (or /start)");
            }
        }
    }
}
