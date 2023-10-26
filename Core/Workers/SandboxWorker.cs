using DataLib.SandBoxes;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Workers;

public class SandboxWorker : BackgroundService
{
    private static int Count => 1_000_000;
    private readonly Sandbox _sandbox;
    private readonly ILogger _logger;
    private readonly IHostApplicationLifetime _lifetime;

    public SandboxWorker(Sandbox sandbox, ILogger logger, IHostApplicationLifetime lifetime)
    {
        _sandbox = sandbox;
        _logger = logger;
        _lifetime = lifetime;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var success = 0;
        var completed = 0;
        for (var i = 0; i < Count && !stoppingToken.IsCancellationRequested; i++)
        {
            
            if (i % 100000 == 0)
            {
                _logger.LogInformation($"Completed {i} iteration");
            }

            if (_sandbox.Round())
            {
                success += 1;
            }

            completed += 1;
        }

        Console.WriteLine($"Experiments completed: {completed}");
        Console.WriteLine($"Success rate: {(double)success / completed}");

        _lifetime.StopApplication();

        return Task.CompletedTask;
    }
}