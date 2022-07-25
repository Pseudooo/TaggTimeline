
using NUnit.Framework;
using TaggTimeline.WebApi.Test.Configuration;
using TaggTimeline.WebApi.Test.WebApplicationFactory;

namespace TaggTimeline.WebApi.Test;

[SetUpFixture]
public class GlobalSetup
{
    public static TestConfiguration TestConfiguration { get; private set; } = null!;

    public static SandboxApplication SandboxApplication { get; private set; } = null!;

    [OneTimeSetUp]
    public void SetUp()
    {
        TestConfiguration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false)
                                                      .Build()
                                                      .Get<TestConfiguration>();

        SandboxApplication = new SandboxApplication(TestConfiguration);
    }

    [OneTimeTearDown]
    public void TearDown()
    {

    }
}
