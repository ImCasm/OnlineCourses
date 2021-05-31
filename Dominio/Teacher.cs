using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Teacher
    {
        public Guid TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public byte[] ProfilePicture { get; set; }
        public ICollection<Course> Courses;
    }
}
