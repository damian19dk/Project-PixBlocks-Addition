using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml;

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


        public Tag(string name, string description, string fontColor, string backgroundColor, string language, string localization)
        {
            Id = Guid.NewGuid();
            SetName(name, localization);
            SetDescription(description, localization);
            SetColor(fontColor, backgroundColor, localization);
            SetLanguage(language, localization);
        }

        public void SetName(string name, string language)
        {
            if(String.IsNullOrWhiteSpace(name) || name.Length < 2)
            {
                if (language == "en")
                    throw new MyException(MyCodesNumbers.InvalidTagName, "Tag name must have at least 2 characters!");
                else
                    throw new MyException(MyCodesNumbers.InvalidTagName, "Nazwa tagu musi mieć przynajmniej 2 znaki");
            }
            if(name.Contains(',') || name.Contains(';'))
            {
                if (language == "en")
                    throw new MyException(MyCodesNumbers.InvalidTagName, "Tag name cannot contain , or ;");
                else
                    throw new MyException(MyCodesNumbers.InvalidTagName, "Nazwa tagu nie może zawierać , lub ;");
            }
            Name = name;
        }

        public void SetColor(string fontColor, string backgroundColor, string language)
        {
            string file = $"Resources\\MyExceptions.{language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
  
            if (fontColor == null || backgroundColor == null)
            {
                throw new MyException(MyCodesNumbers.InvalidColor, doc.SelectSingleNode($"exceptions/InvalidColor").InnerText);
            }
            FontColor = fontColor;
            BackgroundColor = backgroundColor;
        }

        public void SetDescription(string description, string language)
        {
            string file = $"Resources\\MyExceptions.{language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            if (description == null)
            {
                throw new MyException(MyCodesNumbers.InvalidDescription, doc.SelectSingleNode($"exceptions/InvalidDescription").InnerText);
            }
            Description = description;
        }

        public void SetLanguage(string language, string userLanguage)
        {
            string file = $"Resources\\MyExceptions.{userLanguage}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            if (!Languages.IsValidLanguage(language)) 
            {
                throw new MyException(MyCodesNumbers.InvalidLanguage, doc.SelectSingleNode($"exceptions/LanguageTooShortTooLong").InnerText);
            }
            Language = language;
        }

        protected Tag()
        {
        }
    }
}
