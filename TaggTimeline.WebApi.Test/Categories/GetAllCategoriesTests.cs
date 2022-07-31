
using System.Net.Http.Headers;
using NUnit.Framework;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.WebApi.Test.Categories;

[TestFixture]
public class GetAllCategoriesTests
{
    public HttpClient client { get; private set; } = null!;

    [SetUp]
    public void SetUp()
    {
        client = GlobalSetup.SandboxApplication.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalSetup.Token);
    }

    [Test]
    public async Task Get_All_Categories_Should_Return_Categories()
    {
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("/Category/all", UriKind.Relative),
        };
        var response = await client.SendAsync(request);
        Assert.IsTrue(response.IsSuccessStatusCode);

        var returnedCategories = await response.Content.ReadFromJsonAsync<IEnumerable<CategoryPreviewModel>>();
        Assert.IsNotNull(returnedCategories);
        Assert.IsInstanceOf<IEnumerable<CategoryPreviewModel>>(returnedCategories);

        var category = returnedCategories!.First();
        Assert.IsNotNull(category);
        Assert.IsNotEmpty(category.Key);
    }
}
