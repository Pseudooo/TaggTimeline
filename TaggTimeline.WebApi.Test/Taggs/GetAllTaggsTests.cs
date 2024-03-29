
using System.Net.Http.Headers;
using NUnit.Framework;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.WebApi.Test.Taggs;

[TestFixture]
public class GetAllTaggsTests
{

    public HttpClient client { get; private set; } = null!;

    [SetUp]
    public void SetUp()
    {
        client = GlobalSetup.SandboxApplication.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalSetup.Token);
    }

    [Test]
    public async Task Get_All_Taggs_Should_Return_Taggs()
    {
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("/Tagg/all", UriKind.Relative),
        };
        var response = await client.SendAsync(request);
        Assert.IsTrue(response.IsSuccessStatusCode);

        var returnedTaggs = await response.Content.ReadFromJsonAsync<IEnumerable<TaggPreviewModel>>();
        Assert.IsNotNull(returnedTaggs);
        Assert.IsInstanceOf<IEnumerable<TaggPreviewModel>>(returnedTaggs);

        var tagg = returnedTaggs!.First();
        Assert.IsNotNull(tagg);
        Assert.IsNotEmpty(tagg.Key);
        Assert.IsNotEmpty(tagg.Colour);
        Assert.AreEqual(tagg.Colour.Length == 7 || tagg.Colour.Length == 4, true);
    }

}
