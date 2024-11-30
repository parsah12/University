using Microsoft.AspNetCore.Server.Kestrel.Core;

var app = Host.CreateDefaultBuilder()
    .ConfigureWebHostDefaults(builder =>
    {
        builder.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(5050, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
            });
        });

        builder.UseStartup<Startup>();
    })
    .Build();

app.Run();

