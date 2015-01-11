using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WashingtonSalesTaxRateLookup
{
    public class Address
    {
        public Address() {
        }
        
        public Address(string street, string city, string zipCode) {
            Street = street;
            City = city;
            ZipCode = zipCode;
        }

        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }
}
