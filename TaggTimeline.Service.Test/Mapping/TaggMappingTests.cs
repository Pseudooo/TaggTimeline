
using Mapster;
using NUnit.Framework;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Domain.Entities.Taggs;
using TaggTimeline.Service.Test.Mocks.Taggs;

namespace TaggTimeline.Service.Test.Mapping;

[TestFixture]
public class TaggMappingTests
{
    [Test]
    public void Map_Tagg_To_TaggModel_Should_Map()
    {
        var tagg = TaggTestData.InitialTaggs[0];

        var taggModel = tagg.Adapt<TaggModel>();

        Assert.IsNotNull(taggModel);
        Assert.IsInstanceOf<TaggModel>(taggModel);
        Assert.AreEqual(tagg.Id, taggModel.Id);
        Assert.AreEqual(tagg.Key, taggModel.Key);
        Assert.AreEqual(tagg.CreatedDate, taggModel.CreatedDate);
        Assert.AreEqual(tagg.ModifiedDate, taggModel.ModifiedDate);
        Assert.AreEqual(tagg.DeletedDate, taggModel.DeletedDate);
        Assert.AreEqual(tagg.Colour, taggModel.Colour);
    }

    [Test]
    public void Map_Tagg_To_TaggPreviewModel_Should_Map()
    {
        var tagg = TaggTestData.InitialTaggs[0];

        var taggPreviewModel = tagg.Adapt<TaggPreviewModel>();

        Assert.IsNotNull(taggPreviewModel);
        Assert.IsInstanceOf<TaggPreviewModel>(taggPreviewModel);
        Assert.AreEqual(tagg.Id, taggPreviewModel.Id);
        Assert.AreEqual(tagg.Key, taggPreviewModel.Key);
        Assert.AreEqual(tagg.Colour, taggPreviewModel.Colour);
    }

    [Test]
    public void Map_Tagg_Instance_To_InstanceModel_Should_Map()
    {
        var instance = new Instance()
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now,
            OccuranceDate = DateTime.Now,
        };

        var instanceModel = instance.Adapt<InstanceModel>();

        Assert.IsNotNull(instanceModel);
        Assert.IsInstanceOf<InstanceModel>(instanceModel);
        Assert.AreEqual(instance.Id, instanceModel.Id);
        Assert.AreEqual(instance.CreatedDate, instanceModel.CreatedDate);
        Assert.AreEqual(instance.OccuranceDate, instanceModel.OccuranceDate);
    }
}
