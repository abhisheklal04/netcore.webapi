﻿using System;

namespace WebApi.Models.Request
{
    public class CustomerUpdateRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
