
using NUnit.Framework;


namespace TaggTimeline.WebApi.Test;

[TestFixture]
public class MigrationRunnerTest
{

    [Test, Order(0)]
    public void Test_Migration_Runner()
    {
        var result = TaggTimeline.MigrationRunner.Program.Main(new string[0]);
        Assert.AreEqual(0, result);
    }


}
