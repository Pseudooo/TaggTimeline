
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using TaggTimeline.Domain.Context;

namespace TaggTimeline.WebApi.Test;

public class SandboxApplication : WebApplicationFactory<Program>
{

    public SandboxApplication() : base()
    {
        WithWebHostBuilder(builder => {
            builder.ConfigureServices(sc => {
                sc.AddDbContext<DataContext>(opts => opts.UseNpgsql("User ID=taggserver;Password=Q6%5nWgeN4#9;Host=localhost;Port=5432;Database=taggtimelinesb;Pooling=true;Connection Lifetime=0;"));
            });
       });
    }


}
