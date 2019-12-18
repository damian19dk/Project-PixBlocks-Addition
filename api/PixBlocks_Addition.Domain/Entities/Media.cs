using PixBlocks_Addition.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

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
        public Tag Category { get; protected set; }
        public bool Premium { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public string Picture { get; protected set; }
        public ICollection<string> Resources { get; protected set; } = new HashSet<string>();
        public long Duration { get; protected set; }
        public DateTime PublishDate { get; protected set; }
        public string Language { get; protected set; }
        public ICollection<Tag> Tags { get; protected set; } = new HashSet<Tag>();

        protected Media() { }

        public Media(string mediaId, bool premium, string title, string description, string picture, long duration,
            string lang, IEnumerable<string> resources = null, IEnumerable<Tag> tags = null)
        {
            Id = Guid.NewGuid();
            Index = 0;
            MediaId = mediaId;
            SetPremium(premium);
            SetTitle(title);
            SetDescription(description);
            SetPicture(picture);
            SetDuration(duration);
            SetLanguage(lang);

            if (resources != null)
                foreach (string resource in resources)
                    Resources.Add(resource);

            if (tags != null)
                foreach (Tag tag in tags)
                    Tags.Add(tag);

            PublishDate = DateTime.Now;
        }

        protected Media(string mediaId, Guid parentId, bool premium, string title, string description, string picture, long duration, string lang, IEnumerable<string> resources, IEnumerable<Tag> tags)
        {
            Id = Guid.NewGuid();
            Index = 0;
            MediaId = mediaId;
            ParentId = parentId;
            SetPremium(premium);
            SetTitle(title);
            SetDescription(description);
            SetPicture(picture);
            SetDuration(duration);
            SetLanguage(lang);

            if (resources != null)
                foreach (string resource in resources)
                    Resources.Add(resource);

            if (tags != null)
                foreach (Tag tag in tags)
                    Tags.Add(tag);

            PublishDate = DateTime.Now;
        }

        public void SetPremium(bool value) => Premium = value;
        public void SetTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new MyException(MyCodesNumbers.EmptyTitle, MyCodes.EmptyTitle);
            }
            if (title.Length < 3)
            {
                throw new MyException(MyCodesNumbers.TooShortTitle, MyCodes.TooShortTitle);
            }
            if (title.Length > 250)
            {
                throw new MyException(MyCodesNumbers.TooLongTitle, MyCodes.TooLongTitle);
            }
            Title = title;
        }
        public void SetDescription(string description)
        {
            if (description.Length < 3)
            {
                throw new MyException(MyCodesNumbers.TooShortDescription, MyCodes.TooShortDescription);
            }
            if (description.Length > 10000)
            {
                throw new MyException(MyCodesNumbers.TooLongDescription, MyCodes.TooLongDescription);
            }
            Description = description;
        }
        public void SetPicture(string src)
        {
            Guid result;
            if (!string.IsNullOrEmpty(src) && !Guid.TryParse(src, out result))
                if (!regex_url.IsMatch(src))
                    throw new MyException(MyCodesNumbers.InvalidPictureSrc, MyCodes.InvalidPicSource);
            Picture = src;
        }
        public void SetDuration(long length)
        {
            if(length<0)
            {
                throw new MyException(MyCodesNumbers.InvalidDuration, MyCodes.InvalidDuration);
            }
            Duration = length;
        }
        public void SetLanguage(string lang)
        {
            if(string.IsNullOrWhiteSpace(lang))
            {
                throw new MyException(MyCodesNumbers.EmptyLanguage, MyCodes.EmptyLanguageTitle);
            }
            if(!regex_language.IsMatch(lang))
            {
                throw new MyException(MyCodesNumbers.InvalidLanguage, MyCodes.WrongCharactersInLanguage);
            }
            if(lang.Length < 2)
            {
                throw new MyException(MyCodesNumbers.TooShortLanguage, MyCodes.TooShortLanguage);
            }
            if (lang.Length > 60)
            {
                throw new MyException(MyCodesNumbers.TooLongLanguage, MyCodes.TooLongLanguage);
            }
            Language = lang;
        }
        public void SetIndex(int index)
        {
            if (index < 0)
                throw new MyException(MyCodesNumbers.InvalidIndex, "Index cannot be negative");
            Index = index;
        }
    }
}
