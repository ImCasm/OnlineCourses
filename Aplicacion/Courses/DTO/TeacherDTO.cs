using System;

namespace Application.Courses.DTO
{
    public class TeacherDTO
    {
        public Guid TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public byte[] ProfilePicture { get; set; }
    }
}
