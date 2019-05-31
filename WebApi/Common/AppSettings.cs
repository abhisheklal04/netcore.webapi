using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Common
{
    public class AppSettings
    {
        public bool EnableCurlLogger { get; set; } = false;
        public bool EnableEFLogger { get; set; } = false;
    }
}
