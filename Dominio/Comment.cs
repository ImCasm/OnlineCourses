using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Student { get; set; }
        public int Review { get; set; }
        public string Content { get; set; }
        public int CourseId { get; set; } 
        public Course Course { get; set; }
    }
}
