using Common;
using MessageListener;
using Microsoft.Extensions.Options;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<RabbitMqSettings>(hostContext.Configuration.GetSection("RabbitMqSettings"));
        services.AddHostedService<Worker>();
        services.AddSingleton<Worker, Worker>(sp => new Worker(sp));
    });


var host = builder.Build();

host.Run();
