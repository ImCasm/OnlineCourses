using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCourses
{
    public class Course
    {

        public int CourseId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PublicationDate { get; set; }
    }
}
