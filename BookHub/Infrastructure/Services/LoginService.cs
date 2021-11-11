using AutoMapper;
using Core.Models;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LoginService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<LoginResult> LoginAsync(string username, string password)
        {
            var user = await _userRepository.SelectUserWithLoginInfoAsync(username, password);

            if(user != null)
            {
                return new LoginResult()
                {
                    Successful = true,
                    User = _mapper.Map<BookUser, BookUserDTO>(user)
                };
            }

            return new LoginResult()
            {
                Successful = false,
                User = null
            };
        }
    }
}
