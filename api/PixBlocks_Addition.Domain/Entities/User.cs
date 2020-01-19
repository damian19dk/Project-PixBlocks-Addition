using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace PixBlocks_Addition.Domain.Entities
{
    public class User
    {
        private static readonly Regex regex_login = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");
        private static readonly Regex regex_mail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        public Guid Id { get; protected set; }
        public string Login { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public int RoleId { get; protected set; }
        public bool IsPremium { get; protected set; }

        public int Status { get; protected set; }

        public virtual Role Role { get; protected set; }

        protected User() { }
      
        public User(string login, string e_mail, string password, Role role, IEncrypter encrypter, string localization)
        {
            Id = Guid.NewGuid();
            SetLogin(login, localization);
            SetEmail(e_mail, localization);
            SetRole(role.Id, localization);
            SetPassword(password, encrypter, localization);
        }

        public User(string login, string e_mail, int role, string password, IEncrypter encrypter, string localization)
        {
            Id = Guid.NewGuid();
            SetLogin(login, localization);
            SetEmail(e_mail, localization);
            SetRole(role, localization);
            SetPassword(password, encrypter, localization);
        }

        public void SetLogin(string login, string language)
        {
            string file = $"Resources\\MyExceptions.{language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            if (login.Length < 3) throw new MyException(MyCodesNumbers.TooShortLogin, doc.SelectSingleNode($"exceptions/TooShortLogin").InnerText);
            if (login.Length >= 20) throw new MyException(MyCodesNumbers.TooLongLogin, doc.SelectSingleNode($"exceptions/TooLongLogin").InnerText);
            if (String.IsNullOrEmpty(login)) throw new MyException(MyCodesNumbers.WrongCharactersInLogin, doc.SelectSingleNode($"exceptions/WrongCharactersInLogin").InnerText);
            if (!regex_login.IsMatch(login)) throw new MyException(MyCodesNumbers.WrongCharactersInLogin, doc.SelectSingleNode($"exceptions/WrongCharactersInLogin").InnerText);
            Login = login;
        }

        public void SetPassword(string password, IEncrypter encrypter, string language)
        {
            string file = $"Resources\\MyExceptions.{language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            if (string.IsNullOrWhiteSpace(password)) throw new MyException(MyCodesNumbers.WrongCharactersInPassword, doc.SelectSingleNode($"exceptions/WrongCharactersInPassword").InnerText);
            if (password.Length < 6) throw new MyException(MyCodesNumbers.TooShortPassword, doc.SelectSingleNode($"exceptions/TooShortPassword").InnerText);
            if (password.Length >= 20) throw new MyException(MyCodesNumbers.TooLongPassword, doc.SelectSingleNode($"exceptions/TooLongPassword").InnerText);
            if (password == Password) throw new MyException(MyCodesNumbers.SamePassword, doc.SelectSingleNode($"exceptions/SamePassword").InnerText);

            string salt = encrypter.GetSalt(password);
            string hash = encrypter.GetHash(password, salt);

            Password = hash;
            Salt = salt;
        }
        
        public void SetPremium(bool premium)
        {
            IsPremium = premium;
        }

        public void SetRole(int role, string language)
        {
            IsRoleCorrectSet(role, language);
            if (role > 1) SetPremium(true);
            RoleId = role;
        }
        public void SetEmail(string mail, string language)
        {
            string file = $"Resources\\MyExceptions.{language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            if (!regex_mail.IsMatch(mail)) throw new MyException(MyCodesNumbers.WrongFormatOfMail, doc.SelectSingleNode($"exceptions/WrongFormatOfMail").InnerText);
            if (mail == Email) throw new MyException(MyCodesNumbers.SameEmail, doc.SelectSingleNode($"exceptions/SameEmail").InnerText);
            Email = mail;
        }
        public void SetStatus(int status, string language)
        {
            string file = $"Resources\\MyExceptions.{language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            if (status == 1 || status == 0) Status = status;
            else throw new MyException(MyCodesNumbers.WrongUserStatus, doc.SelectSingleNode($"exceptions/WrongUserStatus").InnerText);
        }
        public void IsRoleCorrectSet(int roleid, string language)
        {
            string file = $"Resources\\MyExceptions.{language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            if (roleid == 3 || roleid == 2 || roleid == 1) RoleId = roleid;
            else throw new MyException(MyCodesNumbers.WrongRoleId, doc.SelectSingleNode($"exceptions/WrongRoleId").InnerText);
        }
        public string GetRoleName(int roleId)
        {
            return Role.GetRoleName(roleId);
        }
    }
}
