using System;

namespace Persistence.Dapper.Teacher
{
    public class Teacher
    {
        public Guid TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Grade { get; set; }
        public DateTime? CreationDate { get; set; }
        public string JobTitle { get; set; }
    }
}
