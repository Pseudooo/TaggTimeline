
using System.Net.Http.Headers;
using NUnit.Framework;
using TaggTime.Service.Commands;
using TaggTimeline.ClientModel.Taggs;

namespace TaggTimeline.WebApi.Test.Categories;

[TestFixture]
public class CreateCategoryTests
{

    public HttpClient client { get; private set; } = null!;

    [SetUp]
    public void SetUp()
    {
        client = GlobalSetup.SandboxApplication.CreateClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GlobalSetup.Token);
    }

    [Test]
    public async Task Create_Category_Should_Create_Category()
    {
        var categoryKey = "important";
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("/Category", UriKind.Relative),
            Content = JsonContent.Create(new CreateCategoryCommand() { Key = categoryKey }),
        };
        var response = await client.SendAsync(request);
        Assert.IsTrue(response.IsSuccessStatusCode);

        var createdCategory = await response.Content.ReadFromJsonAsync<CategoryModel>();
        Assert.IsNotNull(createdCategory);
        Assert.IsInstanceOf<CategoryModel>(createdCategory);

        Assert.IsNotNull(createdCategory!.Id);
        Assert.IsNotNull(createdCategory.CreatedDate);
        Assert.IsNotNull(createdCategory.Key);
        Assert.AreEqual(categoryKey, createdCategory.Key);
        Assert.IsNotNull(createdCategory.Taggs);
        Assert.IsInstanceOf<IEnumerable<TaggPreviewModel>>(createdCategory.Taggs);
    }

}
