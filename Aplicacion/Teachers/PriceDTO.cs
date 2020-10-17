using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacion.Courses.DTO
{
    public class PriceDTO
    {
        public Guid PriceId { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal Offer { get; set; }
        public Guid CourseId { get; set; }
    }
}
