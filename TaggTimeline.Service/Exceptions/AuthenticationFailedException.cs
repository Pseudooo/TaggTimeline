using System.Net;

namespace TaggTimeline.Service.Exceptions;

public class AuthenticationFailedException : ApiException
{
    public AuthenticationFailedException(string message)
    : base(message, HttpStatusCode.Unauthorized)
        { }
}
