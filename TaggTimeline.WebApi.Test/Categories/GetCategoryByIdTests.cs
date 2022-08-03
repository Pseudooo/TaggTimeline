
using System.Net;
using System.Net.Http.Headers;
using NUnit.Framework;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.WebApi.Test.Categories;

[TestFixture]
public class GetCategoryByIdTests
{
    public HttpClient client { get; private set; } = null!;

    [SetUp]
    public void SetUp()
    {
        client = GlobalSetup.SandboxApplication.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalSetup.Token);
    }

    [Test]
    public async Task Get_Category_By_Id_Should_Get_Category()
    {
        var categoryToLookup = GlobalSetup.SandboxApplication
                                          .Context
                                          .Set<Category>()
                                          .First();

        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/Category/{categoryToLookup.Id}", UriKind.Relative),
        };
        var response = await client.SendAsync(request);
        Assert.IsTrue(response.IsSuccessStatusCode);

        var returnedCategory = await response.Content.ReadFromJsonAsync<CategoryModel>();
        Assert.IsNotNull(returnedCategory);
        Assert.IsInstanceOf<CategoryModel>(returnedCategory);

        Assert.AreEqual(categoryToLookup.Id, returnedCategory!.Id);
        Assert.IsNotEmpty(returnedCategory.Key);
        Assert.AreEqual(categoryToLookup.Key, returnedCategory.Key);
        Assert.IsNotNull(returnedCategory.Taggs);
        Assert.IsInstanceOf<IEnumerable<TaggPreviewModel>>(returnedCategory.Taggs);
    }

    [Test]
    public async Task Get_Category_By_Id_Should_Fail()
    {
        var idToLookup = Guid.NewGuid();
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"/Category/{idToLookup}", UriKind.Relative),
        };
        var response = await client.SendAsync(request);
        Assert.IsFalse(response.IsSuccessStatusCode);
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}
