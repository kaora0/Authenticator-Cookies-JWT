using Lab_7._1.Data.Models;
using Lab_7._1.Data.Entities;
using Lab_7._1.Data;
using Lab_7._1.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Lab_7.Repositories
{

    public class UsersRepository : IUsersRepository
    {
        private readonly LearningDbContext _context;
        private readonly IMapper _mapper;

        public UsersRepository(LearningDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(User user)
        {
            var userEntity = new UserEntity()
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Email = user.Email
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();

            return _mapper.Map<User>(userEntity);
        }
    }
}
