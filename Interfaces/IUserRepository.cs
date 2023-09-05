using TelegramBot_OpenAI.Models;

namespace TelegramBot_OpenAI.Interfaces
{
    public interface IUserRepository
    {
        Task<TelegramUser> GetById(long id);
        Task<bool> Add(TelegramUser user);
        Task<bool> Update(TelegramUser user);
        Task<bool> Save();
    }
}
