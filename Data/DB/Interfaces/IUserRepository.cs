using TelegramBot_OpenAI.Models;

namespace TelegramBot_OpenAI.Data.DB.Interfaces
{
    public interface IUserRepository
    {
        Task<TelegramUser?> GetById(long id, bool track);
        Task<bool> Add(TelegramUser user);
        Task<bool> Update(TelegramUser user);
        Task<bool> Save();
    }
}
