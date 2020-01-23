using PixBlocks_Addition.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class VideoHistory
    {
        public Guid Id { get; protected set; }
        public User User { get; protected set; }
        public ICollection<VideoRecord> Videos { get; protected set; } = new HashSet<VideoRecord>();

        public VideoHistory(User user)
        {
            Id = Guid.NewGuid();
            User = user ?? throw new MyException(MyCodesNumbers.UserNotFound, Exceptions.ExceptionMessages.DomainExceptionMessages.UserNotFound);
        }

        protected VideoHistory()
        {

        }
    }
}
