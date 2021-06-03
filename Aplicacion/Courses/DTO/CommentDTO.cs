using System;

namespace Application.Courses.DTO
{
    public class CommentDTO
    {
        public Guid CommentId { get; set; }
        public string Student { get; set; }
        public int Review { get; set; }
        public string Content { get; set; }
        public DateTime? CreationDate { get; set; }
        public Guid CourseId { get; set; }
    }
}
