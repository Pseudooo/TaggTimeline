
using NUnit.Framework;
using TaggTimeline.WebApi.Test.Configuration;
using TaggTimeline.WebApi.Test.WebApplicationFactory;

namespace TaggTimeline.WebApi.Test;

[SetUpFixture]
public class GlobalSetup
{
    public static TestConfiguration TestConfiguration { get; private set; } = null!;

    public static SandboxApplication SandboxApplication { get; private set; } = null!;
    public static string Token { get; private set; } = null!;
    public static string UserId { get; private set; } = null!;

    [OneTimeSetUp]
    public async Task SetUp()
    {
        TestConfiguration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false)
                                                      .Build()
                                                      .Get<TestConfiguration>();

        SandboxApplication = new SandboxApplication(TestConfiguration);
        SandboxApplication.CreateClient();

        var result = TaggTimeline.MigrationRunner.Program.Main(new string[0]);
        Assert.AreEqual(0, result);
        
        var authResult = await SandboxApplication.IdentityService.Register("testuser", "Password123!");
        UserId = (await SandboxApplication.IdentityService.GetIdentityUser("testuser"))!.Id;
        Token = authResult.Token;

        SandboxApplication.Context.SeedTestData();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        if(TestConfiguration.OverrideWebapiConnectionString) // Only delete if running tests on isolated db
            SandboxApplication.Context.Database.EnsureDeleted();
    }
}
