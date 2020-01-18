using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace PixBlocks_Addition.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly ILocalizationService _localization;

        public UserService(IUserRepository userRepository, IEncrypter encrypter, ILocalizationService localization)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _localization = localization;
        }

        public async Task ChangeEmail(string login, string email)
        {
            string file = $"Resources\\MyExceptions.{_localization.Language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            var user = await _userRepository.GetAsync(login);
            var unemail = await _userRepository.IsEmailUnique(email);

            if (user.Email == email) throw new MyException(MyCodesNumbers.SameEmail, doc.SelectSingleNode($"exceptions/SameEmail").InnerText);
            if (!unemail) throw new MyException(MyCodesNumbers.UniqueEmail, doc.SelectSingleNode($"exceptions/UniqueEmail").InnerText);

            user.SetEmail(email);

            await _userRepository.UpdateAsync(user);
        }

        public async Task ChangePassword(string login, string newPassword, string oldPassword)
        {
            string file = $"Resources\\MyExceptions.{_localization.Language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            var user = await _userRepository.GetAsync(login);
            if (newPassword == oldPassword) throw new MyException(MyCodesNumbers.SamePassword, doc.SelectSingleNode($"exceptions/SamePassword").InnerText);

            user.SetPassword(newPassword, _encrypter);

            await _userRepository.UpdateAsync(user);
        }

        public async Task SetPremium(string login)
        {
            var user = await _userRepository.GetAsync(login);

            user.SetPremium(true);

            await _userRepository.UpdateAsync(user);
        }

        public async Task TakePremium(string login)
        {
            var user = await _userRepository.GetAsync(login);

            user.SetPremium(false);

            await _userRepository.UpdateAsync(user);
        }
    }
}
