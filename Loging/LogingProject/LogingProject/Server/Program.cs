using server;



var app = Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>()).Build();
app.Run();


