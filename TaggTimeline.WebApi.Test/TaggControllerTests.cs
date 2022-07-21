
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using TaggTimeline.WebApi;

namespace TaggTimeline.WebApi.Test;

[TestFixture]
public class TaggControllerTests
{

    [SetUp]
    public void SetUp()
    {
        var webApplicationFactory = new SandboxApplication();
    }

    [Test]
    public void Test_Client()
    {
        Assert.IsTrue(true);
    }

}
