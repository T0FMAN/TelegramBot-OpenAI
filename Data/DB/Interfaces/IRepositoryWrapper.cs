namespace TelegramBot_OpenAI.Data.DB.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository UserRepository { get; }
        Task<bool> SaveAsync();
    }
}
