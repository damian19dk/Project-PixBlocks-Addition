using PixBlocks_Addition.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

namespace PixBlocks_Addition.Domain.Entities
{
    /// <summary>
    /// Nie wiadomo czy bedzie potrzebna wszystko zalezy od hostingu filmow implemetuje na zapas
    /// </summary>
    public abstract class Media
    {
        private static readonly Regex regex_url = new Regex(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$");
        private static readonly Regex regex_language = new Regex("[A-Za-z]+");

        public Guid Id { get; protected set; }
        public string MediaId { get; protected set; }
        public Category Category { get; protected set; }
        public bool Premium { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public string Picture { get; protected set; }
        public long Duration { get; protected set; }
        public DateTime PublishDate { get; protected set; }
        public string Language { get; protected set; }
        public ICollection<Tag> Tags { get; protected set; } = new HashSet<Tag>();

        protected Media() { }

        public Media(string mediaId, bool premium, string title, string description, string picture, long duration,
            string lang, IEnumerable<Tag> tags = null)
        {
            Id = Guid.NewGuid();
            MediaId = mediaId;
            SetPremium(premium);
            SetTitle(title);
            SetDescription(description);
            SetPicture(picture);
            SetDuration(duration);
            SetLanguage(lang);

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
                throw new MyException(MyCodesNumbers.InvalidTitle, "The title cannot be null or empty.");
            }
            if (title.Length < 3 || title.Length > 250)
            {
                throw new MyException(MyCodesNumbers.InvalidTitle, "The title must be between 3 and 250 characters.");
            }
            Title = title;
        }
        public void SetDescription(string description)
        {
            Description = description;
        }
        public void SetPicture(string src)
        {
            Guid result;
            if (!string.IsNullOrEmpty(src) && !Guid.TryParse(src, out result))
                if (!regex_url.IsMatch(src))
                    throw new MyException(MyCodesNumbers.InvalidPictureSrc, $"The picture source must be a Guid or url.");
            Picture = src;
        }
        public void SetDuration(long length)
        {
            if(length<0)
            {
                throw new MyException(MyCodesNumbers.InvalidDuration, "Duration cannot negative.");
            }
            Duration = length;
        }
        public void SetLanguage(string lang)
        {
            if(string.IsNullOrWhiteSpace(lang))
            {
                throw new MyException(MyCodesNumbers.InvalidLanguage, "Language cannot be null or empty.");
            }
            if(!regex_language.IsMatch(lang))
            {
                throw new MyException(MyCodesNumbers.InvalidLanguage, "Language contains illegal characters.");
            }
            if(lang.Length < 2 || lang.Length > 60)
            {
                throw new MyException(MyCodesNumbers.InvalidLanguage, "Language must be between 2 and 60 characters.");
            }
            Language = lang;
        }
    }
}
