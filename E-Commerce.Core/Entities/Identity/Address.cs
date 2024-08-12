using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entities.Identity
{
    public class Address
    {
        public int ID { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string State { get; set; }

        public string AppUSerId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
