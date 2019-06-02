using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerApi.Common
{
    public class NotFoundException : Exception { public NotFoundException(string message) : base(message) { } }

    public class UnauthorizedException : Exception { public UnauthorizedException(string message) : base(message) { } }
    public class RequiredException : Exception { public RequiredException(string message) : base(message) { } }
    public class CustomerExistsException : Exception { public CustomerExistsException(string message) : base(message) { } }

    public class MaxLengthException : Exception { public MaxLengthException(string message) : base(message) { } }

    public class InvalidModelException : Exception { public InvalidModelException(string message) : base(message) { } }
}
