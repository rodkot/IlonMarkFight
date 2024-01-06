using Core.Config;
using Core.Sandboxes;
using DataLib.Desks.Interfaces;
using DataLib.SandBoxes;
using DataLib.SandBoxes.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Workers;

public class SandboxWorker : BackgroundService
{
    private readonly int _count;
    private readonly ISandBox _sandbox;
    private readonly ILogger _logger;
    private readonly IShuffleableDesk _shuffleableDesk;
    private readonly IHostApplicationLifetime _lifetime;

    public SandboxWorker(ISandBox sandbox, ILogger<SandboxWorker>  logger, IHostApplicationLifetime lifetime, IShuffleableDesk shuffleableDesk, CoreConfig config, int count)
    {
        _sandbox = sandbox;
        _logger = logger;
        _lifetime = lifetime;
        _shuffleableDesk = shuffleableDesk;
        _count = config.ExperimentCount;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var success = 0;
        var completed = 0;
        for (var i = 0; i < _count && !stoppingToken.IsCancellationRequested; i++)
        {
            
            if (i % 100000 == 0)
            {
                _logger.LogInformation($"Completed {i} iteration");
            }

            if (_sandbox.Round(_shuffleableDesk))
            {
                success += 1;
            }

            completed += 1;
        }

        _logger.LogInformation($"Experiments completed: {completed}");
        _logger.LogInformation($"Success rate: {(double)success / completed}");

        _lifetime.StopApplication();

        return Task.CompletedTask;
    }
}