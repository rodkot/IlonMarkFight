using Core.Config;
using DataLib.SandBoxes.Interfaces;
using DataLib.Shuffler.Interfaces;
using DbStorage.Service;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models;

namespace Core.Workers;

public class SandboxDbWorker : BackgroundService
{
    private readonly ILogger<SandboxDbWorker> _logger;
    private readonly ExperimentConditionService _service;
    private readonly ShuffleableDesk _cardDesk;
    private readonly CoreConfig _config;
    private readonly IHostApplicationLifetime _lifetime;
    private readonly IDeskShuffler _shuffler;
    private readonly ISandBox _sandbox;


    public SandboxDbWorker(ILogger<SandboxDbWorker> logger, ExperimentConditionService service, ISandBox sandbox,
        IHostApplicationLifetime lifetime, IDeskShuffler shuffler, CoreConfig config, ShuffleableDesk cardDesk)
    {
        _logger = logger;
        _service = service;
        _sandbox = sandbox;
        _lifetime = lifetime;
        _shuffler = shuffler;
        _config = config;
        _cardDesk = cardDesk;
        _sandbox = sandbox;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        switch (_config.Request)
        {
            case DbRequest.Generate:
            {
                _logger.LogInformation("start generating experiment conditions");
                return Generate(stoppingToken);
            }
            case DbRequest.UseGenerated:
            {
                _logger.LogInformation("using already generated experiment conditions");
                return UseGenerated(stoppingToken);
            }
            default:
            {
                _logger.LogError("stopping application");
                _lifetime.StopApplication();
                return Task.CompletedTask;
            }
        }
    }

    private Task UseGenerated(CancellationToken stoppingToken)
    {
        var success = 0;
        var completed = 0;
        try
        {
            var desks = _service.GetFirstN(_config.ExperimentCount);
            for (var i = 0; i < desks.Count && !stoppingToken.IsCancellationRequested; i++)
            {
                if (_sandbox.Round(desks[i]))
                {
                    success += 1;
                }

                completed += 1;
            }
        }
        catch (Exception e)
        {
            _logger.LogCritical(e.Message);
        }
        finally
        {
            _lifetime.StopApplication();
        }

        _logger.LogInformation($"Experiments completed: {completed}");
        _logger.LogInformation($"Success rate: {(double)success / completed}");

        return Task.CompletedTask;
    }

    private Task Generate(CancellationToken stoppingToken)
    {
        try
        {
            _service.RecreateDb();
            for (var i = 0; i < _config.ExperimentCount && !stoppingToken.IsCancellationRequested; i++)
            {
                _shuffler.Shuffle(_cardDesk);
                _service.AddOne(_cardDesk);
            }

            _logger.LogInformation($"generated {_config.ExperimentCount} experiments");
        }
        catch (Exception e)
        {
            _logger.LogCritical(e.Message);
        }
        finally
        {
            _lifetime.StopApplication();
        }

        return Task.CompletedTask;
    }
}