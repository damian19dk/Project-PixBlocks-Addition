﻿using PixBlocks_Addition.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    public class Video: Media
    {
        public Video(string mediaId, Guid parentId, bool premium, string title, string description, string picture, long duration,
            string lang, IEnumerable<string> resources = null, IEnumerable<Tag> tags = null)
            : base(mediaId, parentId, premium, title, description, picture, duration, lang, resources, tags)
        {
        }

        protected Video()
        {
        }

        public string Status { get; protected set; }

        public void SetStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                throw new MyException(MyCodesNumbers.WrongUserStatus, Exceptions.ExceptionMessages.DomainExceptionMessages.InvalidVideoStatus);
            }
            Status = status;
        }

        public void SetParent(Course course)
        {
            if(course == null)
            {
                throw new MyException(MyCodesNumbers.NullCourse, Exceptions.ExceptionMessages.DomainExceptionMessages.CourseNotFound);
            }
            ParentId = course.Id;
        }
    }
}
