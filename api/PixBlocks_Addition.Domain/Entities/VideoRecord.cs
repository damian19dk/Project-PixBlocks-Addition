using PixBlocks_Addition.Domain.Exceptions;
using PixBlocks_Addition.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace PixBlocks_Addition.Domain.Entities
{
    public class VideoRecord
    {
        public Guid Id { get; protected set; }
        public Video Video { get; protected set; }
        public long Time { get; protected set; }

        public VideoHistory VideoHistory { get; protected set; }

        public VideoRecord(Video video, long time, string localization)
        {
            Id = Guid.NewGuid();
            Video = video;
            SetTime(time, localization);
        }

        protected VideoRecord()
        {

        }

        public void SetTime(long time, string language)
        {
            string file = $"Resources\\MyExceptions.{language}.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            if (time < 0)
            {
                throw new MyException(MyCodesNumbers.NegativeTime, doc.SelectSingleNode($"exceptions/NegativeTime").InnerText);
            }
            Time = time;
        }
    }
}
