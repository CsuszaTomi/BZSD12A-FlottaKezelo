using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace BZSD12A.Models
    {
        internal class Brand
        {
            public int Id { get; set; }
            public string BrandName { get; set; } = string.Empty;
        }

        internal class CarModel
        {
            public int Id { get; set; }
            public string ModelName { get; set; } = string.Empty;
            public string ModelMaker { get; set; } = string.Empty;   // a gyártó neve (manufacturers.BrandName)
        }

        internal class UserLookup
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }
    }
