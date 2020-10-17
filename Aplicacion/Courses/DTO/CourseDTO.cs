using Aplicacion.Courses.DTO;
using System;
using System.Collections.Generic;

namespace Aplicacion.Courses.DTO
{
    public class CourseDTO
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PublicationDate { get; set; }
        public byte[] Image { get; set; }
        public ICollection<TeacherDTO> Teachers { get; set; }
        public PriceDTO Price { get; set; }
        public List<CommentDTO> Comments { get; set; }
    }
}
