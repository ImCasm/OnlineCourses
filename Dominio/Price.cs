using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio
{
    public class Price
    {
        public int PriceId { get; set; }
        public decimal ActualPrice { get; set; }
        public decimal Offer { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
