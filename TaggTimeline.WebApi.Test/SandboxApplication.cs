
using Microsoft.AspNetCore.Mvc.Testing;

namespace TaggTimeline.WebApi.Test;

public class SandboxApplication : WebApplicationFactory<Program>
{

    public SandboxApplication() : base()
    {
        WithWebHostBuilder(builder => {
            builder.ConfigureServices(sc => {
                
            });
       });
    }


}
