using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entities.OrdderAggregate
{
    public class Address
    {
        public Address()
        {
            
        }

        public Address(string streetName, string houseNumber, string state)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
            State = state;
        }

        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string State { get; set; }
    }
}
