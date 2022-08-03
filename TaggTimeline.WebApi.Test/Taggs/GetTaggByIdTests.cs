
using System.Net;
using System.Net.Http.Headers;
using NUnit.Framework;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.WebApi.Test.Taggs;

[TestFixture]
public class GetTaggByIdTests
{

    public HttpClient client { get; private set; } = null!;

    [SetUp]
    public void SetUp()
    {
        client = GlobalSetup.SandboxApplication.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalSetup.Token);
    }

    [Test]
    public async Task Get_Tagg_By_Id_Should_Get_Tagg()
    {
        var taggToLookup = GlobalSetup.SandboxApplication
                                      .Context
                                      .Set<Tagg>()
                                      .First();

        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/Tagg/{taggToLookup.Id}", UriKind.Relative),
        };
        var response = await client.SendAsync(request);
        Assert.IsTrue(response.IsSuccessStatusCode);

        var returnedTagg = await response.Content.ReadFromJsonAsync<TaggModel>();
        Assert.IsNotNull(returnedTagg);
        Assert.IsInstanceOf<TaggModel>(returnedTagg);

        Assert.AreEqual(taggToLookup.Key, returnedTagg!.Key);
        Assert.AreEqual(taggToLookup.Colour, returnedTagg!.Colour);
        Assert.IsNotNull(returnedTagg.Instances);
        Assert.IsInstanceOf<IEnumerable<InstanceModel>>(returnedTagg.Instances);
        Assert.IsNotNull(returnedTagg.Categories);
        Assert.IsInstanceOf<IEnumerable<CategoryPreviewModel>>(returnedTagg.Categories);
    }

    [Test]
    public async Task Get_Tagg_By_Id_Should_Fail()
    {
        var idToLookup = Guid.NewGuid();
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/Tagg/{idToLookup}", UriKind.Relative),
        };
        var response = await client.SendAsync(request);
        Assert.IsFalse(response.IsSuccessStatusCode);
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }

}
