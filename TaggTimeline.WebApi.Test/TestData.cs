
using TaggTimeline.Domain.Context;
using TaggTimeline.Domain.Entities.Taggs;

namespace TaggTimeline.WebApi.Test;

public static class TestData
{

    public static DataContext SeedTestData(this DataContext context)
    {

        Taggs[0].Categories = new[] { Categories[0] };
        Categories[0].Taggs = new[] { Taggs[0] };
        
        foreach(var tagg in Taggs)
        {
            context.Set<Tagg>().Add(tagg);
        }

        foreach(var category in Categories)
        {
            context.Set<Category>().Add(category);
        }

        context.SaveChanges();

        return context;
    }

    public static IList<Instance> Instances { get; } = new List<Instance>()
    {
        new Instance()
        {
            OccuranceDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
        },
    };

    public static IList<Tagg> Taggs { get; } = new List<Tagg>()
    {
        new Tagg()
        {
            Key = "Climbing",
            Colour = "CAE9F5",
            Instances = TestData.Instances,
            Categories = Enumerable.Empty<Category>(),
        },
        new Tagg()
        {
            Key = "Walking",
            Colour = "CAE9F5",
            Instances = Enumerable.Empty<Instance>(),
            Categories = Enumerable.Empty<Category>(),
        }
    };

    public static IList<Category> Categories { get; } = new List<Category>()
    {
        new Category()
        {
            Key = "Sport",
            Taggs = Enumerable.Empty<Tagg>(),
        },
        new Category()
        {
            Key = "Important",
            Taggs = Enumerable.Empty<Tagg>(),
        },
    };

}
