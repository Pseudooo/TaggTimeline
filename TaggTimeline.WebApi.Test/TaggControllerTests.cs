
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.WebApi.Test;

[TestFixture]
public class TaggControllerTests
{
    [Test]
    public async Task Test_Client()
    {
        var client = GlobalSetup.SandboxApplication.CreateClient();
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
