using ConsoleApp;
using ConsoleApp.Desks;
using ConsoleApp.Distributors;
using ConsoleApp.Opponents;
using ConsoleApp.Sandboxes;
using ConsoleApp.Shufflers;
using ConsoleApp.Strategies;
using DataLib.Desk;
using DataLib.Persons.Distributors;
using DataLib.Persons.Opponents;
using DataLib.SandBoxes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<SandboxWorker>();
                services.AddScoped<Sandbox, СolosseumSandbox>();
                services.AddSingleton<IDeskShuffler, RandomDeskShuffler>();
                services.AddSingleton<Distributor,Zeus>();
                services.AddSingleton<Opponent>(new Ilon(new PickFirstCardStrategy()));
                services.AddSingleton<Opponent>(new Mark(new PickFirstCardStrategy()));
                services.AddSingleton<ShuffleableDesk>(new Shuffleable36CardDesk());
            });
    }
}


 