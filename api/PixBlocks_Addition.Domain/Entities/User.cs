﻿using PixBlocks_Addition.Domain.Exceptions;
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
            SetRole(role.Id);
            SetPassword(password, encrypter);
        }

        public User(string login, string e_mail, int role, string password, IEncrypter encrypter)
        {
            Id = Guid.NewGuid();
            SetLogin(login);
            SetEmail(e_mail);
            SetRole(role);
            SetPassword(password, encrypter);
        }

        public void SetLogin(string login)
        {
            if (login.Length < 3) throw new MyException(MyCodesNumbers.TooShortLogin, Exceptions.ExceptionMessages.DomainExceptionMessages.InvalidUserLoginLength);
            if (login.Length > 20) throw new MyException(MyCodesNumbers.TooLongLogin, Exceptions.ExceptionMessages.DomainExceptionMessages.InvalidUserLoginLength);
            if (String.IsNullOrEmpty(login)) throw new MyException(MyCodesNumbers.WrongCharactersInLogin, Exceptions.ExceptionMessages.DomainExceptionMessages.InvalidUserLoginLength);
            if (!regex_login.IsMatch(login)) throw new MyException(MyCodesNumbers.WrongCharactersInLogin, Exceptions.ExceptionMessages.DomainExceptionMessages.IllegalUserLoginCharacters);
            Login = login;
        }

        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new MyException(MyCodesNumbers.WrongCharactersInPassword, Exceptions.ExceptionMessages.DomainExceptionMessages.InvalidUserPasswordLength);
            if (password.Length < 6) throw new MyException(MyCodesNumbers.TooShortPassword, Exceptions.ExceptionMessages.DomainExceptionMessages.InvalidUserPasswordLength);
            if (password.Length >= 20) throw new MyException(MyCodesNumbers.TooLongPassword, Exceptions.ExceptionMessages.DomainExceptionMessages.InvalidUserPasswordLength);
            if (password == Password) throw new MyException(MyCodesNumbers.SamePassword, Exceptions.ExceptionMessages.DomainExceptionMessages.SameUserPassword);

            string salt = encrypter.GetSalt(password);
            string hash = encrypter.GetHash(password, salt);

            Password = hash;
            Salt = salt;
        }
        
        public void SetPremium(bool premium)
        {
            IsPremium = premium;
        }

        public void SetRole(int role)
        {
            IsRoleCorrectSet(role);
            if (role > 1) SetPremium(true);
            RoleId = role;
        }
        public void SetEmail(string mail)
        {
            if (!regex_mail.IsMatch(mail)) throw new MyException(MyCodesNumbers.WrongFormatOfMail, Exceptions.ExceptionMessages.DomainExceptionMessages.WrongFormatOfEmail);
            if (mail == Email) throw new MyException(MyCodesNumbers.SameEmail, Exceptions.ExceptionMessages.DomainExceptionMessages.SameEmail);
            Email = mail;
        }
        public void SetStatus(int status)
        {
            if (status == 1 || status == 0) Status = status;
            else throw new MyException(MyCodesNumbers.WrongUserStatus, Exceptions.ExceptionMessages.DomainExceptionMessages.InvalidUserStatus);
        }
        public void IsRoleCorrectSet(int roleid)
        {
            if (roleid == 3 || roleid == 2 || roleid == 1) RoleId = roleid;
            else throw new MyException(MyCodesNumbers.WrongRoleId, Exceptions.ExceptionMessages.DomainExceptionMessages.InvalidRole);
        }
        public string GetRoleName(int roleId)
        {
            return Role.GetRoleName(roleId);
        }
    }
}
