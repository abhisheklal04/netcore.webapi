using System;
using System.Collections.Generic;
using System.Text;

namespace WebApi.Common
{
    public class NotFoundException : Exception { public NotFoundException(string message) : base(message) { } }

    public class UnauthorizedException : Exception { public UnauthorizedException(string message) : base(message) { } }
    public class RequiredFirstNameException : Exception { public RequiredFirstNameException(string message) : base(message) { } }
    public class RequiredLastNameException : Exception { public RequiredLastNameException(string message) : base(message) { } }

    public class InvalidModelException : Exception { public InvalidModelException(string message) : base(message) { } }
}
