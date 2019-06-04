using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PixBlocks_Addition.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;

        public UserService(IUserRepository userRepository, IEncrypter encrypter)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
        }

        public async Task ChangeEmail(string login, string email)
        {
            var user = await _userRepository.GetAsync(login);
            var unemail = await _userRepository.IsEmailUnique(email);

            if (user.Email == email) throw new MyException(MyCodesNumbers.SameEmail, MyCodes.SameEmail);
            if (!unemail) throw new MyException(MyCodesNumbers.UniqueEmail, MyCodes.UniqueEmail);

            user.SetEmail(email);

            await _userRepository.UpdateAsync(user);
        }

        public async Task ChangePassword(string login, string newPassword, string oldPassword)
        {
            var user = await _userRepository.GetAsync(login);
            if (newPassword == oldPassword) throw new MyException(MyCodesNumbers.SamePassword, MyCodes.SamePassword);

            user.SetPassword(newPassword, _encrypter);

            await _userRepository.UpdateAsync(user);
        }

    }
}
