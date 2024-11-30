using Microsoft.AspNetCore.Server.Kestrel.Core;
using University.Units;



var app = Host.CreateDefaultBuilder()
    .ConfigureWebHostDefaults(builder =>
    {
        builder.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(4040, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
            });
        });

        builder.UseStartup<Startup>();
    })
    .Build();

app.Run();

