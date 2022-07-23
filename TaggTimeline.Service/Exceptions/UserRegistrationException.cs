using System.Net;

namespace TaggTimeline.Service.Exceptions;

public class UserRegistrationException : ApiException
{
    public UserRegistrationException(string message)
    : base(message, HttpStatusCode.BadRequest)
        { }
}
