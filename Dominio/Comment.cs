using System;

namespace Domain
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string Student { get; set; }
        public int Review { get; set; }
        public string Content { get; set; }
        public Guid CourseId { get; set; } 
        public Course Course { get; set; }
    }
}
