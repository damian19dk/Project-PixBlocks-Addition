using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

namespace PixBlocks_Addition.Domain.Entities
{
    [Table("Users", Schema = "dbo")]
    public class Users
    {
        private static readonly Regex regex_login = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");
        private static readonly Regex regex_mail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int Id { get; protected set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        [Display(Name = "Login")]
        public string Login { get; protected set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        [Display(Name = "e-mail")]
        public string E_mail { get; protected set; }

        [Required]
        [Column(TypeName = "varchat(20)")]
        [Display(Name = "Password")]
        public string Password { get; protected set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Salt")]
        public string Salt { get; protected set; }

        [Required]
        [Column(TypeName = "int")]
        [Display(Name = "Role")]
        public int Role { get; protected set; }

        [Required]
        [Column(TypeName = "bool")]
        [Display(Name = "Is premium")]
        public bool Is_premium { get; protected set; }

        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Token")]
        public string Token { get; protected set; }
        
        public Users()
        {

        }

        public void SetLogin(string login)
        {
            if (login.Length < 3) throw new Exception();
            if (login.Length > 20) throw new Exception();
            if (String.IsNullOrEmpty(login)) throw new Exception();
            if (!regex_login.IsMatch(login)) throw new Exception();
            Login = login;
        }

        public void SetPassword(string password)
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
    }
}
