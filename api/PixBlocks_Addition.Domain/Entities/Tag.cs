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
            if(String.IsNullOrWhiteSpace(name) || name.Length < 2 || name.Length > 60)
            {
                throw new MyException(MyCodesNumbers.InvalidTagName, Exceptions.ExceptionMessages.DomainExceptionMessages.InvalidTagName);
            }
            if(name.Contains(',') || name.Contains(';'))
            {
                throw new MyException(MyCodesNumbers.InvalidTagName, Exceptions.ExceptionMessages.DomainExceptionMessages.InvalidTagName);
            }
            Name = name;
        }

        public void SetColor(string fontColor, string backgroundColor)
        {
            if (fontColor == null || backgroundColor == null)
            {
                throw new MyException(MyCodesNumbers.InvalidColor, Exceptions.ExceptionMessages.DomainExceptionMessages.InvalidColor);
            }
            FontColor = fontColor;
            BackgroundColor = backgroundColor;
        }

        public void SetDescription(string description)
        {
            if(description == null)
            {
                throw new MyException(MyCodesNumbers.InvalidTagDescription, Exceptions.ExceptionMessages.DomainExceptionMessages.InvalidTagDescription);
            }
            Description = description;
        }

        public void SetLanguage(string language)
        {
            if(!Languages.IsValidLanguage(language))
            {
                throw new MyException(MyCodesNumbers.InvalidLanguage, Exceptions.ExceptionMessages.DomainExceptionMessages.InvalidLanguage);
            }
            Language = language;
        }

        protected Tag()
        {
        }
    }
}
