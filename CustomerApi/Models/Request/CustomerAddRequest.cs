using System;

namespace CustomerApi.Models.Request
{
    public class CustomerAddRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
