using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Courses.DTO
{
    public class CourseTeacherDTO
    {
        public Guid CourseId { get; set; }
        public Guid TeacherId { get; set; }
    }
}
