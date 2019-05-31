using System;
using System.Collections.Generic;

namespace Services.Repository
{
    public partial class Customers
    {
        public int CustomerId { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
