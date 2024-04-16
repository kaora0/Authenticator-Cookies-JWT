using Lab_7._1.Interfaces;

namespace Lab_7._1.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        //генерация
        public string Generate(string password)
            => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        //проверка
        public bool Verify(string password, string hashedPassword)
            => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }
}
