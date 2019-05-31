using System;

namespace WebApi.Models.Request
{
    public class CustomerAddRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
