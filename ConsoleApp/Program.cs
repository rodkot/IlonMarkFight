using Core.Client;
using Core.Config;
using Core.Desks;
using Core.Distributors;
using Core.Opponents;
using Core.Sandboxes;
using Core.Shufflers;
using Core.Strategies;
using Core.Workers;
using DataLib.Desks.Interfaces;
using DataLib.Distributors.Interfaces;
using DataLib.Opponents;
using DataLib.Opponents.Interfaces;
using DataLib.SandBoxes.Interfaces;
using DataLib.Shuffler.Interfaces;
using DbStorage.Context;
using DbStorage.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models;

namespace ConsoleApp;

internal static class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        /*return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                
                services.AddHostedService<SandboxDbWorker>();
                services.AddSingleton<ExperimentConditionService>();
                services.AddDbContext<ExperimentConditionContext>();
                services.AddDbContextFactory<ExperimentConditionContext>(
                    options => options.UseSqlite(
                        hostContext.Configuration.GetConnectionString("RoundDatabase")));
                services.AddScoped<ISandBox, NoShuffleableDeskSandbox>();
                services.AddSingleton<IDeskShuffler, RandomDeskShuffler>();
                services.AddSingleton<IDistributor, Zeus>();
                services.AddSingleton<Opponent>(new Ilon(new PickFirstCardStrategy()));
                services.AddSingleton<Opponent>(new Mark(new PickFirstCardStrategy()));
                services.AddSingleton<ShuffleableDesk>(new Shuffleable36CardDesk());
                services.AddSingleton(new CoreConfig(1_000,DbRequest.Generate));
                
                 
                 services.AddHostedService<SandboxWorker>();
                 services.AddScoped<ISandBox, NoShuffleableDeskSandbox>();
                 services.AddSingleton<IDeskShuffler, RandomDeskShuffler>();
                 services.AddSingleton<IDistributor, Zeus>();
                 services.AddSingleton<Opponent>(new Ilon(new PickFirstCardStrategy()));
                 services.AddSingleton<Opponent>(new Mark(new PickFirstCardStrategy()));
                 services.AddSingleton<IShuffleableDesk>(new Shuffleable36CardDesk());
               
            });    
    
        */

        CoreConfig config = new CoreConfig(100, DbRequest.UseGenerated);
        
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                Console.WriteLine($"db path is: {hostContext.Configuration.GetConnectionString("ExperimentDatabase")}");
                var uris = new List<Uri>();
                uris.Add(new Uri(hostContext.Configuration.GetConnectionString("Player1")!));
                uris.Add(new Uri(hostContext.Configuration.GetConnectionString("Player2")!));
                var expConfig = new ExperimentConfig {Uris = uris};
                
                services.AddHostedService<SandboxDbWorker>();
                services.AddSingleton<IDeskShuffler, RandomDeskShuffler>();
                services.AddScoped<ISandBox, NoShuffleableDeskSandbox>();
                services.AddSingleton<ShuffleableDesk>(new Shuffleable36CardDesk());
                services.AddSingleton<IDistributor, Zeus>();
                services.AddSingleton<IChooseCardAsync>(new AsyncOpponent(new HttpOpponentAsker(uris[0])));
                services.AddSingleton<IChooseCardAsync>(new AsyncOpponent(new HttpOpponentAsker(uris[1])));
                services.AddDbContextFactory<ExperimentConditionContext>(
                    options => options.UseSqlite(
                        hostContext.Configuration.GetConnectionString("ExperimentDatabase")));
                services.AddSingleton<ExperimentConditionService>();
                services.AddSingleton(config);
                services.AddSingleton(expConfig);
            });
    }
}