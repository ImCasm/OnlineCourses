﻿using System;
using System.Collections.Generic;

namespace Domain
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PublicationDate { get; set; }
        public byte[] Image { get; set; }
        public Price OfferPrice { get; set; }
        public DateTime? CreationDate { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<CourseTeacher> Teachers { get; set; }
    }
}
