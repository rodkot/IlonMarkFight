    using Core.Desks;
using Core.Distributors;
using Core.Opponents;
using Core.Sandboxes;
using Core.Shufflers;
using Core.Strategies;
using Core.Workers;
using DataLib.Desks;
using DataLib.Desks.Interfacies;
using DataLib.Persons.Distributors;
using DataLib.Persons.Opponents;
using DataLib.SandBoxes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp;

internal static class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddHostedService<SandboxWorker>();
                services.AddScoped<Sandbox, СolosseumSandbox>();
                services.AddSingleton<IDeskShuffler, RandomDeskShuffler>();
                services.AddSingleton<IDistributor,Zeus>();
                services.AddSingleton<Opponent>(new Ilon(new PickFirstCardStrategy()));
                services.AddSingleton<Opponent>(new Mark(new PickFirstCardStrategy()));
                services.AddSingleton<ShuffleableDesk>(new Shuffleable36CardDesk());
            });
    }
}