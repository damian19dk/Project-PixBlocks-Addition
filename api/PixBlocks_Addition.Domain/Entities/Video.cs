using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Entities
{
    /// <summary>
    /// Nie wiadomo czy bedzie potrzebna wszystko zalezy od hostingu filmow implemetuje na zapas
    /// </summary>
    public class Video
    {
        public Guid Id { get; protected set; }
        public Guid CourseId { get; protected set; }
        public string VideoSrc { get; protected set; }

        public virtual Course Course { get; protected set; }

        protected Video() { }

        public Video(Course course, string videosrc)
        {
            SetVideoSrc(videosrc);
            CourseId = course.Id;
        }

        public void SetVideoSrc(string videosrc)
        {
            VideoSrc = videosrc;
        }
    }
}
