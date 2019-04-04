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
        public string E_mail { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public int RoleId { get; protected set; }
        public bool Is_premium { get; protected set; }
        public int Status { get; protected set; }

        public virtual Role Role { get; protected set; }

        protected User() { }

        public User(Guid id, string login, string e_mail, int status, string password, Role role, string salt)
        {
            Id = id;
            SetLogin(login);
            SetEmail(e_mail);
            SetRole(role);
            SetPassword(password, salt);
            Is_premium = false;
        }

        public void SetLogin(string login)
        {
            if (login.Length < 3) throw new Exception();
            if (login.Length > 20) throw new Exception();
            if (String.IsNullOrEmpty(login)) throw new Exception();
            if (!regex_login.IsMatch(login)) throw new Exception();
            Login = login;
        }

        public void SetPassword(string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new Exception();
            if (password.Length < 6) throw new Exception();
            if (password.Length > 20) throw new Exception();
            //string salt = encrypter.GetSalt(password);
            //string hash = encrypter.GetHash(password, salt);

            //Password = hash;
            //Salt = salt

            Password = password;
            Salt = password;
        }
        
        public void SetPremium(bool premium)
        {
            Is_premium = premium;
        }

        public void SetRole(Role role)
        {
            RoleId = role.Id;
        }
        public void SetEmail(string mail)
        {
            if (!regex_mail.IsMatch(mail)) throw new Exception();
            E_mail = mail;
        }
        public void SetStatus(int status)
        {
            if (status == 1 || status == 0) Status = status;
            else throw new Exception();
        }

    }
}
