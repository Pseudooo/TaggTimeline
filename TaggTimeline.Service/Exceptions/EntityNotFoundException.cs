using System.Net;

namespace TaggTimeline.Service.Exceptions;

public class EntityNotFoundException : ApiException
{
    public EntityNotFoundException(string message) 
        : base(message, HttpStatusCode.NotFound)
        { }
}
