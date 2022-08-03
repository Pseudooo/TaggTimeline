
using System.Net;
using System.Net.Http.Headers;
using NUnit.Framework;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Service.Queries;

namespace TaggTimeline.WebApi.Test.Taggs;

[TestFixture]
public class SearchForTaggTests
{

    public HttpClient client { get; private set; } = null!;

    [SetUp]
    public void SetUp()
    {
        client = GlobalSetup.SandboxApplication.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalSetup.Token);
    }

    [Test]
    public async Task Search_For_Taggs_Should_Return_Tagg()
    {
        var searchTerm = "Climb";
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("/Tagg/search", UriKind.Relative),
            Content = JsonContent.Create<SearchForTaggQuery>(new SearchForTaggQuery() { SearchTerm = searchTerm }),
        };
        var response = await client.SendAsync(request);
        Assert.IsTrue(response.IsSuccessStatusCode);

        var returnedTaggs = await response.Content.ReadFromJsonAsync<IEnumerable<TaggPreviewModel>>();
        Assert.IsNotNull(returnedTaggs);
        Assert.IsInstanceOf<IEnumerable<TaggPreviewModel>>(returnedTaggs);
        Assert.AreEqual(1, returnedTaggs!.Count());

        var tagg = returnedTaggs!.First();
        Assert.IsNotNull(tagg);
        Assert.IsTrue(tagg.Key.Contains(searchTerm));
    }

    [Test]
    public async Task Search_For_Taggs_Should_Return_Nothing()
    {
        var searchTerm = "asuperuniquesearchtermthatwontappear";
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("/Tagg/search", UriKind.Relative),
            Content = JsonContent.Create<SearchForTaggQuery>(new SearchForTaggQuery() { SearchTerm = searchTerm }),
        };
        var response = await client.SendAsync(request);
        Assert.IsTrue(response.IsSuccessStatusCode);

        var returnedTaggs = await response.Content.ReadFromJsonAsync<IEnumerable<TaggPreviewModel>>();
        Assert.IsNotNull(returnedTaggs);
        Assert.IsInstanceOf<IEnumerable<TaggPreviewModel>>(returnedTaggs);
        Assert.IsFalse(returnedTaggs!.Any());
    }

    [Test]
    public async Task Search_For_Taggs_No_SearchTerm_Should_Fail()
    {
        var searchTerm = string.Empty;
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("/Tagg/search", UriKind.Relative),
            Content = JsonContent.Create<SearchForTaggQuery>(new SearchForTaggQuery() { SearchTerm = searchTerm }),
        };
        var response = await client.SendAsync(request);
        Assert.IsFalse(response.IsSuccessStatusCode);
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
