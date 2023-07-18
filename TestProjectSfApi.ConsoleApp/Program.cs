// See https://aka.ms/new-console-template for more information

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using TestProjectSfApi.Application;
using TestProjectSfApi.ConsoleApp.Wrapper;

var builder = new HostBuilder();

builder
    .ConfigureServices(ConfigureServices);

static void ConfigureServices(
            HostBuilderContext host,
            IServiceCollection services)
{
    services
        .AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()))
        .AddApplicationServices()
        .AddHostedService<SystemDataLoadLogWrapper>();
        }

var cancellationTokenSource = new CancellationTokenSource();
_ = Task.Factory.StartNew(() =>
{
    var key = Console.ReadKey();
    switch (key.Key)
    {
        case ConsoleKey.Escape:
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine("Stopping application");
            cancellationTokenSource.Cancel();
            break;
    }
}, CancellationToken.None);

await builder.RunConsoleAsync(cancellationTokenSource.Token);

Console.WriteLine("Hello, World!");