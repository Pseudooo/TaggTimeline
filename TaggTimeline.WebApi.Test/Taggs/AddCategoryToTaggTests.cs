
using NUnit.Framework;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.WebApi.Test.Taggs;

[TestFixture]
public class AddCategoryToTaggTests
{
    public HttpClient client { get; private set; } = null!;

    [SetUp]
    public void SetUp()
    {
        client = GlobalSetup.SandboxApplication.CreateClient();
    }

    [Test]
    public async Task Add_Category_To_Tagg_Should_Succeed()
    {
        var categoryToAdd = GlobalSetup.SandboxApplication
                                    .Context
                                    .Set<Category>()
                                    .Where(category => category.Key == "Important")
                                    .Single();

        var tagg = GlobalSetup.SandboxApplication
                                .Context
                                .Set<Tagg>()
                                .First();

        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"/Tagg/{tagg.Id}/categorise/{categoryToAdd.Id}", UriKind.Relative),
        };
        var response = await client.SendAsync(request);
        Assert.IsTrue(response.IsSuccessStatusCode);

        var returnedTagg = await response.Content.ReadFromJsonAsync<TaggModel>();
        Assert.IsNotNull(returnedTagg);
        Assert.IsInstanceOf<TaggModel>(returnedTagg);
        Assert.IsTrue(returnedTagg!.Categories.Any(category => category.Id == categoryToAdd.Id));
    }
}
