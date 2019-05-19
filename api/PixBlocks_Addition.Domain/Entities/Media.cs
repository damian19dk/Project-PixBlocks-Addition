using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    /// <summary>
    /// Nie wiadomo czy bedzie potrzebna wszystko zalezy od hostingu filmow implemetuje na zapas
    /// </summary>
    public abstract class Media
    {
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
            string lang, IEnumerable<Tag> tags=null)
        {
            Id = Guid.NewGuid();
            MediaId=mediaId;
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
        public void SetTitle(string title) => Title = title;
        public void SetDescription(string description) => Description = description;
        public void SetPicture(string src) => Picture = src;
        public void SetDuration(long length) => Duration = length;
        public void SetLanguage(string lang) => Language = lang;
    }
}
