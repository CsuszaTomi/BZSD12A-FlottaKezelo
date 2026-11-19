using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BZSD12A.Models
{
    internal class Maintenance
    {
        public int Id { get; set; }
        public int CarID { get; set; }
        public DateTime ServiceDate { get; set; }
        public string? Description { get; set; }
        public int Cost { get; set; }
        public int RanKm { get; set; }
    }
}
