
using System.Net;

namespace TaggTimeline.Service.Exceptions;

public class ApiException : Exception
{
    public ApiException(string message, HttpStatusCode httpStatusCode) : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }

    public HttpStatusCode HttpStatusCode { get; set; }
}
