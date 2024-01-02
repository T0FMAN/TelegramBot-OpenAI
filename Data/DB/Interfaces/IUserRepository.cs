using TelegramBot_OpenAI.Models;

namespace TelegramBot_OpenAI.Data.DB.Interfaces
{
    public interface IUserRepository
    {
        Task<TelegramUser?> GetById(long id, bool track);
        Task Add(TelegramUser user);
        Task Update(TelegramUser user);
    }
}
