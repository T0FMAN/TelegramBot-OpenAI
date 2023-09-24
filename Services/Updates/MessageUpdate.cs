using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot_OpenAI.Data.DB.Interfaces;
using TelegramBot_OpenAI.Data.Enums;
using TelegramBot_OpenAI.Extensions;
using TelegramBot_OpenAI.Models;

namespace TelegramBot_OpenAI.Services
{
    public partial class UpdateHandlers // Message update
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
                else if (_user.IsReg is false)
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
                
                var add = await userRepository.Add(user);

                if (!add)
                    await TelegramExtenisons.ExceptionActionWithDataBase(
                        botClient: botClient, 
                        chatId: message.Chat.Id, 
                        table: nameof(user).ToRightNameOfTable(), 
                        operation: TableOperation.add,
                        cancellationToken: cancellationToken);

                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id, 
                                                            text: "Hello! This is Chat-GPT bot.\n\n" +
                                                                  "To continue working with the bot, you need to enter your age (18 - 100)",
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

                var update = await userRepository.Update(user);

                if (!update)
                    await TelegramExtenisons.ExceptionActionWithDataBase(
                        botClient: botClient,
                        chatId: message.Chat.Id,
                        table: nameof(user).ToRightNameOfTable(),
                        operation: TableOperation.update,
                        cancellationToken: cancellationToken);

                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id, 
                                                            text: "You have successfully registered!\n\n" +
                                                                  "Check out the functionality",
                                                            replyMarkup: TelegramKeyboardExtensions.Menu,
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
                if (user.CountGenerations == 0)
                {
                    user.UserAction = UserAction.ChooseModel;

                    var updated = await userRepository.Update(user);

                    return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                                text: "Choose model\n\n P.S\n" +
                                                                      "This is a <b>one-time</b> action, since you have not yet set the model to automatically select the generation model.\n" +
                                                                      "At any time, you can cancel the selection and change it in the profile settings)",
                                                                parseMode: ParseMode.Html,
                                                                cancellationToken: cancellationToken);
                }
                else
                {
                    user.UserAction = UserAction.ChooseWhatGenerate;

                    var updated = await userRepository.Update(user);

                    return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                                text: "Choose what you want to generate",
                                                                replyMarkup: TelegramKeyboardExtensions.ChooseWhatGenerate,
                                                                cancellationToken: cancellationToken);
                }
            }

            static async Task<Message> Usage(TelegramUser user, IUserRepository repository, ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
            {
                string answer;

                if (user is null)
                {
                    answer = "Its your first launch";

                    var telegramId = message.Chat.Id;
                    var userName = message.Chat.Username;
                    var bio = message.Chat.Bio;
                    var lastActionDate = DateTime.Now.ToUniversalTime();
                    var userAction = UserAction.Regestration;

                    var isReg = await repository.Add(new TelegramUser(telegramId, userName, bio, lastActionDate, userAction));

                    await botClient.SendTextMessageAsync(message.Chat.Id, isReg.ToString());
                }
                else if (user.IsReg)
                    answer = "You are reg";
                else answer = "You are not reg";

                return await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: answer,
                    replyMarkup: new ReplyKeyboardRemove(),
                    cancellationToken: cancellationToken);
            }
        }
    }
}
