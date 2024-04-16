using Lab_7._1.Data.Entities;
using Lab_7._1.Data.Models;

namespace Lab_7._1.Interfaces
{
    public interface IJwtProvider
    {
        string Generate(User user); // Изменение типа параметра
    }
}
