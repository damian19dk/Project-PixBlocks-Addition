using PixBlocks_Addition.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class VideoRecord
    {
        public Guid Id { get; protected set; }
        public Video Video { get; protected set; }
        public long Time { get; protected set; }

        public VideoHistory VideoHistory { get; protected set; }

        public VideoRecord(Video video, long time)
        {
            Id = Guid.NewGuid();
            Video = video;
            SetTime(time);
        }

        protected VideoRecord()
        {

        }

        public void SetTime(long time)
        {
            if (time < 0)
            {
                throw new MyException(MyCodesNumbers.InvalidTime, Exceptions.ExceptionMessages.DomainExceptionMessages.InvalidTime);
            }
            Time = time;
        }
    }
}
