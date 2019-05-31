using System;
using System.Collections.Generic;

namespace Services.Repository
{
    public partial class Clients
    {
        public int ClientId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public DateTime DateCreated { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateLastLogin { get; set; }
        public int RoadhouseClientId { get; set; }
    }
}
