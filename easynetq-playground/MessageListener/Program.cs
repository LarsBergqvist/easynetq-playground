using Common;
using MessageListener;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<RabbitMqSettings>(hostContext.Configuration.GetSection("RabbitMqSettings"));
        services.AddSingleton<Listener>();
        services.AddHostedService<Worker>();
    });

var host = builder.Build();
host.Run();
