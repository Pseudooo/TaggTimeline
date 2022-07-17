using System.Net;

namespace TaggTimeline.Service.Exceptions;

public class ValidationFailedException : ApiException
{
    public ValidationFailedException(string message)
    : base(message, HttpStatusCode.BadRequest)
        { }
}
