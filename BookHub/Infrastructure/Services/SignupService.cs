using AutoMapper;
using Core.Models;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SignupService : ISignupService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public SignupService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<SignupResult> SignupAsync(SignupUserDTO signupUserDTO)
        {
            bool existsAlready = await _userRepository.CheckAlreadyRegisteredUserAsync(signupUserDTO);

            if (!existsAlready)
            {
                return await CheckCredentials(signupUserDTO);
            }

            return SignupResult.UserAlreadyRegistered;
        }

        private async Task<SignupResult> CheckCredentials(SignupUserDTO signupUser)
        {
            if (!AllFieldsValid(signupUser))
            {
                return SignupResult.BadCredentials;
            }

            var mappedUser = _mapper.Map<SignupUserDTO, BookUser>(signupUser);
            await _userRepository.InsertAsync(mappedUser);
            return SignupResult.Registered;
        }

        private bool AllFieldsValid(SignupUserDTO signupUser)
        {
            if(signupUser.Username.Equals(string.Empty)
                || signupUser.Password.Equals(string.Empty)
                || signupUser.FirstName.Equals(string.Empty)
                || signupUser.LastName.Equals(string.Empty)
                || signupUser.Email.Equals(string.Empty)
                || !signupUser.Password.Equals(signupUser.RepeatedPassword))
            {
                return false;
            }

            return true;
        }
    }
}
