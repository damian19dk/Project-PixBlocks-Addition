using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

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
      
        public User(string login, string e_mail, string password, Role role, IEncrypter encrypter)
        {
            Id = Guid.NewGuid();
            SetLogin(login);
            SetEmail(e_mail);
            SetRole(role);
            SetPassword(password, encrypter);
            IsPremium = false;
        }

        public User(string login, string e_mail, int role, string password, IEncrypter encrypter)
        {
            Id = Guid.NewGuid();
            SetLogin(login);
            SetEmail(e_mail);
            IsRoleCorrectSet(role);
            SetPassword(password, encrypter);
            IsPremium = false;
        }

        public void SetLogin(string login)
        {
            if (login.Length < 3) throw new MyException(MyCodes.TooShortLogin);
            if (login.Length >= 20) throw new MyException(MyCodes.TooLongLogin);
            if (String.IsNullOrEmpty(login)) throw new MyException(MyCodes.WrongCharactersInLogin);
            if (!regex_login.IsMatch(login)) throw new MyException(MyCodes.WrongCharactersInLogin);
            Login = login;
        }

        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new MyException(MyCodes.WrongCharactersInPassword);
            if (password.Length < 6) throw new MyException(MyCodes.TooShortPassword);
            if (password.Length >= 20) throw new MyException(MyCodes.TooLongPassword);
            if (password == Password) throw new Exception();

            string salt = encrypter.GetSalt(password);
            string hash = encrypter.GetHash(password, salt);

            Password = hash;
            Salt = salt;
        }
        
        public void SetPremium(bool premium)
        {
            IsPremium = premium;
        }

        public void SetRole(Role role)
        {
            RoleId = role.Id;
        }
        public void SetEmail(string mail)
        {
            if (!regex_mail.IsMatch(mail)) throw new MyException(MyCodes.WrongFormatOfMail);
            if (mail == Email) throw new Exception();
            Email = mail;
        }
        public void SetStatus(int status)
        {
            if (status == 1 || status == 0) Status = status;
            else throw new MyException(MyCodes.WrongUserStatus);
        }
        public void IsRoleCorrectSet(int roleid)
        {
            if (roleid == 3 || roleid == 1) RoleId = roleid;
            else throw new MyException(MyCodes.WrongRoleId);
        }
    }
}
