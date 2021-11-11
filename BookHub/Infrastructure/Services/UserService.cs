using AutoMapper;
using Core.Models;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using Infrastructure.Repositories;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotosUrlResolver _photosUrlResolver;

        public UserService(IUserRepository userRepository, IMapper mapper, IPhotosUrlResolver photosUrlResolver)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _photosUrlResolver = photosUrlResolver;
        }

        public async Task<BookUserDTO> GetUserInfoByIdAsync(int id)
        {
            var user = await _userRepository.SelectUserInformationByIdAsync(id);
            return MapUser(user);
        }

        public async Task<BookUserDTO> GetUserInfoByUsernameAsync(string username)
        {
            var user = await _userRepository.SelectUserInformationByUsernameAsync(username);
            return MapUser(user);
        }

        public async Task<bool> SaveProfileChangesAsync(BookUserDTO bookUserDTO)
        {
            var bookUser = _mapper.Map<BookUserDTO, BookUser>(bookUserDTO);
            bool emailInUse = await _userRepository.CheckEmailAlreadyInUse(bookUser);

            if (emailInUse)
            {
                return false;
            }

            var dbUser = await _userRepository.SelectByIdAsync(bookUser.Id);
            bookUser.Password = dbUser.Password;

            _userRepository.DetachLocal(bookUser);
            await _userRepository.UpdateAsync(bookUser);
            return true;
        }

        private BookUserDTO MapUser(BookUser user)
        {
            if (user is null)
            {
                return null;
            }

            _photosUrlResolver.ResolveUrl(user);
            user.BookPosts?.ForEach(bp => _photosUrlResolver.ResolveUrl(bp));

            return _mapper.Map<BookUser, BookUserDTO>(user);
        }
    }
}
