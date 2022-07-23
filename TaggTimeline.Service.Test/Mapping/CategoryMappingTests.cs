
using Mapster;
using NUnit.Framework;
using TaggTimeline.ClientModel.Taggs;
using TaggTimeline.Service.Configuration;
using TaggTimeline.Service.Test.Mocks.Categories;

namespace TaggTimeline.Service.Test.Mapping;

[TestFixture]
public class CategoryMappingTests
{

    [SetUp]
    public void SetUp()
    {
        _ = new MappingConfig();
    }

    [Test]
    public void Map_Category_To_CategoryModel_Should_Map()
    {
        var category = CategoryTestData.InitCategories[0];

        var categoryModel = category.Adapt<CategoryModel>();

        Assert.IsNotNull(categoryModel);
        Assert.IsInstanceOf<CategoryModel>(categoryModel);
        Assert.AreEqual(category.Id, categoryModel.Id);
        Assert.AreEqual(category.Key, categoryModel.Key);
        Assert.AreEqual(category.CreatedDate, categoryModel.CreatedDate);
        Assert.AreEqual(category.ModifiedDate, categoryModel.ModifiedDate);
        Assert.AreEqual(category.DeletedDate, categoryModel.DeletedDate);
    }

    [Test]
    public void Map_Category_To_CategoryPreviewModel_Should_Map()
    {
        var category = CategoryTestData.InitCategories[0];

        var categoryPreviewModel = category.Adapt<CategoryPreviewModel>();

        Assert.IsNotNull(categoryPreviewModel);
        Assert.IsInstanceOf<CategoryPreviewModel>(categoryPreviewModel);
        Assert.AreEqual(category.Id, categoryPreviewModel.Id);
        Assert.AreEqual(category.Key, categoryPreviewModel.Key);
    }

}
