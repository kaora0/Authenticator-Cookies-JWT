﻿using Lab_7._1.Data.Models;
using Lab_7._1.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Lab_7._1.Services
{
    public class UserService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public UserService(
            IUsersRepository usersRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task Register(string userName, string email, string password)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = User.Create(
                Guid.NewGuid(),
                userName,
                hashedPassword,
                email);

            await _usersRepository.Add(user);
        }

        public async Task<string> Login(string email, string password)
        {
            //возвращение юзера по почте
            var user = await _usersRepository.GetByEmail(email);

            //проверка пароля
            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = _jwtProvider.Generate(user);

            return token;
        }
    }
}
