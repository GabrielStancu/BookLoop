using AutoMapper;
using Core.Models;
using Infrastructure.Contexts;
using Infrastructure.DTOs;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace BookHub.Tests
{
    public class MessageRepositoryTests
    {
        private BookContext _context;
        private IMessageRepository _messageRepository;
        private IMapper _mapper;
        private List<PrivateConversation> _conversations;
        private List<Message> _messages;
        private List<BookUser> _users;

        [SetUp]
        public void Setup()
        {
            SetupContext();
            SetupUsers();
            _messageRepository = new MessageRepository(_context);
            _mapper = new MapperConfiguration(c => c.AddProfile<DtoModule>()).CreateMapper();
        }

        [Test]
        public void GetLastMessageForUserConversations()
        {
            int conversationsNumber = 4, messagesPerConversation = 5;
            CreateUserConversations(conversationsNumber, messagesPerConversation);
            var dbLastMessages = new List<Message>(_messageRepository.GetLastMessagesForUserAsync(_conversations).Result);
            var expectedLastMessages = new List<Message>();

            for(int conversationIndex = 0; conversationIndex < conversationsNumber; conversationIndex++)
            {
                var conversation = _conversations[conversationIndex];
                expectedLastMessages.Add(new Message()
                {
                    Id = _messages[(conversationIndex + 1) * messagesPerConversation - 1].Id,
                    ConversationId = conversation.Id, 
                    ConversationType = conversation.ConversationType,
                    MessageContent = $"Test {conversation.ConversationType}/{conversation.Id}/{messagesPerConversation - 1}",
                    SendTime = DateTime.Today,
                    Sender = _users[1 - messagesPerConversation % 2],
                    SenderId = _users[1 - messagesPerConversation % 2].Id
                });
            }
            CollectionAssert.AreEqual(expectedLastMessages, dbLastMessages, new MessageComparer());

            CleanupMessages();
            CleanupUsers();
        }

        [Test]
        public void GetMessagesForUserConversation()
        {
            int conversationsNumber = 1, messagesPerConversation = 5;
            CreateUserConversations(conversationsNumber, messagesPerConversation);
            var conversationDTO = _mapper.Map<Conversation, ConversationDTO>(_conversations[0]);
            var dbMessages = new List<Message>(_messageRepository.GetMessagesFromConversationAsync(conversationDTO).Result);
            var expectedMessages = new List<Message>();

            for (int messageIndex = 0; messageIndex < messagesPerConversation; messageIndex++)
            {
                var conversation = _conversations[0];
                expectedMessages.Add(new Message()
                {
                    Id = _messages[messageIndex].Id,
                    ConversationId = conversation.Id,
                    ConversationType = conversation.ConversationType,
                    MessageContent = $"Test {conversation.ConversationType}/{conversation.Id}/{messageIndex}",
                    SendTime = DateTime.Today,
                    Sender = _users[messageIndex % 2],
                    SenderId = _users[messageIndex % 2].Id
                });
            }
            CollectionAssert.AreEqual(expectedMessages, dbMessages, new MessageComparer());

            CleanupMessages();
            CleanupUsers();
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

        private void SetupUsers()
        {
            _users = new List<BookUser>();
            var user1 = new BookUser()
            {
                FirstName = "Test",
                LastName = $"User 1",
                Username = "TestUser1"
            };

            var user2 = new BookUser()
            {
                FirstName = "Test",
                LastName = $"User 2",
                Username = "TestUser2"
            };

            _context.BookUser.Add(user1);
            _context.SaveChanges();
            user1 = _context.BookUser.ToList().Last();

            _context.BookUser.Add(user2);
            _context.SaveChanges();
            user2 = _context.BookUser.ToList().Last();

            _users.Add(user1);
            _users.Add(user2);
        }

        private void CreateUserConversations(int conversationsNumber, int messagesPerConversation)
        {
            _conversations = new List<PrivateConversation>();
            _messages = new List<Message>();
            for(int conversationIndex = 0; conversationIndex < conversationsNumber; conversationIndex++)
            {
                var conversation = new PrivateConversation()
                {
                    ConversationType = ConversationType.Private,
                    User1Id = _users[0].Id,
                    User2Id = _users[1].Id
                };
                _context.PrivateConversation.Add(conversation);
                _context.SaveChanges();
                conversation = _context.PrivateConversation.ToList().Last();
                _conversations.Add(conversation);

                for(int messageIndex = 0; messageIndex < messagesPerConversation; messageIndex++ )
                {
                    var message = new Message()
                    {
                        ConversationId = conversation.Id,
                        ConversationType = conversation.ConversationType,
                        SenderId = _users[messageIndex % 2].Id,
                        MessageContent = $"Test {conversation.ConversationType}/{conversation.Id}/{messageIndex}",
                        SendTime = DateTime.Today
                    };

                    _context.Message.Add(message);
                    _context.SaveChanges();
                    message = _context.Message.ToList().Last();
                    _messages.Add(message);
                }
            }
        }

        private void CleanupMessages()
        {
            _context.Message.RemoveRange(_messages);
            _context.SaveChanges();
        }

        private void CleanupUsers()
        {
            _context.BookUser.RemoveRange(_users);
            _context.SaveChanges();
        }
    }
}