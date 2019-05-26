using PixBlocks_Addition.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class CustomImage
    {
        public Guid Id { get; protected set; }
        public string ContentType { get; protected set; }
        public string Image { get; protected set; }

        public CustomImage(string contentType, string base64Image)
        {
            Id = Guid.NewGuid();
            SetImage(contentType, base64Image);
        }

        protected CustomImage()
        {
        }

        public void SetImage(string contentType, string base64Image)
        {
            if(!CustomImage.ContentTypes.Contains(contentType))
            {
                throw new MyException(MyCodes.InvalidImage, $"Unsupported media type {contentType}.");
            }
            ContentType = contentType;
            Image = base64Image;
        }

        public static CustomImage Create(string contentType, string base64Image)
            => new CustomImage(contentType, base64Image);

        public static IReadOnlyCollection<string> ContentTypes { get; }

        static CustomImage()
        {
            ContentTypes = new List<string>() { "image/jpg", "image/jpeg", "image/png", "image/bmp" };
        }
    }
}
