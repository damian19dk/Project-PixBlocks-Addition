using PixBlocks_Addition.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class CustomResource
    {
        public Guid Id { get; protected set; }
        public string ContentType { get; protected set; }
        public string File { get; protected set; }

        public CustomResource(string contentType, string base64Image)
        {
            Id = Guid.NewGuid();
            SetFile(contentType, base64Image);
        }

        protected CustomResource()
        {
        }

        public void SetFile(string contentType, string base64Resource)
        {
            if(!CustomResource.ContentTypes.Contains(contentType))
            {
                throw new MyException(MyCodesNumbers.WrongFormatOfPhoto, $"Niewspierany typ pliku: {contentType}.");
            }
            ContentType = contentType;
            File = base64Resource;
        }

        public static CustomResource Create(string contentType, string base64Resource)
            => new CustomResource(contentType, base64Resource);

        public static IReadOnlyCollection<string> ContentTypes { get; }

        static CustomResource()
        {
            ContentTypes = new List<string>() { "image/jpg", "image/jpeg", "image/png", "image/bmp" };
        }
    }
}
