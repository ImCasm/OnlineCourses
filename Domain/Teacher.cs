using System;
using System.Collections.Generic;

namespace Domain
{
    public class Teacher
    {
        public Guid TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public DateTime? CreationDate { get; set; }
        public byte[] ProfilePicture { get; set; }
        public ICollection<Course> Courses;
    }
}
