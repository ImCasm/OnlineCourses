﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class CourseTeacher
    {
        public Guid CourseId { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public Course Course { get; set; }
    }
}