using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PublicationDate { get; set; }
        public byte[] Image { get; set; }
        public Price OfferPrice { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<CourseTeacher> Teachers { get; set; }
    }
}
