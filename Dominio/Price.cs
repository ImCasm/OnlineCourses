using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class Price
    {
        public Guid PriceId { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal ActualPrice { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Offer { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
