
using System.Net;
using NUnit.Framework;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Service.Commands;

namespace TaggTimeline.WebApi.Test.Taggs;

[TestFixture]
public class AddInstanceToTaggTests
{
    public HttpClient client { get; private set; } = null!;

    [SetUp]
    public void SetUp()
    {
        client = GlobalSetup.SandboxApplication.CreateClient();
    }

    [Test]
    public async Task Add_Instance_To_Tagg_Should_Succeed()
    {
        var tagg = GlobalSetup.SandboxApplication
                              .Context
                              .Set<Tagg>()
                              .ToList()
                              .Last();
        var occuranceDate = DateTime.UtcNow;

        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"/Tagg/instance", UriKind.Relative),
            Content = JsonContent.Create(new CreateInstanceCommand() { TaggId = tagg.Id, OccuranceDate = occuranceDate }),
        };
        var response = await client.SendAsync(request);
        Assert.IsTrue(response.IsSuccessStatusCode);

        var returnedInstance = await response.Content.ReadFromJsonAsync<InstanceModel>();
        Assert.IsNotNull(returnedInstance);
        Assert.AreEqual(occuranceDate, returnedInstance!.OccuranceDate);
    }

    [Test]
    public async Task Add_Instance_To_Tagg_Should_Fail()
    {
        var taggId = Guid.NewGuid();
        var occuranceDate = DateTime.UtcNow;

        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"/Tagg/instance", UriKind.Relative),
            Content = JsonContent.Create(new CreateInstanceCommand() { TaggId = taggId, OccuranceDate = occuranceDate }),
        };
        var response = await client.SendAsync(request);
        Assert.IsFalse(response.IsSuccessStatusCode);
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}
