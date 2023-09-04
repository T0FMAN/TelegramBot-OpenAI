using TelegramBot_OpenAI.Models;

namespace TelegramBot_OpenAI.Interfaces
{
    public interface IUserRepository
    {
        Task<TelegramUser> GetById(long id);
        bool Add(TelegramUser user);
        bool Update(TelegramUser user);
        bool Save();
    }
}
