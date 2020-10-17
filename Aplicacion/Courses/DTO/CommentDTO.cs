using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion.Courses.DTO
{
    public class CommentDTO
    {
        public Guid CommentId { get; set; }
        public string Student { get; set; }
        public int Review { get; set; }
        public string Content { get; set; }
        public Guid CourseId { get; set; }
    }
}
