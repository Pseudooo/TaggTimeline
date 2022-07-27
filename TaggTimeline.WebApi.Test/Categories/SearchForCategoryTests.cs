
using System.Net;
using NUnit.Framework;
using TaggTime.Service.Queries;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.WebApi.Test.Categories;

[TestFixture]
public class SearchForCategoryTests
{
    public HttpClient client { get; private set; } = null!;

    [SetUp]
    public void SetUp()
    {
        client = GlobalSetup.SandboxApplication.CreateClient();
    }

    [Test]
    public async Task Search_For_Categories_Should_Return_Category()
    {
        var searchTerm = "Spo";
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("/Category/search", UriKind.Relative),
            Content = JsonContent.Create<SearchForCategoriesQuery>(new SearchForCategoriesQuery() { SearchTerm = searchTerm }),
        };
        var response = await client.SendAsync(request);
        Assert.IsTrue(response.IsSuccessStatusCode);

        var returnedCategories = await response.Content.ReadFromJsonAsync<IEnumerable<CategoryPreviewModel>>();
        Assert.IsNotNull(returnedCategories);
        Assert.IsInstanceOf<IEnumerable<CategoryPreviewModel>>(returnedCategories);
        Assert.AreEqual(1, returnedCategories!.Count());

        var category = returnedCategories!.First();
        Assert.IsNotNull(category);
        Assert.IsTrue(category.Key.Contains(searchTerm));
    }

    [Test]
    public async Task Search_For_Categories_Should_Return_Nothing()
    {
        var searchTerm = "asuperuniquesearchtermthatwontcomeup";
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("/Category/search", UriKind.Relative),
            Content = JsonContent.Create<SearchForCategoriesQuery>(new SearchForCategoriesQuery() { SearchTerm = searchTerm }),
        };
        var response = await client.SendAsync(request);
        Assert.IsTrue(response.IsSuccessStatusCode);

        var returnedCategories = await response.Content.ReadFromJsonAsync<IEnumerable<CategoryPreviewModel>>();
        Assert.IsNotNull(returnedCategories);
        Assert.IsInstanceOf<IEnumerable<CategoryPreviewModel>>(returnedCategories);
        Assert.IsFalse(returnedCategories!.Any());
    }

    [Test]
    public async Task Search_For_Categories_No_SearchTerm_Should_Fail()
    {
        var searchTerm = string.Empty;
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("Category/search", UriKind.Relative),
            Content = JsonContent.Create<SearchForCategoriesQuery>(new SearchForCategoriesQuery() { SearchTerm = searchTerm }),
        };
        var response = await client.SendAsync(request);
        Assert.IsFalse(response.IsSuccessStatusCode);
        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
