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
        return args.Length switch
        {
            1 => Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    var config = new CoreConfig(int.Parse(args[0]), DbRequest.None);
                    
                    services.AddHostedService<SandboxWorker>();
                    services.AddScoped<ISandBox, NoShuffleableDeskSandbox>();
                    services.AddSingleton<IDeskShuffler, RandomDeskShuffler>();
                    services.AddSingleton<IDistributor, Zeus>();
                    services.AddSingleton<Opponent>(new Ilon(new PickFirstCardStrategy()));
                    services.AddSingleton<Opponent>(new Mark(new PickFirstCardStrategy()));
                    services.AddSingleton<IShuffleableDesk>(new Shuffleable36CardDesk());
                    services.AddSingleton(config);
                }),
            2 => Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var config = args[0] switch
                    {
                        "generate" => new CoreConfig(int.Parse(args[1]), DbRequest.Generate),
                        "useGenerated" => new CoreConfig(int.Parse(args[1]), DbRequest.UseGenerated),
                        _ => throw new ArgumentException(
                            $"bad cmd argument, available arguments are: generate, useGenerated")
                    };

                    var firstUri = new Uri(hostContext.Configuration.GetConnectionString("Opponents1")!);
                    var secondUri = new Uri(hostContext.Configuration.GetConnectionString("Opponents2")!);

                    services.AddHostedService<SandboxDbWorker>();
                    services.AddSingleton<IDeskShuffler, RandomDeskShuffler>();
                    services.AddScoped<ISandBox, NoShuffleableDeskSandbox>();
                    services.AddSingleton<ShuffleableDesk>(new Shuffleable36CardDesk());
                    services.AddSingleton<IDistributor, Zeus>();
                    services.AddSingleton<IChooseCardAsync>(new AsyncOpponent(new HttpOpponentAsker(firstUri)));
                    services.AddSingleton<IChooseCardAsync>(new AsyncOpponent(new HttpOpponentAsker(secondUri)));
                    // services.AddDbContextFactory<ExperimentConditionContext>(options =>
                    //     options.UseSqlite(hostContext.Configuration.GetConnectionString("Database")));
                    services.AddDbContextFactory<ExperimentConditionContext>(
                        options => options.UseSqlite(
                            hostContext.Configuration.GetConnectionString("Database")));
                    services.AddSingleton<ExperimentConditionService>();
                    services.AddSingleton(config);
                }),
            _ => throw new ArgumentException($"wrong amount of arguments, expected 1 or 2 has {args.Length}")
        };
    }
}