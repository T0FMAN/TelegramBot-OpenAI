using TelegramBot_OpenAI.Data.DB.Interfaces;

namespace TelegramBot_OpenAI.Data.DB.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private TelegramBot_DbContext _context;
        private IUserRepository _userRepository = default!;
        private readonly ILogger<RepositoryWrapper> _logger;

        public RepositoryWrapper(TelegramBot_DbContext context,
            ILogger<RepositoryWrapper> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }
                return _userRepository;
            }
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();

            return saved > 0;
        }
    }
}
