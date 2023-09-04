using Telegram.Bot.Types;
using TelegramBot_OpenAI.Data.DB;
using TelegramBot_OpenAI.Interfaces;
using TelegramBot_OpenAI.Models;

namespace TelegramBot_OpenAI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        async Task<TelegramUser> IUserRepository.GetById(long id)
        {
            var user = await _context.Users.FindAsync(id);

            return user;
        }

        bool IUserRepository.Add(TelegramUser user)
        {
            throw new NotImplementedException();
        }


        bool IUserRepository.Save()
        {
            throw new NotImplementedException();
        }

        bool IUserRepository.Update(TelegramUser user)
        {
            throw new NotImplementedException();
        }
    }
}
