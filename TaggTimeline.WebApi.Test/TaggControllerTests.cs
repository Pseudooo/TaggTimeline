
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using TaggTimeline.WebApi;

namespace TaggTimeline.WebApi.Test;

[TestFixture]
public class TaggControllerTests
{
    private SandboxApplication sandboxApplication = null!;

    [SetUp]
    public void SetUp()
    {
        sandboxApplication = new SandboxApplication();
    }

    [Test]
    public async Task Test_Client()
    {
        var client = sandboxApplication.CreateClient();
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("/Tagg/all", UriKind.Relative),            
        };
        
        var response = await client.SendAsync(request);
        
        Assert.IsNotNull(response);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

}
