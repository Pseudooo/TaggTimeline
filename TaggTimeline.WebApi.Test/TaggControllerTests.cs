
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Context;
using TaggTimeline.WebApi;

namespace TaggTimeline.WebApi.Test;

[TestFixture]
public class TaggControllerTests
{
    private WebApplicationFactory<Program> sandboxApplication = null!;

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
        var taggList = await response.Content.ReadFromJsonAsync<IEnumerable<TaggModel>>();
        
        Assert.IsNotNull(response);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsNotNull(taggList);
        Assert.IsInstanceOf<IEnumerable<TaggModel>>(taggList);
    }

}
