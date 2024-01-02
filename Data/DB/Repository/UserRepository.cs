using Microsoft.EntityFrameworkCore;
using TelegramBot_OpenAI.Data.DB.Interfaces;
using TelegramBot_OpenAI.Models;

namespace TelegramBot_OpenAI.Data.DB.Repository
{
    public class UserRepository(TelegramBot_DbContext context) : IUserRepository
    {
        private readonly TelegramBot_DbContext _context = context;

        public async Task<TelegramUser?> GetById(long id, bool track = true)
        {
            var user = track switch
            {
                true => await _context.Users.FirstOrDefaultAsync(x => x.TelegramId == id),
                false => await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.TelegramId == id)
            };

            return user;
        }

        public async Task Add(TelegramUser user)
        {
            await _context.Users.AddAsync(user);
        }

        public Task Update(TelegramUser user)
        {
            _context.Users.Update(user);

            return Task.CompletedTask;
        }
    }
}
