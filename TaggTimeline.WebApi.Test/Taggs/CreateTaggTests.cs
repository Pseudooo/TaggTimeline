
using NUnit.Framework;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Service.Commands;

namespace TaggTimeline.WebApi.Test.Taggs;

[TestFixture]
public class CreateTaggTests
{

    public HttpClient client { get; private set; } = null!;

    [SetUp]
    public void Setup()
    {   
        client = GlobalSetup.SandboxApplication.CreateClient();
    }

    [Test]
    public async Task Create_Tagg_Should_Create_Tagg()
    {
        var taggKey = "Tagg";
        var taggColour = "#CAE9F5";
        var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri("/Tagg", UriKind.Relative),
            Content = JsonContent.Create(new CreateTaggCommand() { Key = taggKey, Colour = taggColour }),
        };
        var response = await client.SendAsync(request);
        Assert.IsTrue(response.IsSuccessStatusCode);
        
        var createdTagg = await response.Content.ReadFromJsonAsync<TaggModel>();
        Assert.IsNotNull(createdTagg);
        Assert.IsInstanceOf<TaggModel>(createdTagg);

        Assert.IsNotNull(createdTagg!.Id);
        Assert.IsNotNull(createdTagg.CreatedDate);
        Assert.IsNotNull(createdTagg.Key);
        Assert.AreEqual(taggKey, createdTagg.Key);
        Assert.IsNotNull(createdTagg.Colour);
        Assert.AreEqual(createdTagg.Colour, taggColour);
        Assert.IsNotNull(createdTagg.Instances);
        Assert.IsInstanceOf<IEnumerable<InstanceModel>>(createdTagg.Instances);
        Assert.IsNotNull(createdTagg.Categories);
        Assert.IsInstanceOf<IEnumerable<CategoryPreviewModel>>(createdTagg.Categories);
    }

}
