using System;
using System.Collections.Generic;

namespace Services.Repository
{
    public partial class ShortNames
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ShortName { get; set; }
    }
}
