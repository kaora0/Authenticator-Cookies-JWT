using Lab_7._1.Data.Models;

namespace Lab_7._1.Interfaces
{
    public interface IUsersRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
    }
}
