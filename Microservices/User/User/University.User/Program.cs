using Microsoft.AspNetCore.Server.Kestrel.Core;
using University.User;


var app = Host.CreateDefaultBuilder()
    .ConfigureWebHostDefaults(builder =>
    {
        builder.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(6060, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
            });
        });
       
        
        builder.UseStartup<Startup>();
    })
    .Build();

app.Run();
