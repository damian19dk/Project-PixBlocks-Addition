using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace PixBlocks_Addition.Domain.Entities
{
    public abstract class Media
    {
        private static readonly Regex regex_url = new Regex(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$");
        private static readonly Regex regex_language = new Regex("[A-Za-z]+");

        public Guid Id { get; protected set; }
        public int Index { get; protected set; }
        public string MediaId { get; protected set; }
        public Guid ParentId { get; protected set; }
        public bool Premium { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public string Picture { get; protected set; }
        public ICollection<string> Resources { get; protected set; } = new HashSet<string>();
        public long Duration { get; protected set; }
        public DateTime PublishDate { get; protected set; }
        public string Language { get; protected set; }
        public Guid? QuizId { get; protected set; } = null;
        public ICollection<Tag> Tags { get; protected set; } = new HashSet<Tag>();

        protected Media() { }

        public Media(string mediaId, Guid parentId, bool premium, string title, string description, string picture, long duration,
            string lang, string localization, IEnumerable<string> resources = null, IEnumerable<Tag> tags = null)
        {
            Id = Guid.NewGuid();
            Index = 0;
            MediaId = mediaId;
            ParentId = parentId;
            SetPremium(premium);
            SetTitle(title, localization);
            SetDescription(description, localization);
            SetPicture(picture, localization);
            SetDuration(duration, localization);
            SetLanguage(lang, localization);

            if (resources != null)
                foreach (string resource in resources)
                    Resources.Add(resource);

            if (tags != null)
                foreach (Tag tag in tags)
                    Tags.Add(tag);

            PublishDate = DateTime.Now;
        }


        public void SetPremium(bool value) => Premium = value;

        public void SetTitle(string title, string language)
        {
            string file = $"Resources\\MyExceptions.{language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            if (string.IsNullOrEmpty(title))
            {
                throw new MyException(MyCodesNumbers.EmptyTitle, doc.SelectSingleNode($"exceptions/EmptyTitle").InnerText);
            }
            if (title.Length < 3)
            {
                throw new MyException(MyCodesNumbers.TooShortTitle, doc.SelectSingleNode($"exceptions/TooShortTitle").InnerText);
            }
            if (title.Length > 250)
            {
                throw new MyException(MyCodesNumbers.TooLongTitle, doc.SelectSingleNode($"exceptions/TooLongTitle").InnerText);
            }
            Title = title;
        }

        public void SetDescription(string description, string language)
        {
            string file = $"Resources\\MyExceptions.{language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            if (description.Length < 3)
            {
                throw new MyException(MyCodesNumbers.TooShortDescription, doc.SelectSingleNode($"exceptions/TooShortDescription").InnerText);
            }
            if (description.Length > 10000)
            {
                throw new MyException(MyCodesNumbers.TooLongDescription, doc.SelectSingleNode($"exceptions/TooShortDescription").InnerText);
            }
            Description = description;
        }

        public void SetPicture(string src, string language)
        {
            string file = $"Resources\\MyExceptions.{language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            Guid result;
            if (!string.IsNullOrEmpty(src) && !Guid.TryParse(src, out result))
                if (!regex_url.IsMatch(src))
                    throw new MyException(MyCodesNumbers.InvalidPictureSrc, doc.SelectSingleNode($"exceptions/InvalidPictureSrc").InnerText);
            Picture = src;
        }

        public void SetDuration(long length, string language)
        {
            string file = $"Resources\\MyExceptions.{language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            if (length<0)
            {
                throw new MyException(MyCodesNumbers.InvalidDuration, doc.SelectSingleNode($"exceptions/InvalidDuration").InnerText);
            }
            Duration = length;
        }

        public void SetLanguage(string lang, string language)
        {
            string file = $"Resources\\MyExceptions.{language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            if (string.IsNullOrWhiteSpace(lang))
            {
                throw new MyException(MyCodesNumbers.EmptyLanguage, doc.SelectSingleNode($"exceptions/EmptyLanguage").InnerText);
            }
            if(!regex_language.IsMatch(lang))
            {
                throw new MyException(MyCodesNumbers.InvalidLanguage, doc.SelectSingleNode($"exceptions/InvalidLanguage").InnerText);
            }
            if(lang.Length < 2)
            {
                throw new MyException(MyCodesNumbers.TooShortLanguage, doc.SelectSingleNode($"exceptions/TooShortLanguage").InnerText);
            }
            if (lang.Length > 60)
            {
                throw new MyException(MyCodesNumbers.TooLongLanguage, doc.SelectSingleNode($"exceptions/TooLongLanguage").InnerText);
            }
            Language = lang;
        }

        public void SetQuiz(Guid? id)
        {
            QuizId = id;
        }

        public void SetIndex(int index, string language)
        {
            string file = $"Resources\\MyExceptions.{language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            if (index < 0)
                throw new MyException(MyCodesNumbers.InvalidIndex, doc.SelectSingleNode($"exceptions/InvalidIndex").InnerText);
            Index = index;
        }
    }
}
