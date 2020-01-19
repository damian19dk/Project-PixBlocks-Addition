using PixBlocks_Addition.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class Tag
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string FontColor { get; protected set; }
        public string BackgroundColor { get; protected set; }
        public string Language { get; protected set; }


        public Tag(string name, string description, string fontColor, string backgroundColor, string language)
        {
            Id = Guid.NewGuid();
            SetName(name);
            SetDescription(description);
            SetColor(fontColor, backgroundColor);
            SetLanguage(language);
        }

        public void SetName(string name)
        {
            if(String.IsNullOrWhiteSpace(name) || name.Length < 2)
            {
                throw new MyException(MyCodesNumbers.InvalidTagName, "Tag name must have at least 2 characters.");
            }
            if(name.Contains(',') || name.Contains(';'))
            {
                throw new MyException(MyCodesNumbers.InvalidTagName, "Tag name cannot contain , or ;");
            }
            Name = name;
        }

        public void SetColor(string fontColor, string backgroundColor)
        {
            if (fontColor == null || backgroundColor == null)
            {
                throw new MyException(MyCodesNumbers.InvalidColor, "Color cannot be null.");
            }
            FontColor = fontColor;
            BackgroundColor = backgroundColor;
        }

        public void SetDescription(string description)
        {
            if(description == null)
            {
                throw new MyException(MyCodesNumbers.InvalidDescription, "Description cannot be null.");
            }
            Description = description;
        }

        public void SetLanguage(string language)
        {
            if(!Languages.IsValidLanguage(language))
            {
                throw new MyException(MyCodesNumbers.InvalidLanguage, "Language has to contain from 2 to 60 letters.");
            }
            Language = language;
        }

        protected Tag()
        {
        }
    }
}
