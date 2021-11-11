using AutoMapper;
using Core.Models;
using Infrastructure.Contexts;
using Infrastructure.DTOs;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace BookHub.Tests
{
    public class LoginControllerTests
    {
        private BookContext _context;
        private ILoginService _loginService;
        private IUserRepository _userRepository;
        private IMapper _mapper;
        private BookUser _user;
        [SetUp]
        public void Setup()
        {
            SetupContext();
            SetupUser();
            _userRepository = new UserRepository(_context);
            _mapper = new MapperConfiguration(c => c.AddProfile<DtoModule>()).CreateMapper();
            _loginService = new LoginService(_userRepository, _mapper);
        }

        [Test]
        public void LoginWithValidCredentials()
        {
            var loginResult = _loginService.LoginAsync(_user.Username, _user.Password).Result;
            Assert.True(loginResult.Successful == true &&
                        loginResult.User.Username == _user.Username);
            CleanupUser();
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            var loginResult = _loginService.LoginAsync($"User{_user.Username}", _user.Password).Result;
            Assert.True(loginResult.Successful == false &&
                        loginResult.User is null);
            CleanupUser();
        }

        private void SetupContext()
        {
            string startupPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName, @"BookHub\API");
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(startupPath)
                .AddJsonFile("appsettings.json")
                .Build();

            var options = new DbContextOptionsBuilder<BookContext>()
                .UseSqlServer(new SqlConnection(configuration.GetConnectionString("BookLoopConn")))
                .Options;

            _context = new BookContext(options);
            _context.Database.EnsureCreated();
        }

        private void SetupUser()
        {
            var user1 = new BookUser()
            {
                Username = "TestUser1", 
                Password = "test"
            };

            _context.BookUser.Add(user1);
            _context.SaveChanges();
            _user = _context.BookUser.ToList().Last();
        }

        private void CleanupUser()
        {
            _context.BookUser.Remove(_user);
            _context.SaveChanges();
        }
    }
}
