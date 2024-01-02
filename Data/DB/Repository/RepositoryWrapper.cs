using TelegramBot_OpenAI.Data.DB.Interfaces;

namespace TelegramBot_OpenAI.Data.DB.Repository
{
    public class RepositoryWrapper(
        TelegramBot_DbContext context,
        ILogger<RepositoryWrapper> logger) : IRepositoryWrapper
    {
        private readonly ILogger<RepositoryWrapper> _logger = logger;
        private readonly TelegramBot_DbContext _context = context;
        private IUserRepository _userRepository = default!;

        public IUserRepository UserRepository
        {
            get
            {
                _userRepository ??= new UserRepository(_context);

                return _userRepository;
            }
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();

            _logger.LogInformation("Saved {Count} context items", saved);

            return saved > 0;
        }
    }
}
